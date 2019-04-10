using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class MatchPage : ContentPage
	{
		public MatchPage ()
		{
			InitializeComponent ();

			image1.Source = Images.SourceById (10);
			image2.Source = Images.SourceById (11);
			image3.Source = Images.SourceById (11);
			image4.Source = Images.SourceById (10);
		}
	}
}