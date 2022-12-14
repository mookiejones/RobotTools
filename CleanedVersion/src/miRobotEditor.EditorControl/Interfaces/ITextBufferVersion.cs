using System;
using System.Collections.Generic;
using miRobotEditor.Core.Interfaces;

namespace miRobotEditor.EditorControl.Interfaces
{
    /// <summary>
    /// Represents a version identifier for a text buffer.
    /// </summary>
    /// <remarks>
    /// This is SharpDevelop's equivalent to AvalonEdit ChangeTrackingCheckpoint.
    /// It is used by the ParserService to efficiently detect whether a document has changed and needs reparsing.
    /// It is a separate class from ITextBuffer to allow the GC to collect the text buffer while the version checkpoint
    /// is still in use.
    /// </remarks>
    public interface ITextBufferVersion
    {
        /// <summary>
        /// Gets whether this checkpoint belongs to the same document as the other checkpoint.
        /// </summary>
        bool BelongsToSameDocumentAs(ITextBufferVersion other);

        /// <summary>
        /// Compares the age of this checkpoint to the other checkpoint.
        /// </summary>
        /// <remarks>This method is thread-safe.</remarks>
        /// <exception cref="ArgumentException">Raised if 'other' belongs to a different document than this version.</exception>
        /// <returns>-1 if this version is older than <paramref name="other"/>.
        /// 0 if <c>this</c> version instance represents the same version as <paramref name="other"/>.
        /// 1 if this version is newer than <paramref name="other"/>.</returns>
        int CompareAge(ITextBufferVersion other);

        /// <summary>
        /// Gets the changes from this checkpoint to the other checkpoint.
        /// If 'other' is older than this checkpoint, reverse changes are calculated.
        /// </summary>
        /// <remarks>This method is thread-safe.</remarks>
        /// <exception cref="ArgumentException">Raised if 'other' belongs to a different document than this checkpoint.</exception>
        IEnumerable<TextChangeEventArgs> GetChangesTo(ITextBufferVersion other);

        /// <summary>
        /// Calculates where the offset has moved in the other buffer version.
        /// </summary>
        /// <exception cref="ArgumentException">Raised if 'other' belongs to a different document than this checkpoint.</exception>
        int MoveOffsetTo(ITextBufferVersion other, int oldOffset, AnchorMovementType movement);
    }
}