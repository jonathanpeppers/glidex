using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Android.Glide.Sample
{
    [Activity(Label = "glidex.forms.sample", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate (bundle);

            Xamarin.Forms.Forms.SetFlags ("Visual_Experimental");
            Xamarin.Forms.Forms.Init (this, bundle);
            //Force the custom renderers to get loaded
            Android.Glide.Forms.Init (this, debug: true);
            LoadApplication (new App ());
        }
    }
}

