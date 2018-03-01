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

		async void Grid_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new GridPage ());
		}
		async void Edge_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new EdgeCasesPage ());
		}

		async void ViewCell_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new ViewCellPage ());
		}
	}
}
