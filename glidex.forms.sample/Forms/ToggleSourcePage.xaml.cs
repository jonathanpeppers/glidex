using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Android.Glide.Sample
{
	[XamlCompilation (XamlCompilationOptions.Compile)]
	public partial class ToggleSourcePage : ContentPage
	{
		public ToggleSourcePage ()
		{
			InitializeComponent ();
		}

		private void Button_Clicked (object sender, EventArgs e)
		{
			var source = Images.RandomSource ();
			_image.Source =
				_imageButton.Source = source;
			_label.Text = source.ToString ();
		}
	}
}