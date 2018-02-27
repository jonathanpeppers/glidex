using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Glide;
using Android.Glide.Request;

namespace Android.Glide.Sample
{
	[Activity (Label = "glidex.sample", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Main);

			var image = FindViewById<ImageView> (Resource.Id.testImage);
			Glide.With (this)
				.Load ("https://botlist.co/system/BotList/Bot/logos/000/002/271/medium/chuck_norris.jpg")
				.Apply (RequestOptions.CircleCropTransform ().Placeholder (Android.Resource.Drawable.IcMenuCamera))
				.Into (image);
		}
	}
}

