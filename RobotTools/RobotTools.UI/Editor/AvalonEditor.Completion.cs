using ICSharpCode.AvalonEdit.CodeCompletion;
using RobotTools.UI.Editor.Completion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RobotTools.UI.Editor
{
    public partial class AvalonEditor
    {
        public IList<ICompletionDataProvider> CompletionDataProviders { get; set; }

        public static readonly DependencyProperty CompletionWindowProperty =
            DependencyProperty.Register("CompletionWindow", typeof(CompletionWindow), typeof(Editor));


        private CompletionWindow CompletionWindow
        {
            get { return (CompletionWindow)GetValue(CompletionWindowProperty); }
            set { SetValue(CompletionWindowProperty, value); }
        }

        public IList<ICompletionData> CompletionData
        {
            get; // ReSharper disable once UnusedAutoPropertyAccessor.Local
            private set;
        }

        partial void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
