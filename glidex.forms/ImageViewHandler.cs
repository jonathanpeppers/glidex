using System.Threading;
using System.Threading.Tasks;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportImageSourceHandler (typeof (FileImageSource), typeof (Android.Glide.ImageViewHandler))]
[assembly: ExportImageSourceHandler (typeof (StreamImageSource), typeof (Android.Glide.ImageViewHandler))]
[assembly: ExportImageSourceHandler (typeof (UriImageSource), typeof (Android.Glide.ImageViewHandler))]

namespace Android.Glide
{
	public class ImageViewHandler : IImageViewHandler
	{
		public Task LoadImageAsync (ImageSource source, ImageView imageView, CancellationToken token = default (CancellationToken))
		{
			imageView.LoadViaGlide (source, token);
			return Task.FromResult (true);
		}
	}
}