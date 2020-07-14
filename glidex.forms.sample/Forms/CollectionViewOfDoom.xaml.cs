using System;
using System.Collections.Generic;
using System.Linq;
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
		const string Url = "https://img.resized.co/irishpostcouk/eyJkYXRhIjoie1widXJsXCI6XCJodHRwczpcXFwvXFxcL3MzLWV1LXdlc3QtMS5hbWF6b25hd3MuY29tXFxcL3N0b3JhZ2UucHVibGlzaGVycGx1cy5pZVxcXC9tZWRpYS5pcmlzaHBvc3QuY28udWtcXFwvdXBsb2Fkc1xcXC8yMDE5XFxcLzA3XFxcLzA0MTE0OTQ5XFxcL0RvbmFsZC1UcnVtcC1XV0UtSXJpc2gtUG9zdC5wbmdcIixcIndpZHRoXCI6NzAwLFwiaGVpZ2h0XCI6MzcwLFwiZGVmYXVsdFwiOlwiaHR0cHM6XFxcL1xcXC93d3cuaXJpc2hwb3N0LmNvbVxcXC9pXFxcL25vLWltYWdlLnBuZ1wifSIsImhhc2giOiJjMTAzMTQ1NDk1YzY2NGMwM2ZlMTc2ZmY5MzI1YmY4NTc4MWVkOGFiIn0=/donald-trump-wwe-irish-post.png?rand=";

		IEnumerable<ImageSource> GetImageUrls (int count)
		{
			for (int i = 0; i < count; i++) {
				yield return ImageSource.FromUri (new Uri (Url + random.Next ()));
			}
		}
	}
}
