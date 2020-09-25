using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class MainPage : ContentPage
	{
		readonly List<ValueTuple<string, Func<Page>>> pages = new List<ValueTuple<string, Func<Page>>> {
			( "Grid Example", () => new GridPage () ),
			( "Edge Cases", () => new EdgeCasesPage () ),
			( "ViewCells", () => new ViewCellPage () ),
			( "ImageCells", () => new ImageCellPage () ),
			( "Huge Images", () => new HugeImagePage () ),
			( "Toggle Images", () => new ToggleSourcePage () ),
			( "Toggle Images Material", () => new ToggleSourcePage { Visual = VisualMarker.Material } ),
			( "Images Should Match", () => new MatchPage () ),
			( "ListView Of Doom", () => new ListViewOfDoom () ),
			( "CollectionView Of Doom", () => new CollectionViewOfDoom () ),
			( "FlexLayout Image Buttons", () => new FlexLayoutImageButtonPage () ),
			( "VisualStateManager", () => new VisualStateManagerPage () ),
		};

		public MainPage ()
		{
			InitializeComponent ();

			BindingContext = pages.Select (p => p.Item1).ToArray ();
		}

		async void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			if (0 <= e.SelectedItemIndex && e.SelectedItemIndex < pages.Count) {
				var (title, callback) = pages [e.SelectedItemIndex];
				var page = callback ();
				if (string.IsNullOrEmpty (page.Title))
					page.Title = title;
				await Navigation.PushAsync (page);
				((ListView) sender).SelectedItem = null;
			}
		}
	}
}
