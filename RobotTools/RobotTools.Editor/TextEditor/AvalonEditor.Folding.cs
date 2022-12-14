using System.IO;
using System.Linq;
using ICSharpCode.AvalonEdit.Folding;
using RobotTools.Editor.TextEditor.Folding;
using RobotTools.Editor.TextEditor.Options;

namespace RobotTools.Editor.TextEditor
{
    public partial class AvalonEditor
    {
        
        private FoldingManager _foldingManager;
        private object _foldingStrategy;


        

        private void ToggleFolds()
        {
            if (_foldingManager != null)
            {
                var foldingSection =
                    _foldingManager.GetNextFolding(TextArea.Document.GetOffset(TextArea.Caret.Line,
                        TextArea.Caret.Column));
                if (foldingSection == null ||
                    Document.GetLineByOffset(foldingSection.StartOffset).LineNumber != TextArea.Caret.Line)
                {
                    foldingSection = _foldingManager.GetFoldingsContaining(TextArea.Caret.Offset).LastOrDefault();
                }
                if (foldingSection != null)
                {
                    foldingSection.IsFolded = !foldingSection.IsFolded;
                }
            }
        }


        private void ToggleAllFolds()
        {
            if (_foldingManager != null)
            {
                foreach (var current in _foldingManager.AllFoldings)
                {
                    current.IsFolded = !current.IsFolded;
                }
            }
        }


        
        private void UpdateFolds()
        {
            var editorOptions = Options as EditorOptions?? new EditorOptions();
            var flag = editorOptions != null && editorOptions.EnableFolding;
            if (SyntaxHighlighting == null)
            {
                _foldingStrategy = null;
            }
            if (File.Exists(Filename))
            {
                if (Path.GetExtension(Filename) == ".xml" || Path.GetExtension(Filename) == ".cfg")
                {
                    _foldingStrategy = new XmlFoldingStrategy();
                }
                else
                {

                    _foldingStrategy = new KukaRegionFoldingStrategy();
                    
                }
                if (_foldingStrategy != null && flag)
                {
                    if (_foldingManager == null)
                    {
                        _foldingManager = FoldingManager.Install(TextArea);
                    }

                    var xmlStrategy = _foldingStrategy as XmlFoldingStrategy;
                    if (xmlStrategy != null)
                    {
                        xmlStrategy.UpdateFoldings(_foldingManager, Document);
                    }
                    else
                    {
                        ((AbstractFoldingStrategy)_foldingStrategy).UpdateFoldings(_foldingManager, Document);
                    }

                    RegisterFoldTitles();
                }
                else
                {
                    if (_foldingManager != null)
                    {
                        FoldingManager.Uninstall(_foldingManager);
                        _foldingManager = null;
                    }
                }
            }
            else
            {
                _foldingStrategy = new KukaRegionFoldingStrategy();
                if (_foldingStrategy != null && flag)
                {
                    if (_foldingManager == null)
                    {
                        _foldingManager = FoldingManager.Install(TextArea);
                    }

                    var xmlStrategy = _foldingStrategy as XmlFoldingStrategy;
                    if (xmlStrategy != null)
                    {
                        xmlStrategy.UpdateFoldings(_foldingManager, Document);
                    }
                    else
                    {
                        ((AbstractFoldingStrategy)_foldingStrategy).UpdateFoldings(_foldingManager, Document);
                    }

                    RegisterFoldTitles();

                }
                else
                {
                    if (_foldingManager != null)
                    {
                        FoldingManager.Uninstall(_foldingManager);
                        _foldingManager = null;
                    }
                }
            }

            ChangeFoldStatus(true);
        }

        private void RegisterFoldTitles()
        {
            if ( Path.GetExtension(Filename) != ".xml")
            {
                foreach (var current in _foldingManager.AllFoldings)
                {
                    current.Title = KukaRegionFoldingStrategy.FoldTitle(current, Document);
                }
            }
        }



    }
}
