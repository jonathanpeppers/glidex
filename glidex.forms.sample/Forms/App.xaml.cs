using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage (new MainPage ());
		}
	}
}
