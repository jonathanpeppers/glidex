﻿using System.Threading;
using System.Threading.Tasks;
using Android.Runtime;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportImageSourceHandler (typeof (FileImageSource), typeof (Android.Glide.ImageViewHandler))]
[assembly: ExportImageSourceHandler (typeof (StreamImageSource), typeof (Android.Glide.ImageViewHandler))]
[assembly: ExportImageSourceHandler (typeof (UriImageSource), typeof (Android.Glide.ImageViewHandler))]

namespace Android.Glide
{
	[Preserve (AllMembers = true)]
	public class ImageViewHandler : IImageViewHandler
	{
		public ImageViewHandler ()
		{
			Forms.Debug ("IImageViewHandler of type `{0}`, instance created.", GetType ());
		}

		public async Task LoadImageAsync (ImageSource source, ImageView imageView, CancellationToken token = default (CancellationToken))
		{
			Forms.Debug ("IImageViewHandler of type `{0}`, `{1}` called.", GetType (), nameof (LoadImageAsync));
			await imageView.LoadViaGlide (source, token);
		}
	}
}