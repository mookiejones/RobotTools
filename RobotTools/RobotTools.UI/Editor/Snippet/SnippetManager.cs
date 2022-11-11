using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RobotTools.UI.Editor.Snippet
{
    public class SnippetManager
    {
        private static readonly Dictionary<string, SnippetInfo> Snippets = new Dictionary<string, SnippetInfo>();
        private static readonly Dictionary<string, List<SnippetInfo>> SnippetsByExtension = new Dictionary<string, List<SnippetInfo>>();

        public static IList<SnippetCompletionData> CompletionData
        {
            get
            {
                var dictionary = new Dictionary<SnippetInfo, string>();
                var list = new List<SnippetCompletionData>();
                foreach (var current in Snippets.Values)
                {
                    if (!dictionary.ContainsKey(current))
                    {
                        list.Add(new SnippetCompletionData(current));
                        dictionary.Add(current, string.Empty);
                    }
                }
                return list;
            }
        }
        public static IEnumerable<SnippetCompletionData> GetCompletionDataForExtension(string extension)
        {
            if (SnippetsByExtension.ContainsKey(extension))
            {
                foreach (var current in SnippetsByExtension[extension])
                {
                    yield return new SnippetCompletionData(current);
                }
            }
            yield break;
        }
        public static SnippetInfo GetSnippetForShortcut(string shortCut)
        {
            if (Snippets.ContainsKey(shortCut))
            {
                return Snippets[shortCut];
            }
            return null;
        }
        public static IEnumerable<SnippetInfo> GetSnippetsForExtension(string extension)
        {
            if (SnippetsByExtension.ContainsKey(extension))
            {
                foreach (var current in SnippetsByExtension[extension])
                {
                    yield return current;
                }
            }
            yield break;
        }
        public static bool HasSnippetsForExtension(string extension)
        {
            return SnippetsByExtension.ContainsKey(extension);
        }
        public static bool HasSnippetsFor(string shortCut, string extension)
        {
            if (Snippets.ContainsKey(shortCut))
            {
                var snippetInfo = Snippets[shortCut];
                if (snippetInfo.Header.Extensions.Contains(extension))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool KnowsShortCut(string shortCut)
        {
            return Snippets.ContainsKey(shortCut);
        }
        public static bool LoadSnippet(string file)
        {
            file = Path.GetFullPath(file);
            if (!File.Exists(file))
            {
                return false;
            }
            bool result;
            try
            {
                var xElement = XElement.Load(file);
                if (xElement.Name != "CodeSnippets")
                {
                    result = false;
                }
                else
                {
                    if (xElement.Elements("CodeSnippet").Count<XElement>() == 0)
                    {
                        result = false;
                    }
                    else
                    {
                        foreach (var current in xElement.Elements("CodeSnippet"))
                        {
                            var snippetInfo = BuildSnippet(current, file);
                            foreach (var current2 in snippetInfo.Header.Shortcuts)
                            {
                                if (!Snippets.ContainsKey(current2))
                                {
                                    Snippets.Add(current2, snippetInfo);
                                }
                                else
                                {
                                    var msg = new ErrorMessage(string.Format("Duplicate Shortcut :", file), null,
                                        MessageType.Error);
                                    Messenger.Default.Send(msg);

                                }
                            }
                            foreach (var current3 in snippetInfo.Header.Extensions)
                            {
                                if (SnippetsByExtension.ContainsKey(current3))
                                {
                                    var list = SnippetsByExtension[current3];
                                    if (!list.Contains(snippetInfo))
                                    {
                                        list.Add(snippetInfo);
                                    }
                                }
                                else
                                {
                                    SnippetsByExtension[current3] = new List<SnippetInfo>
                                    {
                                        snippetInfo
                                    };
                                }
                            }
                        }
                        result = true;
                    }
                }
            }
            catch (Exception ex2)
            {
                var msg = new ErrorMessage("ErrorOnLoadingSnippet", ex2, MessageType.Error);
                Messenger.Default.Send(msg);

                result = false;
            }
            return result;
        }
        public static void LoadSnippets(string directory)
        {
            if (!Directory.Exists(directory))
            {
                return;
            }
            foreach (var current in
                from x in Directory.GetFiles(directory)
                where x.ToLowerInvariant().EndsWith(".snippet")
                select x)
            {
                LoadSnippet(current);
            }
        }
        public static void ImportSnippet(string sourceFilePath, string targetDirectory)
        {
            if (!Path.IsPathRooted(targetDirectory))
            {
                targetDirectory = Path.GetFullPath(targetDirectory);
            }
            if (!Path.IsPathRooted(sourceFilePath))
            {
                sourceFilePath = Path.GetFullPath(sourceFilePath);
            }
            if (File.Exists(targetDirectory))
            {
                throw new ArgumentException("Target directory is an existing file.");
            }
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }
            var name = FileExtended.GetName(sourceFilePath);
            var destFileName = Path.Combine(targetDirectory, name);
            if (LoadSnippet(sourceFilePath))
            {
                File.Copy(sourceFilePath, destFileName, true);
            }
        }
        private static SnippetInfo BuildSnippet(XElement element, string path)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            var xAttribute = element.Attribute("Format");
            if (xAttribute == null)
            {
                throw new XmlException("The Attribute 'Format' is missing on element'" + element + "'");
            }
            var value = xAttribute.Value;
            var headerElement = element.Element("Header");
            var header = new SnippetHeader(headerElement);
            var element2 = element.Element("Snippet");
            Snippet snippet = SnippetParser.BuildSnippet(element2);
            return new SnippetInfo(path)
            {
                Version = value,
                Snippet = snippet,
                Header = header
            };
        }
    }

    public class SnippetInfo
    {
        public string Version
        {
            get;
            set;
        }
        public string Path
        {
            get;
            private set;
        }
        public string Filename
        {
            get
            {
                if (string.IsNullOrEmpty(Path))
                {
                    return string.Empty;
                }
                return System.IO.Path.GetFileName(Path);
            }
        }
        public SnippetHeader Header
        {
            get;
            set;
        }
        public ICSharpCode.AvalonEdit.Snippets.Snippet Snippet
        {
            get;
            set;
        }
        public SnippetInfo()
        {
        }
        internal SnippetInfo(string path)
        {
            Path = path;
        }
    }
    public class SnippetHeader
    {
        public string Title
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }
        public List<string> Shortcuts
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public List<string> Extensions
        {
            get;
            set;
        }
        public string Author
        {
            get;
            set;
        }
        public List<SnippetType> Types
        {
            get;
            set;
        }
        public SnippetHeader(XElement headerElement)
        {

            throw new NotImplementedException();
            /*        Title = headerElement.ElementsValue("Title", "No title");
            Description = headerElement.ElementsValue("Description", "No description");
            Author = headerElement.ElementsValue("Author", "No author");
            Text = headerElement.ElementsValue("Text", Title);
     * */
            Shortcuts = new List<string>();
            foreach (var current in headerElement.Elements("Shortcut"))
            {
                if (!string.IsNullOrEmpty(current.Value) && !Shortcuts.Contains(current.Value))
                {
                    Shortcuts.Add(current.Value);
                }
            }
            Types = new List<SnippetType>();
            var xElement = headerElement.Elements("SnippetTypes").FirstOrDefault<XElement>();
            if (xElement != null)
            {
                foreach (var current2 in xElement.Elements("SnippetType"))
                {
                    try
                    {
                        var item = (SnippetType)Enum.Parse(typeof(SnippetType), current2.Value);
                        Types.Add(item);
                    }
                    catch
                    {
                    }
                }
            }
            LoadExtension(headerElement);
        }


        private void LoadExtension(XElement headerElement)
        {
            if (headerElement == null)
            {
                throw new ArgumentNullException("headerElement");
            }
            Extensions = new List<string>();
            var enumerable = headerElement.Elements("Extensions");
            foreach (var current in enumerable)
            {
                var value = current.Value;
                var array = value.Split(new char[]
                {
                    ' '
                });
                var array2 = array;
                for (var i = 0; i < array2.Length; i++)
                {
                    var text = array2[i];
                    if (!string.IsNullOrEmpty(text) && !Extensions.Contains(text))
                    {
                        var text2 = text;
                        if (!text.StartsWith("."))
                        {
                            text2 += ".";
                        }
                        Extensions.Add(text2);
                    }
                }
            }
        }
    }
}
