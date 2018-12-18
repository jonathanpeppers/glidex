using Android.Content;
using Android.Graphics;
using Xamarin.Forms;

[assembly: ExportRenderer (typeof (ImageButton), typeof (Android.Glide.ImageButtonRenderer))]

namespace Android.Glide
{
	public class ImageButtonRenderer : Xamarin.Forms.Platform.Android.ImageButtonRenderer
	{
		public ImageButtonRenderer (Context context) : base (context)
		{
			//HACK: workaround until https://github.com/xamarin/Xamarin.Forms/pull/4542 is released
			// See https://github.com/jonathanpeppers/glidex/issues/16
			Tag = null;
		}

		/// <summary>
		/// HACK: workaround until https://github.com/xamarin/Xamarin.Forms/pull/4783 is released
		/// </summary>
		public override void Draw (Canvas canvas)
		{
			if (Drawable == null)
				return;

			base.Draw (canvas);
		}
	}
}