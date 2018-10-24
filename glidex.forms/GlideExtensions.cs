using System;
using System.IO;
using System.Threading;
using Android.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Android.Glide
{
	public static class GlideExtensions
	{
		const string Tag = "glidex";

		public static async void LoadViaGlide (this ImageView imageView, ImageSource source, CancellationToken token)
		{
			try {
				if (source == null) {
					Clear (imageView);
					return;
				}

				//NOTE: see https://github.com/bumptech/glide/issues/1484#issuecomment-365625087
				var activity = imageView.Context as Activity;
				if (activity != null) {
					if (activity.IsFinishing) {
						Log.Warn (Tag, "Activity of type `{0}` is finishing, aborting image load for `{1}`.", activity.GetType ().FullName, source);
						return;
					}
					if (activity.IsDestroyed) {
						Log.Warn (Tag, "Activity of type `{0}` is destroyed, aborting image load for `{1}`.", activity.GetType ().FullName, source);
						return;
					}
				} else {
					Log.Warn (Tag, "Context `{0}` is not an Android.App.Activity, aborting image load for `{1}`.", imageView.Context, source);
					return;
				}

				RequestManager request = Glide.With (imageView.Context);
				RequestBuilder builder = null;

                switch (source)
                {
                    case FileImageSource fileSource:
                        var drawable = ResourceManager.GetDrawableByName(fileSource.File);
                        if (drawable != 0){
                            builder = request.Load(drawable);
                        }else{
                            builder = request.Load(fileSource.File);
                        }
                        break;

                    case UriImageSource uriSource:
                        builder = request.Load(uriSource.Uri.OriginalString);
                        break;

                    case StreamImageSource streamSource:
                        using (var memoryStream = new MemoryStream())
                        using (var stream = await streamSource.Stream(token)){
                            if (token.IsCancellationRequested || stream == null)
                                return;
                            stream.CopyTo(memoryStream);
                            builder = request.Load(memoryStream.ToArray());
                        }
                        break;
                }

				if (builder is null) {
                    Clear(imageView);
				} else {
                    imageView.Visibility = ViewStates.Visible;
                    builder.Into(imageView);
                }
			} catch (Exception exc) {
				//Since developers can't catch this themselves, I think we should log it and silently fail
				Log.Warn (Tag, "Unexpected exception in glidex: {0}", exc);
			}
		}

		static void Clear (ImageView imageView)
		{
			imageView.Visibility = ViewStates.Gone;
			imageView.SetImageBitmap (null);
		}
	}
}