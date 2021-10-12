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

			BindingContext = Images.GetPicsumImages (100).ToArray ();
		}
	}
}
