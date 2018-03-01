using System.Linq;
using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public partial class ViewCellPage : ContentPage
	{
		public ViewCellPage ()
		{
			InitializeComponent ();

			BindingContext = Images.RandomSources ().ToArray ();
		}
	}
}