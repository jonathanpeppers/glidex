using System;
using System.Collections.Generic;
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
						Source = RandomSource (random.Next (MaxImages))
					};
					Grid.SetRow (image, i);
					Grid.SetColumn (image, j);
					grid.Children.Add (image);
				}
			}
			_scroll.Content = grid;
		}

		const int MaxImages = 3;

		IEnumerable<ImageSource> RandomSources ()
		{
			var random = new Random ();
			for (int i = 0; i < 100; i++) {
				yield return RandomSource (random.Next (MaxImages));
			}
		}

		ImageSource RandomSource (int x)
		{
			switch (x) {
				case 0:
					return ImageSource.FromUri (new Uri ("https://botlist.co/system/BotList/Bot/logos/000/002/271/medium/chuck_norris.jpg"));
				case 1:
					return ImageSource.FromUri (new Uri ("https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Steven_Seagal_November_2016.jpg/230px-Steven_Seagal_November_2016.jpg"));
				case 2:
					return ImageSource.FromUri (new Uri ("https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cg_face%2Cq_80%2Cw_300/MTIwNjA4NjM0MDQyNzQ2Mzgw/hulk-hogan-9542305-1-402.jpg"));
				default:
					throw new ArgumentNullException ($"Not implemented {x}!");
			}
		}
	}
}
