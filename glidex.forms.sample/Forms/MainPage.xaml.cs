using System;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();

			var random = new Random ();

			var grid = new Grid ();
			grid.RowSpacing = 0;
			grid.ColumnSpacing = 0;
			grid.ColumnDefinitions.Add (new ColumnDefinition ());
			grid.ColumnDefinitions.Add (new ColumnDefinition ());
			grid.ColumnDefinitions.Add (new ColumnDefinition ());
			grid.ColumnDefinitions.Add (new ColumnDefinition ());

			for (int i = 0; i < 100; i++) {
				grid.RowDefinitions.Add (new RowDefinition { Height = 50 });

				for (int j = 0; j < 4; j++) {
					var image = new Image {
						Source = Images.RandomSource (),
					};
					Grid.SetRow (image, i);
					Grid.SetColumn (image, j);
					grid.Children.Add (image);
				}
			}
			_scroll.Content = grid;
		}
	}
}
