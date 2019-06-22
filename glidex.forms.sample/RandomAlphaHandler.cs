using System;
using System.Threading;
using Android.Graphics.Drawables;
using Android.Widget;
using Bumptech.Glide;
using Bumptech.Glide.Request.Target;
using Bumptech.Glide.Request.Transition;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public class RandomAlphaHandler : IGlideHandler
	{
		public bool Build (ImageView imageView, ImageSource source, RequestBuilder builder, CancellationToken token)
		{
			if (builder != null) {
				builder.Into (new MyTarget (imageView));
				return true;
			} else {
				return false;
			}
		}

		class MyTarget : SimpleTarget
		{
			static readonly Random rand = new Random();
			readonly ImageView imageView;

			public MyTarget (ImageView imageView)
			{
				this.imageView = imageView;
			}

			public override void OnResourceReady (Java.Lang.Object resource, ITransition transition)
			{
				if (resource is BitmapDrawable bitmap) {
					bitmap.Alpha = rand.Next (0, 255);
					imageView.SetImageDrawable (bitmap);
				}
			}
		}
	}
}
