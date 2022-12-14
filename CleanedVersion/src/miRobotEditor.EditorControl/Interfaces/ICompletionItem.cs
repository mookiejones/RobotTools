using miRobotEditor.Core.Interfaces;
using miRobotEditor.EditorControl.Classes;

namespace miRobotEditor.EditorControl.Interfaces
{
    public interface ICompletionItem
    {
        string Text { get; }
        string Description { get; }
        IImage Image { get; }

        /// <summary>
        /// Performs code completion for the item.
        /// </summary>
        void Complete(CompletionContext context);

        /// <summary>
        /// Gets a priority value for the completion data item.
        /// When selecting items by their start characters, the item with the highest
        /// priority is selected first.
        /// </summary>
        double Priority
        {
            get;
        }
    }
}
