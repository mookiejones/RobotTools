// Background Renderer used to Color the Current line

using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;

namespace miRobotEditor.EditorControl.Classes
{
    /// <summary>
    /// Used for Highlighting Background
    /// </summary>
    [DebuggerStepThrough]
    public sealed class BackgroundRenderer : IBackgroundRenderer
    {

        private readonly DocumentLine _line;

        public BackgroundRenderer(DocumentLine line)
        {
            _line = line;
        }

        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            textView.EnsureVisualLines();

            if (_line.IsDeleted) return;
            var segment = new TextSegment { StartOffset = _line.Offset, EndOffset = _line.EndOffset };
            foreach (var r in BackgroundGeometryBuilder.GetRectsForSegment(textView, segment))
            {
                drawingContext.DrawRoundedRectangle(new SolidColorBrush( EditorOptions.Instance.HighlightedLineColor), new Pen(Brushes.Red, 0),new Rect(r.Location, new Size(textView.ActualWidth, r.Height)), 3, 3);
            }
        }

// ReSharper disable UnusedAutoPropertyAccessor.Local
        public KnownLayer Layer { get; private set; }
// ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}
