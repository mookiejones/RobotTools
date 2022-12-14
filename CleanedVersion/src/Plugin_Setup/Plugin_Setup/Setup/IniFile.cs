using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Plugin_Setup.Setup
{
    public class IniFile
    {
        public class IniSection
        {
            public class IniKey
            {
                private string m_sKey;
                private string m_sValue;
                private IniSection m_section;
                public string Name
                {
                    get
                    {
                        return m_sKey;
                    }
                }
                public string Value
                {
                    get
                    {
                        return m_sValue;
                    }
                    set
                    {
                        m_sValue = value;
                    }
                }
                protected internal IniKey(IniSection parent, string sKey)
                {
                    m_section = parent;
                    m_sKey = sKey;
                }
                public void SetValue(string sValue)
                {
                    m_sValue = sValue;
                }
                public string GetValue()
                {
                    return m_sValue;
                }
                public bool SetName(string sKey)
                {
                    sKey = sKey.Trim();
                    if (sKey.Length != 0)
                    {
                        var key = m_section.GetKey(sKey);
                        if (key != this && key != null)
                        {
                            return false;
                        }
                        try
                        {
                            m_section.m_keys.Remove(m_sKey);
                            m_section.m_keys[sKey] = this;
                            m_sKey = sKey;
                            return true;
                        }
                        catch (Exception expr_66)
                        {
                            var ex = expr_66;
                            Trace.WriteLine(ex.Message);
                        }
                        return false;
                    }
                    return false;
                }
                public string GetName()
                {
                    return m_sKey;
                }
            }
            private IniFile m_pIniFile;
            private string m_sSection;
            private Hashtable m_keys;
            public ICollection Keys
            {
                get
                {
                    return m_keys.Values;
                }
            }
            public string Name
            {
                get
                {
                    return m_sSection;
                }
            }
            protected internal IniSection(IniFile parent, string sSection)
            {
                m_pIniFile = parent;
                m_sSection = sSection;
                m_keys = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            }
            public IniKey AddKey(string sKey)
            {
                sKey = sKey.Trim();
                IniKey iniKey = null;
                if (sKey.Length != 0)
                {
                    if (m_keys.ContainsKey(sKey))
                    {
                        iniKey = (IniKey)m_keys[sKey];
                    }
                    else
                    {
                        iniKey = new IniKey(this, sKey);
                        m_keys[sKey] = iniKey;
                    }
                }
                return iniKey;
            }
            public bool RemoveKey(string sKey)
            {
                return RemoveKey(GetKey(sKey));
            }
            public bool RemoveKey(IniKey Key)
            {
                if (Key != null)
                {
                    try
                    {
                        m_keys.Remove(Key.Name);
                        return true;
                    }
                    catch (Exception expr_1E)
                    {

                        var ex = expr_1E;
                        Trace.WriteLine(ex.Message);

                    }
                    return false;
                }
                return false;
            }
            public bool RemoveAllKeys()
            {
                m_keys.Clear();
                return m_keys.Count == 0;
            }
            public IniKey GetKey(string sKey)
            {
                sKey = sKey.Trim();
                if (m_keys.ContainsKey(sKey))
                {
                    return (IniKey)m_keys[sKey];
                }
                return null;
            }
            public bool SetName(string sSection)
            {
                sSection = sSection.Trim();
                if (sSection.Length != 0)
                {
                    var section = m_pIniFile.GetSection(sSection);
                    if (section != this && section != null)
                    {
                        return false;
                    }
                    try
                    {
                        m_pIniFile.m_sections.Remove(m_sSection);
                        m_pIniFile.m_sections[sSection] = this;
                        m_sSection = sSection;
                        return true;
                    }
                    catch (Exception expr_66)
                    {
                        var ex = expr_66;
                        Trace.WriteLine(ex.Message);
                    }
                    return false;
                }
                return false;
            }
            public string GetName()
            {
                return m_sSection;
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
            var streamReader = new StreamReader(sFileName);
            var regex = new Regex("^([\\s]*#.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var regex2 = new Regex("^[\\s]*\\[[\\s]*([^\\[\\s].*[^\\s\\]])[\\s]*\\][\\s]*$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var regex3 = new Regex("^\\s*([^=\\s]*)[^=]*=(.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            while (!streamReader.EndOfStream)
            {
                var text = streamReader.ReadLine();
                if (!String.IsNullOrEmpty(text))
                {
                    if (regex.Match(text).Success)
                    {
                        var match = regex.Match(text);
                        Trace.WriteLine(string.Format("Skipping Comment: {0}", match.Groups[0].Value));
                    }
                    else
                    {
                        if (regex2.Match(text).Success)
                        {
                            var match = regex2.Match(text);
                            Trace.WriteLine(string.Format("Adding section [{0}]", match.Groups[1].Value));
                            iniSection = AddSection(match.Groups[1].Value);
                        }
                        else
                        {
                            if (regex3.Match(text).Success && iniSection != null)
                            {
                                var match = regex3.Match(text);
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
            var streamReader = new StreamReader(ms);
            var regex = new Regex("^([\\s]*#.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var regex2 = new Regex("^[\\s]*\\[[\\s]*([^\\[\\s].*[^\\s\\]])[\\s]*\\][\\s]*$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var regex3 = new Regex("^\\s*([^=\\s]*)[^=]*=(.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            while (!streamReader.EndOfStream)
            {
                var text = streamReader.ReadLine();
                if (!String.IsNullOrEmpty(text))
                {
                    if (regex.Match(text).Success)
                    {
                        var match = regex.Match(text);
                        Trace.WriteLine(string.Format("Skipping Comment: {0}", match.Groups[0].Value));
                    }
                    else
                    {
                        if (regex2.Match(text).Success)
                        {
                            var match = regex2.Match(text);
                            Trace.WriteLine(string.Format("Adding section [{0}]", match.Groups[1].Value));
                            iniSection = AddSection(match.Groups[1].Value);
                        }
                        else
                        {
                            if (regex3.Match(text).Success && iniSection != null)
                            {
                                var match = regex3.Match(text);
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
            var streamWriter = new StreamWriter(sFileName, false);
            try
            {
                var enumerator = Sections.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var iniSection = (IniSection)enumerator.Current;
                    Trace.WriteLine(string.Format("Writing Section: [{0}]", iniSection.Name));
                    streamWriter.WriteLine(string.Format("[{0}]", iniSection.Name));
                    try
                    {
                        var enumerator2 = iniSection.Keys.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            var iniKey = (IniSection.IniKey)enumerator2.Current;
                            if (!String.IsNullOrEmpty(iniKey.Value))
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
                        IEnumerator enumerator2 =null;
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
                    var ex = expr_1E;
                    Trace.WriteLine(ex.Message);

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
            var section = GetSection(sSection);
            if (section != null)
            {
                var key = section.GetKey(sKey);
                if (key != null)
                {
                    return key.Value;
                }
            }
            return string.Empty;
        }
        public bool SetKeyValue(string sSection, string sKey, string sValue)
        {
            var iniSection = AddSection(sSection);
            if (iniSection != null)
            {
                var iniKey = iniSection.AddKey(sKey);
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
            var result = false;
            var section = GetSection(sSection);
            if (section != null)
            {
                result = section.SetName(sNewSection);
            }
            return result;
        }
        public bool RenameKey(string sSection, string sKey, string sNewKey)
        {
            var section = GetSection(sSection);
            if (section != null)
            {
                var key = section.GetKey(sKey);
                if (key != null)
                {
                    return key.SetName(sNewKey);
                }
            }
            return false;
        }
        public bool RemoveKey(string sSection, string sKey)
        {
            var section = GetSection(sSection);
            return section != null && section.RemoveKey(sKey);
        }
    }
}
