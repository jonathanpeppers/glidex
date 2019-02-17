using Android.Views;
using Android.Widget;
using Square.Picasso;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using P = Square.Picasso.Picasso;

namespace Picasso
{
	public static class PicassoExtensions
	{
		public static Task LoadViaPicasso (this ImageView imageView, ImageSource source, CancellationToken token)
		{
			try {
				if (source is null) {
					Forms.Debug ("`{0}` is null, clearing image", nameof (ImageSource));
					Clear (imageView);
					return Task.FromResult (true);
				}

				var picasso = P.With (imageView.Context);
				if (Forms.IsDebugEnabled)
					picasso.LoggingEnabled = true;
				RequestCreator request = null;

				switch (source) {
					case FileImageSource fileSource:
						var fileName = fileSource.File;
						var drawable = ResourceManager.GetDrawableByName (fileName);
						if (drawable != 0) {
							Forms.Debug ("Loading `{0}` as an Android resource", fileName);
							request = picasso.Load (drawable);
						} else {
							Forms.Debug ("Loading `{0}` from disk", fileName);
							request = picasso.Load (new Java.IO.File (fileName));
						}
						break;

					case UriImageSource uriSource:
						var url = uriSource.Uri.OriginalString;
						Forms.Debug ("Loading `{0}` as a web URL", url);
						request = picasso.Load (Android.Net.Uri.Parse (url));
						break;

					case StreamImageSource streamSource:
						Forms.Debug ("TODO `{0}` not implemented!", nameof (StreamImageSource));
						break;
				}

				if (request is null) {
					Clear (imageView);
				} else {
					imageView.Visibility = ViewStates.Visible;
					request.Into (imageView);
				}

			} catch (Exception exc) {
				//Since developers can't catch this themselves, I think we should log it and silently fail
				Forms.Warn ("Unexpected exception in picasso: {0}", exc);
			}

			return Task.FromResult (true);
		}

		static void Clear (ImageView imageView)
		{
			imageView.Visibility = ViewStates.Gone;
			imageView.SetImageBitmap (null);
		}
	}
}