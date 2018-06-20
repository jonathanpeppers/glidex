using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class GridPage
	{
		TapGestureRecognizer gesture;

		public GridPage ()
		{
			InitializeComponent ();

			gesture = new TapGestureRecognizer ();
			gesture.Command = new Command (async () => {
				await DisplayAlert ("Hey!", "Gestures work, that's great!", "Ok");
			});

			for (int i = 0; i < 100; i++) {
				_grid.RowDefinitions.Add (new RowDefinition { Height = 50 });

				for (int j = 0; j < 4; j++) {
					var image = new Image {
						Source = Images.RandomSource (),
					};
					image.GestureRecognizers.Add (gesture);
					Grid.SetRow (image, i);
					Grid.SetColumn (image, j);
					_grid.Children.Add (image);
				}
			}
		}
	}
}