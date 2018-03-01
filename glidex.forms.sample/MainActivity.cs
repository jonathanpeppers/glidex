using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Android.Glide.Sample
{
	[Activity (Label = "glidex.forms.sample", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
		//HACK: AppCompat appears to be broken in Forms, see https://developercommunity.visualstudio.com/content/problem/182677/nullreferenceexception-formsappcompatactivityinter.html
		//Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			//TabLayoutResource = Resource.Layout.Tabbar;
			//ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);
			Android.Glide.Forms.Init ();
			//Force the custom renderers to get loaded
			LoadApplication (new App ());
		}
	}
}

