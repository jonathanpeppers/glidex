using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public static class Images
	{
		static readonly Random random = new Random ();
		static readonly string tempDir = Path.Combine (Path.GetTempPath (), "glide.forms.sample");
		const int MaxImages = 12;

		public static IEnumerable<ImageSource> RandomSources ()
		{
			for (int i = 0; i < 100; i++) {
				yield return RandomSource ();
			}
		}

		public static ImageSource RandomSource ()
		{
			return SourceById (random.Next (MaxImages));
		}

		public static ImageSource SourceById (int x)
		{
			switch (x) {
				//Urls
				case 0:
					return ImageSource.FromUri (new Uri ("https://growtix-melupufoagt.stackpathdns.com/media/big//34/28/23/5a9f03c2-3734-4486-bd04-2d45ac1c102e.png"));
				case 1:
					return ImageSource.FromUri (new Uri ("https://www.biography.com/.image/t_share/MTgwNTA0NTc3NjY0MDk5ODAx/gettyimages-1128793071-copy.jpg"));
				case 2:
					return ImageSource.FromUri (new Uri ("https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cg_face%2Cq_80%2Cw_300/MTIwNjA4NjM0MDQyNzQ2Mzgw/hulk-hogan-9542305-1-402.jpg"));
				//Resources
				case 3:
				case 4:
				case 5:
				case 6:
					return ImageSource.FromFile ("patch" + (x - 2));
				//File path
				case 7:
					return ImageSource.FromFile (CopyToTempFile ("assetpatch1.jpg"));
				//Stream
				case 8:
					return ImageSource.FromStream (() => Android.App.Application.Context.Assets.Open ("assetpatch2.jpg"));
				//Embedded Resource
				case 9:
					return ImageSource.FromResource ("Android.Glide.Sample.embeddedpatch1.jpg", typeof (App));
				case 10:
					return ImageSource.FromResource ("Android.Glide.Sample.back.png", typeof (App));
				case 11:
					return ImageSource.FromFile ("back.png");
				default:
					throw new NotImplementedException ($"Whoops {x} not implemented!");
			}
		}

		/// <summary>
		/// Copies an AndroidAsset to a temp file and returns the full path to it
		/// </summary>
		public static string CopyToTempFile (string assetName, string destinationName = "")
		{
			if (string.IsNullOrEmpty (destinationName))
				destinationName = assetName;
			string tempPath = Path.Combine (tempDir, destinationName);
			if (!File.Exists (tempPath)) {
				Directory.CreateDirectory (tempDir);
				using var assetStream = Android.App.Application.Context.Assets.Open (assetName);
				using var fileStream = File.Create (tempPath);
				assetStream.CopyTo (fileStream);
			}
			return tempPath;
		}

		public static IEnumerable<ImageSource> GetPicsumImages (int count)
		{
			var width = (int) (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);
			var url = $"https://picsum.photos/{width}/200";

			for (int i = 0; i < count; i++) {
				yield return ImageSource.FromUri (new Uri ($"{url}?{random.Next ()}"));
			}
		}
	}
}
