using System.Linq;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class ImageCellPage
	{
		public ImageCellPage ()
		{
			InitializeComponent ();

			BindingContext = Images.RandomSources ().ToArray ();
		}
	}
}