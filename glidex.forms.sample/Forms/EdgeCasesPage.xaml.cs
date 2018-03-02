using System;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class EdgeCasesPage
	{
		public EdgeCasesPage ()
		{
			InitializeComponent ();

			_stack.Children.Add (new Image {
				HeightRequest = 50,
				Source = Images.RandomSource (),
			});
			_stack.Children.Add (new Image {
				Source = ImageSource.FromFile ("doesn't exist")
			});
			_stack.Children.Add (new Image {
				Source = ImageSource.FromUri (new Uri ("http://dontexist"))
			});
			_stack.Children.Add (new Image {
				Source = ImageSource.FromUri (new Uri ("http://jonathanpeppers.com/404"))
			});
			_stack.Children.Add (new Image {
				Source = ImageSource.FromResource ("doesn't exist", typeof (App)),
			});
			_stack.Children.Add (new Image {
				Source = ImageSource.FromStream (() => null)
			});
		}
	}
}