using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Android.Glide.Sample
{
	[Activity (Label = "glidex.forms.sample", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			//Comment out this line and remove reference to glidex.forms, to see poor XF performance
			Android.Glide.Forms.Init ();

			LoadApplication (new App ());
		}
	}
}

