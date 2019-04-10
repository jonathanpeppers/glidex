using System;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();

			MainList.ItemsSource = new []
			{
				"Grid Example",
				"Edge Cases",
				"ViewCells",
				"ImageCells",
				"Huge Images",
				"Toggle Images",
				"Toggle Images Material",
				"Images Should Match",
			};
		}

		async void MainList_ItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			switch (e.SelectedItemIndex) {
				case 0:
					await Navigation.PushAsync (new GridPage ());
					break;
				case 1:
					await Navigation.PushAsync (new EdgeCasesPage ());
					break;
				case 2:
					await Navigation.PushAsync (new ViewCellPage ());
					break;
				case 3:
					await Navigation.PushAsync (new ImageCellPage ());
					break;
				case 4:
					await Navigation.PushAsync (new HugeImagePage ());
					break;
				case 5:
					await Navigation.PushAsync (new ToggleSourcePage ());
					break;
				case 6:
					await Navigation.PushAsync (new ToggleSourcePage { Visual = VisualMarker.Material });
					break;
				case 7:
					await Navigation.PushAsync (new MatchPage ());
					break;
				default:
					break;
			}
			MainList.SelectedItem = null;
		}
	}
}
