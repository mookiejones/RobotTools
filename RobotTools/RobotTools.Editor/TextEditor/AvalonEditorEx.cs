using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ICSharpCode.AvalonEdit.Document;

namespace RobotTools.Editor.TextEditor
{
    public static class DocumentUtilitites
    {
        private static int FindNextWordEnd(this TextDocument document, int offset)
        {
            return document.FindNextWordEnd(offset, new List<char>());
        }

        private static int FindNextWordEnd(this TextDocument document, int offset, IList<char> allowedChars)
        {
            for (var num = offset; num != -1; num++)
            {
                if (num >= document.TextLength)
                {
                    return -1;
                }
                var charAt = document.GetCharAt(num);
                if (!IsWordPart(charAt) && !allowedChars.Contains(charAt))
                {
                    return num;
                }
            }
            return -1;
        }

        private static int FindNextWordStart(this TextDocument document, int offset)
        {
            for (var num = offset; num != -1; num++)
            {
                if (num >= document.TextLength)
                {
                    return 0;
                }
                var charAt = document.GetCharAt(num);
                if (!IsWhitespaceOrNewline(charAt))
                {
                    return num;
                }
            }
            return 0;
        }

        public static int FindNextWordStartRelativeTo(this TextDocument document, int offset)
        {
            for (var num = offset; num != -1; num++)
            {
                var charAt = document.GetCharAt(num);
                if (!IsWhitespaceOrNewline(charAt))
                {
                    return num - offset;
                }
            }
            return 0;
        }

        public static int FindPrevWordStart(this TextDocument document, int offset)
        {
            for (var num = offset - 1; num != -1; num--)
            {
                var charAt = document.GetCharAt(num);
                if (!IsWordPart(charAt))
                {
                    return num + 1;
                }
            }
            return 0;
        }

        public static ISegment GetLineWithoutIndent(this TextDocument document, int lineNumber)
        {
            var lineByNumber = document.GetLineByNumber(lineNumber);
            var whitespaceAfter = TextUtilities.GetWhitespaceAfter(document, lineByNumber.Offset);
            if (whitespaceAfter.Length == 0)
            {
                return lineByNumber;
            }
            return new TextSegment
            {
                StartOffset = lineByNumber.Offset + whitespaceAfter.Length,
                EndOffset = lineByNumber.EndOffset,
                Length = lineByNumber.Length - whitespaceAfter.Length
            };
        }

        public static string GetWordBeforeCaret(this AvalonEditor editor)
        {
            if (editor == null)
            {
                throw new ArgumentNullException(nameof(editor));
            }
            var offset = editor.TextArea.Caret.Offset;
            var num = editor.Document.FindPrevWordStart(offset);
            if (num < 0)
            {
                return string.Empty;
            }
            return editor.Document.GetText(num, offset - num);
        }

        public static string GetWordBeforeCaret(this AvalonEditor editor, char[] allowedChars)
        {
            if (editor == null)
            {
                throw new ArgumentNullException(nameof(editor));
            }
            var offset = editor.TextArea.Caret.Offset;
            var num = FindPrevWordStart(editor.Document, offset, allowedChars);
            if (num < 0)
            {
                return string.Empty;
            }
            return editor.Document.GetText(num, offset - num);
        }

        public static string GetStringBeforeCaret(this AvalonEditor editor)
        {
            if (editor == null)
            {
                throw new ArgumentNullException(nameof(editor));
            }
            var line = editor.TextArea.Caret.Line;
            if (line < 1)
            {
                return string.Empty;
            }
            var offset = editor.TextArea.Caret.Offset;
            if (line > editor.Document.LineCount)
            {
                return string.Empty;
            }
            var lineByNumber = editor.Document.GetLineByNumber(line);
            var length = offset - lineByNumber.Offset;
            return editor.Document.GetText(lineByNumber.Offset, length);
        }

        public static string GetWordBeforeOffset(this AvalonEditor editor, int offset, char[] allowedChars)
        {
            if (editor == null)
            {
                throw new ArgumentNullException(nameof(editor));
            }
            var num = FindPrevWordStart(editor.Document, offset, allowedChars);
            if (num < 0)
            {
                return string.Empty;
            }
            return editor.Document.GetText(num, offset - num);
        }

        public static string GetTokenBeforeOffset(this AvalonEditor editor, int offset)
        {
            if (editor == null)
            {
                throw new ArgumentNullException(nameof(editor));
            }
            var num = -1;
            for (var i = offset - 1; i > -1; i--)
            {
                var charAt = editor.Document.GetCharAt(i);
                if (charAt == ' ' || charAt == '\n' || charAt == '\r' || charAt == '\t')
                {
                    num = i + 1;
                    break;
                }
            }
            if (num < 0)
            {
                return string.Empty;
            }
            return editor.Document.GetText(num, offset - num);
        }

        public static string GetWordUnderCaret(this AvalonEditor editor, char[] allowedChars)
        {
            if (editor == null)
            {
                throw new ArgumentNullException(nameof(editor));
            }
            var offset = editor.TextArea.Caret.Offset;
            var num = FindPrevWordStart(editor.Document, offset, allowedChars);
            var num2 = editor.Document.FindNextWordEnd(offset, allowedChars);
            if (num < 0 || num2 == 0 || num2 < num)
            {
                return string.Empty;
            }
            return editor.Document.GetText(num, num2 - num);
        }

