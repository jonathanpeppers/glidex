using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public static class Images
	{
		static readonly Random random = new Random ();
		const int MaxImages = 7;

		public static IEnumerable<ImageSource> RandomSources ()
		{
			var random = new Random ();
			for (int i = 0; i < 100; i++) {
				yield return RandomSource ();
			}
		}

		public static ImageSource RandomSource ()
		{
			int x = random.Next (MaxImages);
			switch (x) {
				case 0:
					return ImageSource.FromUri (new Uri ("https://botlist.co/system/BotList/Bot/logos/000/002/271/medium/chuck_norris.jpg"));
				case 1:
					return ImageSource.FromUri (new Uri ("https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Steven_Seagal_November_2016.jpg/230px-Steven_Seagal_November_2016.jpg"));
				case 2:
					return ImageSource.FromUri (new Uri ("https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cg_face%2Cq_80%2Cw_300/MTIwNjA4NjM0MDQyNzQ2Mzgw/hulk-hogan-9542305-1-402.jpg"));
				case 3:
				case 4:
				case 5:
				case 6:
					return ImageSource.FromFile ("patch" + (x - 2));
				default:
					throw new NotImplementedException ($"Whoops {x} not implemented!");
			}
		}
	}
}