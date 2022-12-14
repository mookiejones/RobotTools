/*
 * Created by SharpDevelop.
 * User: cberman
 * Date: 9/22/2012
 * Time: 9:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Utils;
using miRobotEditor.Core.Interfaces;
using miRobotEditor.EditorControl.Classes;
using miRobotEditor.EditorControl.Interfaces;

namespace miRobotEditor.EditorControl
{
    /// <summary>
    /// Icon bar: contains breakpoints and other icons.
    /// </summary>
    public class IconBarMargin : AbstractMargin, IDisposable
    {
        readonly IBookmarkMargin _manager;

        public IconBarMargin(IBookmarkMargin manager)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");
            _manager = manager;
        }

        #region OnTextViewChanged
        
        /// <inheritdoc/>
        protected override void OnTextViewChanged(TextView oldTextView, TextView newTextView)
        {
            if (oldTextView != null)
            {
                oldTextView.VisualLinesChanged -= OnRedrawRequested;
                _manager.RedrawRequested -= OnRedrawRequested;
            }
            base.OnTextViewChanged(oldTextView, newTextView);
            if (newTextView != null)
            {
                newTextView.VisualLinesChanged += OnRedrawRequested;
                _manager.RedrawRequested += OnRedrawRequested;
            }
            InvalidateVisual();
        }
        [DebuggerStepThrough]
        void OnRedrawRequested(object sender, EventArgs e)
        {
            // Don't invalidate the IconBarMargin if it'll be invalidated again once the
            // visual lines become valid.
            if (TextView != null && TextView.VisualLinesValid)
            {
                InvalidateVisual();
            }
        }

        public virtual void Dispose()
        {
            TextView = null; // detach from TextView (will also detach from manager)
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <inheritdoc/>
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            // accept clicks even when clicking on the background
            return new PointHitTestResult(this, hitTestParameters.HitPoint);
        }

        /// <inheritdoc/>
        [DebuggerStepThrough]
        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(18, 0);
        }


        [DebuggerStepThrough]
        protected override void OnRender(DrawingContext drawingContext)
        {
            var renderSize = RenderSize;
            drawingContext.DrawRectangle(SystemColors.ControlBrush, null,
                                         new Rect(0, 0, renderSize.Width, renderSize.Height));
            drawingContext.DrawLine(new Pen(SystemColors.ControlDarkBrush, 1),
                                    new Point(renderSize.Width - 0.5, 0),
                                    new Point(renderSize.Width - 0.5, renderSize.Height));

            var textView = TextView;
            if (textView == null || !textView.VisualLinesValid) return;
            // create a dictionary line number => first bookmark
            var bookmarkDict = new Dictionary<int, IBookmark>();
            foreach (var bm in _manager.Bookmarks)
            {
                var line = bm.LineNumber;
                IBookmark existingBookmark;
                if (!bookmarkDict.TryGetValue(line, out existingBookmark) || bm.ZOrder > existingBookmark.ZOrder)
                    bookmarkDict[line] = bm;
            }
            var pixelSize = PixelSnapHelpers.GetPixelSize(this);
            Rect rect;
            foreach (var line in textView.VisualLines)
            {
                var lineNumber = line.FirstDocumentLine.LineNumber;
                IBookmark bm;

                if (!bookmarkDict.TryGetValue(lineNumber, out bm)) continue;
                var lineMiddle = line.GetTextLineVisualYPosition(line.TextLines[0], VisualYPosition.TextMiddle) - textView.VerticalOffset;
                rect = new Rect(0, PixelSnapHelpers.Round(lineMiddle - 8, pixelSize.Height), 16, 16);
                if (_dragDropBookmark == bm && _dragStarted)
                    drawingContext.PushOpacity(0.5);
                drawingContext.DrawImage((bm.Image ?? BookmarkBase.defaultBookmarkImage).Bitmap, rect);
                if (_dragDropBookmark == bm && _dragStarted)
                    drawingContext.Pop();
            }
            if (_dragDropBookmark == null || !_dragStarted) return;
            rect = new Rect(0, PixelSnapHelpers.Round(_dragDropCurrentPoint - 8, pixelSize.Height), 16, 16);
            drawingContext.DrawImage((_dragDropBookmark.Image ?? BookmarkBase.defaultBookmarkImage).ImageSource, rect);
        }

        IBookmark _dragDropBookmark; // bookmark being dragged (!=null if drag'n'drop is active)
        double _dragDropStartPoint;
        double _dragDropCurrentPoint;
        bool _dragStarted; // whether drag'n'drop operation has started (mouse was moved minimum distance)

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            CancelDragDrop();
            base.OnMouseDown(e);
            var line = GetLineFromMousePosition(e);
            if (!e.Handled && line > 0)
            {
                var bm = GetBookmarkFromLine(line);
                if (bm != null)
                {
                    bm.MouseDown(e);
                    if (!e.Handled)
                    {
                        if (e.ChangedButton == MouseButton.Left && bm.CanDragDrop && CaptureMouse())
                        {
                            StartDragDrop(bm, e);
                            e.Handled = true;
                        }
                    }
                }
            }
            // don't allow selecting text through the IconBarMargin
            if (e.ChangedButton == MouseButton.Left)
                e.Handled = true;
        }

        IBookmark GetBookmarkFromLine(int line)
        {
            IBookmark[] result = {null};
            foreach (var bm in _manager.Bookmarks.Where(bm => bm.LineNumber == line).Where(bm => result[0] == null || bm.ZOrder > result[0].ZOrder))
            {
                result[0] = bm;
            }
            return result[0];
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            CancelDragDrop();
            base.OnLostMouseCapture(e);
        }

        void StartDragDrop(IBookmark bm, MouseEventArgs e)
        {
            _dragDropBookmark = bm;
            _dragDropStartPoint = _dragDropCurrentPoint = e.GetPosition(this).Y;
            if (TextView == null) return;
            var area = TextView.Services.GetService(typeof(TextArea)) as TextArea;
            if (area != null)
                area.PreviewKeyDown += TextAreaPreviewKeyDown;
        }

        void CancelDragDrop()
        {
            if (_dragDropBookmark == null) return;
            _dragDropBookmark = null;
            _dragStarted = false;
            if (TextView != null)
            {
                var area = TextView.Services.GetService(typeof(TextArea)) as TextArea;
                if (area != null)
                    area.PreviewKeyDown -= TextAreaPreviewKeyDown;
            }
            ReleaseMouseCapture();
            InvalidateVisual();
        }

        void TextAreaPreviewKeyDown(object sender, KeyEventArgs e)
        {
            // any key press cancels drag'n'drop
            CancelDragDrop();
            if (e.Key == Key.Escape)
                e.Handled = true;
        }

        int GetLineFromMousePosition(MouseEventArgs e)
        {
            var textView = TextView;
            if (textView == null)
                return 0;
            var vl = textView.GetVisualLineFromVisualTop(e.GetPosition(textView).Y + textView.ScrollOffset.Y);
            return vl == null ? 0 : vl.FirstDocumentLine.LineNumber;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_dragDropBookmark == null) return;
            _dragDropCurrentPoint = e.GetPosition(this).Y;
            if (Math.Abs(_dragDropCurrentPoint - _dragDropStartPoint) > SystemParameters.MinimumVerticalDragDistance)
                _dragStarted = true;
            InvalidateVisual();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            var line = GetLineFromMousePosition(e);
            if (!e.Handled && _dragDropBookmark != null)
            {
                if (_dragStarted)
                {
                    if (line != 0)
                        _dragDropBookmark.Drop(line);
                    e.Handled = true;
                }
                CancelDragDrop();
            }
            if (e.Handled || line == 0) return;
            var bm = GetBookmarkFromLine(line);
            if (bm != null)
            {
                bm.MouseUp(e);
                if (e.Handled)
                    return;
            }
            if (e.ChangedButton != MouseButton.Left || TextView == null) return;
            var textEditor = TextView.Services.GetService(typeof (ITextEditor)) as ITextEditor;
            if (textEditor != null)
            {
            }
            // no bookmark on the line: create a new breakpoint


            //		ITextEditor textEditor = TextView.Services.GetService(typeof(ITextEditor)) as ITextEditor;
            //		if (textEditor != null) {
            //			DebuggerService.ToggleBreakpointAt(textEditor, line, typeof(BreakpointBookmark));
            //			return;
            //		}
            //		
            //		// create breakpoint for the other posible active contents
            //		var viewContent = WorkbenchSingleton.Workbench.ActiveContent as AbstractViewContentWithoutFile;
            //		if (viewContent != null) {
            //			textEditor = viewContent.Services.GetService(typeof(ITextEditor)) as ITextEditor;
            //			if (textEditor != null) {
            //				DebuggerService.ToggleBreakpointAt(textEditor, line, typeof(DecompiledBreakpointBookmark));
            //				return;
        }
    }
}

