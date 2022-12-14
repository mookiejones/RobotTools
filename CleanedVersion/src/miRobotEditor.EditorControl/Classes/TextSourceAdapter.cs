using System;
using System.IO;
using ICSharpCode.AvalonEdit.Document;
using miRobotEditor.EditorControl.Interfaces;

namespace miRobotEditor.EditorControl.Classes
{
    public class TextSourceAdapter : ITextBuffer
    {
        internal readonly ITextSource TextSource;

        public TextSourceAdapter(ITextSource textSource)
        {
            if (textSource == null)
                throw new ArgumentNullException("textSource");
            TextSource = textSource;
        }

        public virtual ITextBufferVersion Version
        {
            get { return null; }
        }

        /// <summary>
        /// Creates an immutable snapshot of this text buffer.
        /// </summary>
        public virtual ITextBuffer CreateSnapshot()
        {
            return new TextSourceAdapter(TextSource.CreateSnapshot());
        }

        /// <summary>
        /// Creates an immutable snapshot of a part of this text buffer.
        /// Unlike all other methods in this interface, this method is thread-safe.
        /// </summary>
        public ITextBuffer CreateSnapshot(int offset, int length)
        {
            return new TextSourceAdapter(TextSource.CreateSnapshot(offset, length));
        }

        /// <summary>
        /// Creates a new TextReader to read from this text buffer.
        /// </summary>
        public TextReader CreateReader()
        {
            return TextSource.CreateReader();
        }

        /// <summary>
        /// Creates a new TextReader to read from this text buffer.
        /// </summary>
        public TextReader CreateReader(int offset, int length)
        {
            return TextSource.CreateSnapshot(offset, length).CreateReader();
        }

        public int TextLength
        {
            get { return TextSource.TextLength; }
        }

        public string Text
        {
            get { return TextSource.Text; }
        }

        /// <summary>
        /// Is raised when the Text property changes.
        /// </summary>
        public event EventHandler TextChanged
        {
            add { } //TextSource.TextChanged += value; }
            remove { } // TextSource.TextChanged -= value; }
        }

        public char GetCharAt(int offset)
        {
            return TextSource.GetCharAt(offset);
        }

        public string GetText(int offset, int length)
        {
            return TextSource.GetText(offset, length);
        }
    }
}
