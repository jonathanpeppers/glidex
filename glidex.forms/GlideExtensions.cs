using System;
using System.IO;
using System.Threading;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Android.Glide
{
	public static class GlideExtensions
	{
		public static void LoadViaGlide (this ImageView imageView, Image image)
		{
			LoadViaGlide (imageView, () => image.Source);
		}

		public static void LoadViaGlide (this ImageView imageView, ImageCell cell)
		{
			LoadViaGlide (imageView, () => cell.ImageSource);
		}

		static async void LoadViaGlide (ImageView imageView, Func<ImageSource> func)
		{
			var source = func ();
			if (source == null) {
				Clear (imageView);
				return;
			}

			RequestManager request = Glide.With (imageView.Context);
			RequestBuilder builder = null;

			if (source is FileImageSource fileSource) {
				var drawable = ResourceManager.GetDrawableByName (fileSource.File);
				if (drawable != 0) {
					builder = request.Load (drawable);
				} else {
					builder = request.Load (fileSource.File);
				}
			} else if (source is UriImageSource uriSource) {
				builder = request.Load (uriSource.Uri.OriginalString);
			} else if (source is StreamImageSource streamSource) {
				var token = new CancellationToken ();
				using (var memoryStream = new MemoryStream ())
				using (var stream = await streamSource.Stream (token)) {
					if (token.IsCancellationRequested || stream == null || source != func ())
						return;
					stream.CopyTo (memoryStream);
					builder = request.Load (memoryStream.ToArray ());
				}
			}

			if (builder != null) {
				imageView.Visibility = ViewStates.Visible;
				builder.Into (imageView);
			} else {
				Clear (imageView);
			}
		}

		static void Clear (ImageView imageView)
		{
			imageView.Visibility = ViewStates.Gone;
			imageView.SetImageBitmap (null);
		}
	}
}