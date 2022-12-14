using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace miRobotEditor.Core.Interfaces
{
    /// <summary>
    /// Represents an image.
    /// </summary>
    public interface IImage
    {
        /// <summary>
        /// Gets the image as WPF ImageSource.
        /// </summary>
        ImageSource ImageSource { get; }

        /// <summary>
        /// Gets the image as System.Drawing.Bitmap.
        /// </summary>
        BitmapImage Bitmap { get; }

        /// <summary>
        /// Gets the image as System.Drawing.Icon.
        /// </summary>
        Icon Icon { get; }
    }

//  /// <summary>
//  /// Represents an image that gets loaded from a ResourceService.
//  /// </summary>
//  public class ResourceServiceImage : IImage
//
//   private readonly string resourceName;
//
//   /// <summary>
//   /// Creates a new ResourceServiceImage.
//   /// </summary>
//   /// <param name="resourceName">The name of the image resource.</param>
//   public ResourceServiceImage(string resourceName)
//   {
//       if (resourceName == null)
//           throw new ArgumentNullException("resourceName");
//       this.resourceName = resourceName;
//   }

    //      /// <inheritdoc/>
    //      public ImageSource ImageSource
    //      {
    //          get
    //          {
    //              return PresentationResourceService.GetBitmapSource(resourceName);
    //          }
    //      }
    //
    //      /// <inheritdoc/>
    //      public Bitmap Bitmap
    //      {
    //          get
    //          {
    //              return WinFormsResourceService.GetBitmap(resourceName);
    //          }
    //      }
    //
    //     /// <inheritdoc/>
    //     public Icon Icon
    //     {
    //         get
    //         {
    //             Icon icon = WinFormsResourceService.GetIcon(resourceName);
    //             if (icon == null)
    //                 throw new ResourceNotFoundException(resourceName);
    //             return icon;
    //         }
    //     }
}
//
//
