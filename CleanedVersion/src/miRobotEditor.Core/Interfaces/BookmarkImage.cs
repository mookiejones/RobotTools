/*
 * Created by SharpDevelop.
 * User: cberman
 * Date: 9/23/2012
 * Time: 1:14 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace miRobotEditor.Core.Interfaces
{
	/// <summary>
	/// Description of BookmarkImage.
	/// </summary>
	public class BookmarkImage:IImage
	{
		
		private readonly IImage _baseimage = null;
		
		private readonly BitmapImage _bitmap;
		
		public BookmarkImage(BitmapImage bitmap)
		{
			_bitmap = bitmap;
		}
		
		public ImageSource ImageSource {
			get {
				return _baseimage.ImageSource;
			}
		}
		
		public BitmapImage Bitmap {
			get {
			return _bitmap;
			}
		}
		
		public Icon Icon {
			get {
				return _baseimage.Icon;
			}
		}
	}
}
