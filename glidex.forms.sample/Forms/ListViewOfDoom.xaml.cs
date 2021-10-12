using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class ListViewOfDoom : ContentPage
	{
		public ListViewOfDoom ()
		{
			InitializeComponent ();

			BindingContext = Images.GetPicsumImages (100).ToArray ();
		}
	}
}
