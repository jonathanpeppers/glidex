using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class CollectionViewOfDoom : ContentPage
	{
		public CollectionViewOfDoom ()
		{
			InitializeComponent ();

			BindingContext = GetImageUrls (100).ToArray ();
		}

		readonly Random random = new Random ();

		IEnumerable<ImageSource> GetImageUrls (int count)
		{
			var width = (int)(DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);
			var url = $"https://picsum.photos/{width}/200";

			for (int i = 0; i < count; i++) {
				yield return ImageSource.FromUri (new Uri ($"{url}?{random.Next()}"));
			}
		}
	}
}