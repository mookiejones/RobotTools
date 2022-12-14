using System;
using System.Collections.Generic;
using miRobotEditor.Core.Interfaces;
using miRobotEditor.EditorControl.Interfaces;

namespace miRobotEditor.EditorControl.Classes
{
    public class DefaultFormattingStrategy : IFormattingStrategy
    {
        internal static readonly DefaultFormattingStrategy DefaultInstance = new DefaultFormattingStrategy();
		
        public virtual void FormatLine(ITextEditor editor, char charTyped)
        {
        }
		
        public virtual void IndentLine(ITextEditor editor, IDocumentLine line)
        {
            var document = editor.Document;
            var lineNumber = line.LineNumber;
            if (lineNumber <= 1) return;
            document.GetLine(lineNumber - 1);
            throw new NotImplementedException();
            //string indentation = DocumentUtilitites.GetWhitespaceAfter(document, previousLine.Offset);
            // copy indentation to line
            //string newIndentation = DocumentUtilitites.GetWhitespaceAfter(document, line.Offset);
            //document.Replace(line.Offset, newIndentation.Length, indentation);
        }
		
        public virtual void IndentLines(ITextEditor editor, int begin, int end)
        {
            using (editor.Document.OpenUndoGroup()) {
                for (var i = begin; i <= end; i++) {
                    IndentLine(editor, editor.Document.GetLine(i));
                }
            }
        }
		
        public virtual void SurroundSelectionWithComment(ITextEditor editor)
        {
        }
		
        /// <summary>
        /// Default implementation for single line comments.
        /// </summary>
        protected void SurroundSelectionWithSingleLineComment(ITextEditor editor, string comment)
        {
            using (editor.Document.OpenUndoGroup()) {
                var startPosition = editor.Document.OffsetToPosition(editor.SelectionStart);
                var endPosition = editor.Document.OffsetToPosition(editor.SelectionStart + editor.SelectionLength);
				
                // endLine is one above endPosition if no characters are selected on the last line (e.g. line selection from the margin)
                var endLine = (endPosition.Column == 1 && endPosition.Line > startPosition.Line) ? endPosition.Line - 1 : endPosition.Line;
				
                var lines = new List<IDocumentLine>();
                var removeComment = true;
				
                for (var i = startPosition.Line; i <= endLine; i++) {
                    lines.Add(editor.Document.GetLine(i));
                    if (!lines[i - startPosition.Line].Text.Trim().StartsWith(comment, StringComparison.Ordinal))
                        removeComment = false;
                }
				
                foreach (var line in lines) {
                    if (removeComment) {
                        editor.Document.Remove(line.Offset + line.Text.IndexOf(comment, StringComparison.Ordinal), comment.Length);
                    } else {
                        editor.Document.Insert(line.Offset, comment, AnchorMovementType.BeforeInsertion);
                    }
                }
            }
        }
		
        /// <summary>
        /// Default implementation for multiline comments.
        /// </summary>
        protected void SurroundSelectionWithBlockComment(ITextEditor editor, string blockStart, string blockEnd)
        {
            using (editor.Document.OpenUndoGroup()) {
                var startOffset = editor.SelectionStart;
                var endOffset = editor.SelectionStart + editor.SelectionLength;
				
                if (editor.SelectionLength == 0) {
                    var line = editor.Document.GetLineForOffset(editor.SelectionStart);
                    startOffset = line.Offset;
                    endOffset = line.Offset + line.Length;
                }
				
                var region = FindSelectedCommentRegion(editor, blockStart, blockEnd);
				
                if (region != null) {
                    editor.Document.Remove(region.EndOffset, region.CommentEnd.Length);
                    editor.Document.Remove(region.StartOffset, region.CommentStart.Length);
                } else {
                    editor.Document.Insert(endOffset, blockEnd);
                    editor.Document.Insert(startOffset, blockStart);
                }
            }
        }
		
        public static BlockCommentRegion FindSelectedCommentRegion(ITextEditor editor, string commentStart, string commentEnd)
        {
            var document = editor.Document;
			
            if (document.TextLength == 0) {
                return null;
            }
			
            // Find start of comment in selected text.

            var selectedText = editor.SelectedText;
			
            var commentStartOffset = selectedText.IndexOf(commentStart, StringComparison.Ordinal);
            if (commentStartOffset >= 0) {
                commentStartOffset += editor.SelectionStart;
            }

            // Find end of comment in selected text.
			
            var commentEndOffset = commentStartOffset >= 0 ? selectedText.IndexOf(commentEnd, commentStartOffset + commentStart.Length - editor.SelectionStart, StringComparison.Ordinal) : selectedText.IndexOf(commentEnd, StringComparison.Ordinal);
			
            if (commentEndOffset >= 0) {
                commentEndOffset += editor.SelectionStart;
            }
			
            // Find start of comment before or partially inside the
            // selected text.

            if (commentStartOffset == -1) {
                var offset = editor.SelectionStart + editor.SelectionLength + commentStart.Length - 1;
                if (offset > document.TextLength) {
                    offset = document.TextLength;
                }
                var text = document.GetText(0, offset);
                commentStartOffset = text.LastIndexOf(commentStart, StringComparison.Ordinal);
                if (commentStartOffset >= 0)
                {
                    // Find end of comment before comment start.
                    var commentEndBeforeStartOffset = text.IndexOf(commentEnd, commentStartOffset, editor.SelectionStart - commentStartOffset, StringComparison.Ordinal);
                    if (commentEndBeforeStartOffset > commentStartOffset) {
                        commentStartOffset = -1;
                    }
                }
            }
			
            // Find end of comment after or partially after the
            // selected text.
			
            if (commentEndOffset == -1) {
                var offset = editor.SelectionStart + 1 - commentEnd.Length;
                if (offset < 0) {
                    offset = editor.SelectionStart;
                }
                var text = document.GetText(offset, document.TextLength - offset);
                commentEndOffset = text.IndexOf(commentEnd, StringComparison.Ordinal);
                if (commentEndOffset >= 0) {
                    commentEndOffset += offset;
                }
            }
			
            if (commentStartOffset != -1 && commentEndOffset != -1) {
                return new BlockCommentRegion(commentStart, commentEnd, commentStartOffset, commentEndOffset);
            }
			
            return null;
        }
    }
}