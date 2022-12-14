﻿using System;
using System.ComponentModel;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace RobotTools.Editor.TextEditor.Completion
{
    public sealed class CodeCompletion : ICompletionData
    {
        private string _description = string.Empty;

        //public CodeCompletion(IVariable variable)
        //{
        //    Text = variable.Name;
        //    Image = variable.Icon;
        //    Description = variable.Description;
        //}

        [Localizable(false)]
        public CodeCompletion(string text)
        {
            Text = text;
        }

        public ImageSource Image { get; set; }
        public string Text { get; private set; }

        public object Content => Text;

        [Localizable(false)]
        public object Description
        {
            get =>
                string.IsNullOrEmpty(_description)
                    ? null
                    : string.Format("Description for {0} \r\n {1}", Text, _description);
            set => _description = (string)value;
        }

        public double Priority => 0.0;

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            //var instance = ServiceLocator.Current.GetInstance<MainViewModel>();
            //var text = instance.ActiveEditor.TextBox.FindWord();
            //var offset = completionSegment.Offset - text.Length;
            //textArea.Document.Replace(offset, text.Length, Text);
        }
    }
}
