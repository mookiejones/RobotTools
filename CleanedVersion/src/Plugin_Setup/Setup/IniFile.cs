using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
namespace Setup
{
	public class IniFile
	{
		public class IniSection
		{
			public class IniKey
			{
			    private readonly IniSection _mSection;
			    public string Name { get; private set; }

			    public string Value { get; set; }

			    protected internal IniKey(IniSection parent, string sKey)
				{
					_mSection = parent;
					Name = sKey;
				}
				public void SetValue(string sValue)
				{
					Value = sValue;
				}
				public string GetValue()
				{
					return Value;
				}
				public bool SetName(string sKey)
				{
					sKey = sKey.Trim();
					if (sKey.Length != 0)
					{
						IniKey key = _mSection.GetKey(sKey);
						if (key != this && key != null)
						{
							return false;
						}
						try
						{
							_mSection._mKeys.Remove(Name);
							_mSection._mKeys[sKey] = this;
							Name = sKey;
							return true;
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							
							Trace.WriteLine(ex.Message);
							ProjectData.ClearProjectError();
						}
						return false;
					}
					return false;
				}
				public string GetName()
				{
					return Name;
				}
			}
			private IniFile m_pIniFile;
			private string _mSSection;
			private readonly Hashtable _mKeys;
			public ICollection Keys
			{
				get
				{
					return _mKeys.Values;
				}
			}
			public string Name
			{
				get
				{
					return _mSSection;
				}
			}
			protected internal IniSection(IniFile parent, string sSection)
			{
				m_pIniFile = parent;
				_mSSection = sSection;
				_mKeys = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			}
			public IniKey AddKey(string sKey)
			{
				sKey = sKey.Trim();
				IniKey iniKey = null;
				if (sKey.Length != 0)
				{
					if (_mKeys.ContainsKey(sKey))
					{
						iniKey = (IniKey)_mKeys[sKey];
					}
					else
					{
						iniKey = new IniKey(this, sKey);
						_mKeys[sKey] = iniKey;
					}
				}
				return iniKey;
			}
			public bool RemoveKey(string sKey)
			{
				return RemoveKey(GetKey(sKey));
			}
			public bool RemoveKey(IniKey key)
			{
				if (key != null)
				{
					try
					{
						_mKeys.Remove(key.Name);
						return true;
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						
						Trace.WriteLine(ex.Message);
						ProjectData.ClearProjectError();
					}
					return false;
				}
				return false;
			}
			public bool RemoveAllKeys()
			{
				_mKeys.Clear();
				return _mKeys.Count == 0;
			}
			public IniKey GetKey(string sKey)
			{
				sKey = sKey.Trim();
				if (_mKeys.ContainsKey(sKey))
				{
					return (IniKey)_mKeys[sKey];
				}
				return null;
			}
			public bool SetName(string sSection)
			{
				sSection = sSection.Trim();
				if (sSection.Length != 0)
				{
					IniSection section = m_pIniFile.GetSection(sSection);
					if (section != this && section != null)
					{
						return false;
					}
					try
					{
						m_pIniFile.m_sections.Remove(_mSSection);
						m_pIniFile.m_sections[sSection] = this;
						_mSSection = sSection;
						return true;
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						
						Trace.WriteLine(ex.Message);
						ProjectData.ClearProjectError();
					}
					return false;
				}
				return false;
			}
			public string GetName()
			{
				return _mSSection;
			}
		}
		private Hashtable m_sections;
		public ICollection Sections
		{
			get
			{
				return m_sections.Values;
			}
		}
		public IniFile()
		{
			m_sections = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		}
		public void Load(string sFileName, bool bMerge = false)
		{
			if (!bMerge)
			{
				RemoveAllSections();
			}
			IniSection iniSection = null;
			StreamReader streamReader = new StreamReader(sFileName);
			Regex regex = new Regex("^([\\s]*#.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			Regex regex2 = new Regex("^[\\s]*\\[[\\s]*([^\\[\\s].*[^\\s\\]])[\\s]*\\][\\s]*$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			Regex regex3 = new Regex("^\\s*([^=\\s]*)[^=]*=(.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			while (!streamReader.EndOfStream)
			{
				string text = streamReader.ReadLine();
				if (Operators.CompareString(text, string.Empty, false) != 0)
				{
					if (regex.Match(text).Success)
					{
						Match match = regex.Match(text);
						Trace.WriteLine(string.Format("Skipping Comment: {0}", match.Groups[0].Value));
					}
					else
					{
						if (regex2.Match(text).Success)
						{
							Match match = regex2.Match(text);
							Trace.WriteLine(string.Format("Adding section [{0}]", match.Groups[1].Value));
							iniSection = AddSection(match.Groups[1].Value);
						}
						else
						{
							if (regex3.Match(text).Success && iniSection != null)
							{
								Match match = regex3.Match(text);
								Trace.WriteLine(string.Format("Adding Key [{0}]=[{1}]", match.Groups[1].Value, match.Groups[2].Value));
								iniSection.AddKey(match.Groups[1].Value).Value = match.Groups[2].Value;
							}
							else
							{
								if (iniSection != null)
								{
									Trace.WriteLine(string.Format("Adding Key [{0}]", text));
									iniSection.AddKey(text);
								}
								else
								{
									Trace.WriteLine(string.Format("Skipping unknown type of data: {0}", text));
								}
							}
						}
					}
				}
			}
			streamReader.Close();
		}
		public void Load(MemoryStream ms)
		{
			RemoveAllSections();
			IniSection iniSection = null;
			StreamReader streamReader = new StreamReader(ms);
			Regex regex = new Regex("^([\\s]*#.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			Regex regex2 = new Regex("^[\\s]*\\[[\\s]*([^\\[\\s].*[^\\s\\]])[\\s]*\\][\\s]*$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			Regex regex3 = new Regex("^\\s*([^=\\s]*)[^=]*=(.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			while (!streamReader.EndOfStream)
			{
				string text = streamReader.ReadLine();
				if (Operators.CompareString(text, string.Empty, false) != 0)
				{
					if (regex.Match(text).Success)
					{
						Match match = regex.Match(text);
						Trace.WriteLine(string.Format("Skipping Comment: {0}", match.Groups[0].Value));
					}
					else
					{
						if (regex2.Match(text).Success)
						{
							Match match = regex2.Match(text);
							Trace.WriteLine(string.Format("Adding section [{0}]", match.Groups[1].Value));
							iniSection = AddSection(match.Groups[1].Value);
						}
						else
						{
							if (regex3.Match(text).Success && iniSection != null)
							{
								Match match = regex3.Match(text);
								Trace.WriteLine(string.Format("Adding Key [{0}]=[{1}]", match.Groups[1].Value, match.Groups[2].Value));
								iniSection.AddKey(match.Groups[1].Value).Value = match.Groups[2].Value;
							}
							else
							{
								if (iniSection != null)
								{
									Trace.WriteLine(string.Format("Adding Key [{0}]", text));
									iniSection.AddKey(text);
								}
								else
								{
									Trace.WriteLine(string.Format("Skipping unknown type of data: {0}", text));
								}
							}
						}
					}
				}
			}
			streamReader.Close();
		}
		public void Save(string sFileName)
		{
			StreamWriter streamWriter = new StreamWriter(sFileName, false);
			try
			{
				IEnumerator enumerator = Sections.GetEnumerator();
				while (enumerator.MoveNext())
				{
					IniSection iniSection = (IniSection)enumerator.Current;
					Trace.WriteLine(string.Format("Writing Section: [{0}]", iniSection.Name));
					streamWriter.WriteLine(string.Format("[{0}]", iniSection.Name));
					try
					{
						IEnumerator enumerator2 = iniSection.Keys.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							IniSection.IniKey iniKey = (IniSection.IniKey)enumerator2.Current;
							if (Operators.CompareString(iniKey.Value, string.Empty, false) != 0)
							{
								Trace.WriteLine(string.Format("Writing Key: {0}={1}", iniKey.Name, iniKey.Value));
								streamWriter.WriteLine(string.Format("{0}={1}", iniKey.Name, iniKey.Value));
							}
							else
							{
								Trace.WriteLine(string.Format("Writing Key: {0}", iniKey.Name));
								streamWriter.WriteLine(string.Format("{0}", iniKey.Name));
							}
						}
					}
					finally
					{
						IEnumerator enumerator2 = null;
						if (enumerator2 is IDisposable)
						{
							(enumerator2 as IDisposable).Dispose();
						}
					}
				}
			}
			finally
			{
				IEnumerator enumerator = null;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
			streamWriter.Close();
		}
		public IniSection AddSection(string sSection)
		{
			sSection = sSection.Trim();
			IniSection iniSection;
			if (m_sections.ContainsKey(sSection))
			{
				iniSection = (IniSection)m_sections[sSection];
			}
			else
			{
				iniSection = new IniSection(this, sSection);
				m_sections[sSection] = iniSection;
			}
			return iniSection;
		}
		public bool RemoveSection(string sSection)
		{
			sSection = sSection.Trim();
			return RemoveSection(GetSection(sSection));
		}
		public bool RemoveSection(IniSection Section)
		{
			if (Section != null)
			{
				try
				{
					m_sections.Remove(Section.Name);
					return true;
				}
				catch (Exception expr_1E)
				{
					ProjectData.SetProjectError(expr_1E);
					Exception ex = expr_1E;
					Trace.WriteLine(ex.Message);
					ProjectData.ClearProjectError();
				}
				return false;
			}
			return false;
		}
		public bool RemoveAllSections()
		{
			m_sections.Clear();
			return m_sections.Count == 0;
		}
		public IniSection GetSection(string sSection)
		{
			sSection = sSection.Trim();
			if (m_sections.ContainsKey(sSection))
			{
				return (IniSection)m_sections[sSection];
			}
			return null;
		}
		public string GetKeyValue(string sSection, string sKey)
		{
			IniSection section = GetSection(sSection);
			if (section != null)
			{
				IniSection.IniKey key = section.GetKey(sKey);
				if (key != null)
				{
					return key.Value;
				}
			}
			return string.Empty;
		}
		public bool SetKeyValue(string sSection, string sKey, string sValue)
		{
			IniSection iniSection = AddSection(sSection);
			if (iniSection != null)
			{
				IniSection.IniKey iniKey = iniSection.AddKey(sKey);
				if (iniKey != null)
				{
					iniKey.Value = sValue;
					return true;
				}
			}
			return false;
		}
		public bool RenameSection(string sSection, string sNewSection)
		{
			bool result = false;
			IniSection section = GetSection(sSection);
			if (section != null)
			{
				result = section.SetName(sNewSection);
			}
			return result;
		}
		public bool RenameKey(string sSection, string sKey, string sNewKey)
		{
			IniSection section = GetSection(sSection);
			if (section != null)
			{
				IniSection.IniKey key = section.GetKey(sKey);
				if (key != null)
				{
					return key.SetName(sNewKey);
				}
			}
			return false;
		}
		public bool RemoveKey(string sSection, string sKey)
		{
			IniSection section = GetSection(sSection);
			return section != null && section.RemoveKey(sKey);
		}
	}
}
