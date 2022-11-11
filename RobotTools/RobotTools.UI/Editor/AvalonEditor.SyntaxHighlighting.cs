using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Highlighting;
using RobotTools.UI.Editor.Completion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RobotTools.UI.Editor
{
    public partial class AvalonEditor
    {
        public void SetHighlighting()
        {
            try
            {
                if (Filename != null)
                {
                    SyntaxHighlighting =                   HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(Filename));
                }
                else
                {
                    var opt = Options.EnableHyperlinks;
                    SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(".src");
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private IEnumerable<ICompletionData> HighlightList()
        {
            var items = new List<CodeCompletion>();
            foreach (var current in
                from rule in SyntaxHighlighting.MainRuleSet.Rules
                select rule.Regex.ToString()
                    into parseString
                let start = parseString.IndexOf(">", StringComparison.Ordinal) + 1
                let end = parseString.LastIndexOf(")", StringComparison.Ordinal)
                select parseString.Substring(start, end - start)
                        into parseString1
                select parseString1.Split(new[]
        {
                    '|'
                })
                            into spl
                from item in
                from t in spl
                where !string.IsNullOrEmpty(t)
                select new CodeCompletion(t.Replace("\\b", ""))
                        into item
                where !items.Contains(item) && char.IsLetter(item.Text, 0)
                select item
                select item)
            {
                items.Add(current);
            }
            return items.ToArray();
        }
    }
}