        public static string GetFirstWordInLine(this AvalonEditor editor, int lineNumber)
        {
            if (editor == null)
            {
                throw new ArgumentNullException(nameof(editor));
            }
            return editor.Document.GetFirstWordInLine(lineNumber);
        }

        public static string GetFirstWordInLine(this TextDocument document, int lineNumber)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }
            var offset = document.GetOffset(lineNumber, 0);
            var num = document.FindNextWordStart(offset);
            if (num < 0)
            {
                return string.Empty;
            }
            var num2 = document.FindNextWordEnd(num);
            if (num2 < 0)
            {
                return string.Empty;
            }
            return document.GetText(num, num2 - num);
        }

        public static string GetWordUnderCaret(this AvalonEditor editor)
        {
            if (editor == null)
            {
                throw new ArgumentNullException(nameof(editor));
            }
            var offset = editor.TextArea.Caret.Offset;
            var num = editor.Document.FindPrevWordStart(offset);
            var num2 = editor.Document.FindNextWordEnd(offset);
            if (num < 0 || num2 == 0 || num2 < num)
            {
                return string.Empty;
            }
            return editor.Document.GetText(num, num2 - num);
        }

        public static string GetWordUnderOffset(this AvalonEditor editor, int offset)
        {
            if (editor == null)
            {
                throw new ArgumentNullException(nameof(editor));
            }
            var num = editor.Document.FindPrevWordStart(offset);
            var num2 = editor.Document.FindNextWordEnd(offset);
            if (num < 0 || num2 == 0 || num2 < num)
            {
                return string.Empty;
            }
            return editor.Document.GetText(num, num2 - num);
        }

        public static string GetWordUnderOffset(this AvalonEditor editor, int offset, char[] allowedChars)
        {
            if (editor == null)
            {
                throw new ArgumentNullException(nameof(editor));
            }
            var num = FindPrevWordStart(editor.Document, offset, allowedChars);
            var num2 = editor.Document.FindNextWordEnd(offset, allowedChars);
            if (num < 0 || num2 == 0 || num2 < num)
            {
                return string.Empty;
            }
            return editor.Document.GetText(num, num2 - num);
        }

        private static int FindPrevWordStart(TextDocument document, int offset, IList<char> allowedChars)
        {
            for (var num = offset - 1; num != -1; num--)
            {
                var charAt = document.GetCharAt(num);
                if (!IsWordPart(charAt) && !allowedChars.Contains(charAt))
                {
                    return num + 1;
                }
            }
            return 0;
        }

        public static bool IsWhitespaceOrNewline(char ch)
        {
            return ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r';
        }

        private static bool IsWordPart(char ch)
        {
            return char.IsLetterOrDigit(ch) || ch == '_';
        }
    }

    public static class FileExtended
    {
        public static bool AreEqual(string path1, string path2)
        {
            var fullName = new FileInfo(path1).FullName;
            var fullName2 = new FileInfo(path2).FullName;
            return fullName.Equals(fullName2, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string CopyIfExisting(string sourcePath, string targetPath)
        {
            if (!File.Exists(sourcePath))
            {
                throw new ArgumentException("File must exist.", nameof(sourcePath));
            }
            string text;
            if (Directory.Exists(targetPath))
            {
                text = targetPath;
                targetPath = Path.Combine(targetPath, GetName(sourcePath));
            }
            else
            {
                text = Path.GetDirectoryName(targetPath);
                if (text == null)
                {
                    throw new InvalidOperationException("Target path should not be null.");
                }
            }
            Directory.CreateDirectory(text);
            File.Copy(sourcePath, targetPath, true);
            return targetPath;
        }

        public static void CopyIfExisting(string sourceDirectory, string pattern, string targetDirectory)
        {
            var files = Directory.GetFiles(sourceDirectory, pattern);
            for (var i = 0; i < files.Length; i++)
            {
                var sourcePath = files[i];
                CopyIfExisting(sourcePath, targetDirectory);
            }
        }

        public static void DeleteIfExisting(string path)
        {
            DeleteIfExisting(path, true);
        }

        public static void DeleteIfExisting(string path, bool force)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (!File.Exists(path))
            {
                return;
            }
            if (force)
            {
                File.SetAttributes(path, FileAttributes.Normal);
            }
            File.Delete(path);
            for (var i = 0; i < 10; i++)
            {
                if (!File.Exists(path))
                {
                    return;
                }
                Thread.Sleep(20);
            }
        }

        public static void DeleteIfExisting(string directory, string pattern)
        {
            if (!Directory.Exists(directory))
            {
                return;
            }
            var files = Directory.GetFiles(directory, pattern);
            for (var i = 0; i < files.Length; i++)
            {
                var path = files[i];
                DeleteIfExisting(path);
            }
        }

        public static string GetName(string path)
        {
            var fileName = Path.GetFileName(path);
            if (fileName == null)
            {
                throw new InvalidOperationException("Could not acquire filename from " + path);
            }
            return fileName;
        }

        public static string GetNameWithoutExtension(string path)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
            if (fileNameWithoutExtension == null)
            {
                throw new InvalidOperationException("Could not acquire filename from " + path);
            }
            return fileNameWithoutExtension;
        }

        public static void MakeWriteable(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                File.SetAttributes(path, FileAttributes.Normal);
            }
        }

        public static void Move(string sourcePath, string targetPath)
        {
            var directoryName = Path.GetDirectoryName(targetPath);
            if (directoryName == null)
            {
                return;
            }
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            File.Move(sourcePath, targetPath);
        }
    }
}
