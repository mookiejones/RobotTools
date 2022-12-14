/*
 * Created by SharpDevelop.
 * User: cberman
 * Date: 11/14/2012
 * Time: 11:10 AM
 * 
 */

using System;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;

namespace miRobotEditor.EditorControl.Classes
{
	public sealed class BracketHighlightRenderer : IBackgroundRenderer
	{
		BracketSearchResult _result;
		Pen _borderPen;
		Brush _backgroundBrush;
	    readonly TextView _textView;

	    private static readonly Color DefaultBackground = Color.FromArgb(100, 0, 0, 255);

	    public void SetHighlight(BracketSearchResult result)
		{
		    if (_result == result) return;
		    _result = result;
		    _textView.InvalidateLayer(Layer);
		}
		
		public BracketHighlightRenderer(TextView textView)
		{
			if (textView == null)
				throw new ArgumentNullException("textView");
			
			_textView = textView;
			
			_textView.BackgroundRenderers.Add(this);
		}

		void UpdateColors(Color background, Color foreground)
		{
			_borderPen = new Pen(new SolidColorBrush(foreground), 1);
			_borderPen.Freeze();

			_backgroundBrush = new SolidColorBrush(background);
			_backgroundBrush.Freeze();
		}
		
		public KnownLayer Layer {
			get {
				return KnownLayer.Selection;
			}
		}
		
		public void Draw(TextView textview, DrawingContext drawingContext)
		{
			if (_result == null)
				return;
			
			var builder = new BackgroundGeometryBuilder {CornerRadius = 1, AlignToMiddleOfPixels = true};

		    builder.AddSegment(textview, new TextSegment { StartOffset = _result.OpeningBracketOffset, Length = _result.OpeningBracketLength });
			builder.CloseFigure(); // prevent connecting the two segments
			builder.AddSegment(textview, new TextSegment { StartOffset = _result.ClosingBracketOffset, Length = _result.ClosingBracketLength });
			
			var geometry = builder.CreateGeometry();
			
			if (_borderPen == null)
				UpdateColors(DefaultBackground,DefaultBackground);
			
			if (geometry != null) 
				drawingContext.DrawGeometry(_backgroundBrush, _borderPen, geometry);
		}
		
/*
		public static void ApplyCustomizationsToRendering(BracketHighlightRenderer renderer, IEnumerable<Color> customizations)
		{
			renderer.UpdateColors(DefaultBackground, DefaultBorder);
			foreach (var color in customizations) {
				//if (color.Name == BracketHighlight) {
				renderer.UpdateColors(color,color);
//					renderer.UpdateColors(color.Background ?? Colors.Blue, color.Foreground ?? Colors.Blue);
					// 'break;' is necessary because more specific customizations come first in the list
					// (language-specific customizations are first, followed by 'all languages' customizations)
					break;
				//}
			}
		}
*/
	}
}
