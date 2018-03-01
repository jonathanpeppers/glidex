using System.IO;
using System.Threading;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Android.Glide
{
	public static class GlideExtensions
	{
		public static async void LoadViaGlide(this ImageView imageView, Image element)
		{
			var source = element.Source;
			if (source == null) {
				imageView.SetImageBitmap (null);
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
					if (token.IsCancellationRequested || stream == null || source != element.Source)
						return;
					stream.CopyTo (memoryStream);
					builder = request.Load (memoryStream.ToArray ());
				}
			}

			builder?.Into (imageView);
		}
	}
}