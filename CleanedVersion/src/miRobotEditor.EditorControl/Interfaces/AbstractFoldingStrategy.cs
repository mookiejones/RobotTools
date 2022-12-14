using System.Collections.Generic;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;

namespace miRobotEditor.EditorControl.Interfaces
{
   public abstract class AbstractFoldingStrategy
    {

       public void UpdateFoldings(FoldingManager manager, TextDocument document)
       {
            int firstErrorOffset;
                var newFoldings = CreateNewFoldings(document, out firstErrorOffset);
                manager.UpdateFoldings(newFoldings, firstErrorOffset);
       }

       public abstract IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOfffset);
    }
}
