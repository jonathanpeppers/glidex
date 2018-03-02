using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class GridPage
	{
		public GridPage ()
		{
			InitializeComponent ();

			for (int i = 0; i < 100; i++) {
				_grid.RowDefinitions.Add (new RowDefinition { Height = 50 });

				for (int j = 0; j < 4; j++) {
					var image = new Image {
						Source = Images.RandomSource (),
					};
					Grid.SetRow (image, i);
					Grid.SetColumn (image, j);
					_grid.Children.Add (image);
				}
			}
		}
	}
}