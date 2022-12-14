using System;
using System.Collections.Generic;
using miRobotEditor.EditorControl.Enums;
using miRobotEditor.EditorControl.Interfaces;

namespace miRobotEditor.EditorControl.Classes
{
    //TODO UNUSED
// ReSharper disable UnusedMember.Global
    public class DefaultCompletionItemList : ICompletionItemList
    {
        readonly List<ICompletionItem> _items = new List<ICompletionItem>();

        public List<ICompletionItem> Items
        {
            get { return _items; }
        }

        /// <inheritdoc />
        public virtual bool ContainsAllAvailableItems
        {
            get { return true; }
        }

        /// <summary>
        /// Sorts the items by their text.
        /// </summary>
        public void SortItems()	// PERF this is called twice
        {
            // the user might use method names is his language, so sort using CurrentCulture
            _items.Sort((a, b) =>
                {
                    var r = string.Compare(a.Text, b.Text, StringComparison.CurrentCultureIgnoreCase);
                    return r != 0 ? r : string.Compare(a.Text, b.Text, StringComparison.CurrentCulture);
                });
        }

        /// <inheritdoc/>
        public int PreselectionLength { get; set; }

        /// <inheritdoc/>
        public ICompletionItem SuggestedItem { get; set; }

        IEnumerable<ICompletionItem> ICompletionItemList.Items
        {
            get { return _items; }
        }

        /// <summary>
        /// Allows the insertion of a single space in front of the completed text.
        /// </summary>
        public bool InsertSpace { get; set; }

        /// <inheritdoc/>
        public virtual CompletionItemListKeyResult ProcessInput(char key)
        {
            if (key == ' ' && InsertSpace)
            {
                InsertSpace = false; // insert space only once
                return CompletionItemListKeyResult.BeforeStartKey;
            }
            if (char.IsLetterOrDigit(key) || key == '_')
            {
                InsertSpace = false; // don't insert space if user types normally
                return CompletionItemListKeyResult.NormalKey;
            }
            // do not reset insertSpace when doing an insertion!
            return CompletionItemListKeyResult.InsertionKey;
        }

        /// <inheritdoc/>
        public virtual void Complete(CompletionContext context, ICompletionItem item)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (item == null)
                throw new ArgumentNullException("item");
            if (InsertSpace)
            {
                InsertSpace = false;
                context.Editor.Document.Insert(context.StartOffset, " ");
                context.StartOffset++;
                context.EndOffset++;
            }
            item.Complete(context);
        }
    }
    // ReSharper restore UnusedMember.Global

}