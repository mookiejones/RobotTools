using System;
using System.ComponentModel;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using miRobotEditor.Core.Interfaces;

namespace miRobotEditor.EditorControl.Classes
{
    /// Implements AvalonEdit ICompletionData interface to provide the entries in the
    /// completion drop down.
    public sealed class CodeCompletion : ICompletionData
    {


        public string CurrentWord { get; set; }

        public CodeCompletion(IVariable variable) 
        {
            Text = variable.Name;
            Image = variable.Icon;
            Description = variable.Description;
        }


        [Localizable(false)]
        public CodeCompletion(string text)
        {
            Text = text;
        }

        public ImageSource Image{get;set;}
        public string Text { get; private set; }
        private string _description = string.Empty;

        // Use this property if you want to show a fancy UIElement in the list.
        public object Content
        {
            get { return Text; }
        }



        [Localizable(false)]
        public object Description { get { return String.IsNullOrEmpty(_description)?null:String.Format("Description for {0} \r\n {1}", Text, _description); } set { _description = (string)value; } }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            var length = CurrentWord.Length;
            var offs = completionSegment.Offset - length;
            // Create New AnchorSegment 
            textArea.Document.Replace(offs, length, Text);
        }

        public double Priority
        {
            get { return 0; }
        }
    }

    #region DefaultClasses

    #endregion

        #region Interfaces

    #endregion
 }

