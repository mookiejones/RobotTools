using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RobotTools.UI.Editor.Folding
{
    class KukaRegionFoldingStrategy:AbstractFoldingStrategy
    {
        protected override IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
        {
            firstErrorOffset = -1;
            var list = new List<LanguageFold>();
            list.AddRange(CreateFoldingHelper(document, ";fold", ";endfold", true));
            list.AddRange(CreateFoldingHelper(document, "def", "end", true));
            list.AddRange(CreateFoldingHelper(document, "global def", "end", true));
            list.AddRange(CreateFoldingHelper(document, "global deffct", "endfct", true));
            list.AddRange(CreateFoldingHelper(document, "deftp", "endtp", true));
            list.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            return list;
        }

        internal  static string FoldTitle(FoldingSection section, TextDocument doc)
        {
            var array = Regex.Split(section.Title, "�");
            var text = section.TextContent.ToLower().Trim();
            var text2 = section.TextContent.Trim();
            var num = section.TextContent.Trim().IndexOf("%{PE}%", StringComparison.Ordinal) - "%{PE}%".Length;
            var num2 = section.TextContent.Trim().IndexOf("\r\n", StringComparison.Ordinal);
            var num3 = section.StartOffset + array[0].Length;
//            text2 = text2.Substring(text.IndexOf(array[0], StringComparison.Ordinal) + array[0].Length);
            var num4 = text2.Length - array[0].Length;
            if (num > -1)
            {
                num4 = ((num < num2) ? num : num4);
            }
            return text2.Substring(0, num4);
        }

    }
}
