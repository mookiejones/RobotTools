using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Snippets;
using RobotTools.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using System.Windows.Media;
using System.Xml.Linq;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
namespace RobotTools.UI.Editor.Snippets
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
                    if (!xElement.Elements("CodeSnippet").Any())
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
            var name = FileEx.GetName(sourceFilePath);
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

    public class SnippetCompletionData : ICSharpCode.AvalonEdit.CodeCompletion.ICompletionData
    {
        private readonly SnippetInfo snippetInfo;
        public  object Content=> snippetInfo.Header.Text;

        public  object Description=> new SnippetToolTip(snippetInfo);
       
        public  ImageSource Image=> null;

       
        public double Priority=>  100.0;

        public string Text=> snippetInfo.Header.Text;

        public SnippetCompletionData(SnippetInfo snippetInfo)
        {
            this.snippetInfo = snippetInfo;
        }
        public  void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            if (snippetInfo.Header.Types.Contains(SnippetType.Expansion) && textArea.Selection.IsEmpty)
            {
                ReplaceIfNeeded(textArea, snippetInfo);
                snippetInfo.Snippet.Insert(textArea);
                return;
            }
            if (snippetInfo.Header.Types.Contains(SnippetType.SurroundsWith) && !textArea.Selection.IsEmpty)
            {
                snippetInfo.Snippet.Insert(textArea);
            }
        }
        private static bool IsWhitespace(char ch)
        {
            return ch == '\t' || ch == ' ' || ch == '\n';
        }
        private bool ReplaceIfNeeded(TextArea area, SnippetInfo snippInfo)
        {
            var i = area.Caret.Offset;
            var shortcuts = snippInfo.Header.Shortcuts;
            var num = -1;
            var document = area.Document;
            if (i <= 0)
            {
                return false;
            }
            while (i > 0)
            {
                if (i >= document.TextLength)
                {
                    i--;
                }
                var charAt = document.GetCharAt(i);
                if (IsWhitespace(charAt))
                {
                    num = i + 1;
                    break;
                }
                i--;
                num = i;
            }
            if (num < area.Caret.Offset)
            {
                num = Math.Max(num, 0);
                var length = area.Caret.Offset - num;
                var text = document.GetText(num, length);
                if (shortcuts.Any((string shortcut) => shortcut.Contains(text)))
                {
                    document.Replace(num, length, string.Empty);
                    return true;
                }
            }
            return false;
        }
    }
}
