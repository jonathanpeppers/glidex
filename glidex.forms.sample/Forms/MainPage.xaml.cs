using System;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

		private async void Grid_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new GridPage ());
		}
	}
}
