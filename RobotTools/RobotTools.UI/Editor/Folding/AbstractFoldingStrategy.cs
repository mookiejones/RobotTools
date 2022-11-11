using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using System;
using System.Collections.Generic;

namespace RobotTools.UI.Editor.Folding
{
    abstract class AbstractFoldingStrategy
    {
        public void UpdateFoldings(FoldingManager manager, TextDocument document)
        {
            int firstErrorOffset;
            var newFoldings = CreateNewFoldings(document, out firstErrorOffset);
            manager.UpdateFoldings(newFoldings, firstErrorOffset);
        }

        protected abstract IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOfffset);


        protected static IEnumerable<LanguageFold> CreateFoldingHelper(ITextSource document, string startFold,
            string endFold, bool defaultclosed)
        {
            var list = new List<LanguageFold>();
            var stack = new Stack<int>();
            var textDocument = document as TextDocument;
            endFold = endFold.ToLower();
            if (textDocument != null)
            {
                foreach (var current in textDocument.Lines)
                {
                    var lineByNumber = textDocument.GetLineByNumber(current.LineNumber);
                    var text = textDocument.GetText(lineByNumber.Offset, lineByNumber.Length).ToLower();
                    var text2 = text.TrimStart();
                    try
                    {
                        if (IsValidFold(text, startFold, endFold))
                        {
                            if (text2.StartsWith(startFold))
                            {
                                stack.Push(lineByNumber.Offset);
                            }
                            else
                            {
                                if (text2.StartsWith(endFold) && stack.Count > 0)
                                {
                                    bool flag;
                                    if (endFold == "end")
                                    {
                                        if (text.Length == endFold.Length)
                                        {
                                            flag = true;
                                        }
                                        else
                                        {
                                            var array = text.ToCharArray(endFold.Length, 1);
                                            flag = !char.IsLetterOrDigit(array[0]);
                                        }
                                    }
                                    else
                                    {
                                        flag = true;
                                    }
                                    if (flag)
                                    {
                                        var num2 = stack.Pop();
                                        var end = lineByNumber.Offset + text.Length;

                                        var offset = num2 + startFold.Length + 1;
                                        var length = lineByNumber.Offset - num2 - endFold.Length;
                                        if (offset + length > textDocument.TextLength)
                                        {

                                        }
                                        else
                                        {
                                            var text3 = textDocument.GetText(offset, length);
                                            var item = new LanguageFold(num2, end, text3, startFold, endFold, defaultclosed);
                                            list.Add(item);
                                        }
                                    }
                                }
                                else
                                {
                                    //num++;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return list;
        }

        private static bool IsValidFold(string text, string s, string e)
        {
            text = text.Trim();
            var flag = text.StartsWith(s);
            var flag2 = text.StartsWith(e);
            bool result;
            if (!(flag | flag2))
            {
                result = false;
            }
            else
            {
                var text2 = flag ? s : e;
                if (text.Substring(text.IndexOf(text2, StringComparison.Ordinal) + text2.Length).Length == 0)
                {
                    result = true;
                }
                else
                {
                    var value = text.Substring(text.IndexOf(text2, StringComparison.Ordinal) + text2.Length, 1);
                    var c = Convert.ToChar(value);
                    var flag3 = char.IsLetterOrDigit(c);
                    result = !flag3;
                }
            }
            return result;
        }
    }
}
