using Android.Content;
using Android.Views;
using Android.Widget;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportCell (typeof (ImageCell), typeof (Android.Glide.ImageCellRenderer))]

namespace Android.Glide
{
	public class ImageCellRenderer : Xamarin.Forms.Platform.Android.TextCellRenderer
	{
		protected BaseCellView _view;

		protected override Views.View GetCellCore (Cell item, Views.View convertView, ViewGroup parent, Context context)
		{
			_view = (BaseCellView)base.GetCellCore (item, convertView, parent, context);
			UpdateImage ();
			return _view;
		}

		protected override void OnCellPropertyChanged (object sender, PropertyChangedEventArgs args)
		{
			base.OnCellPropertyChanged (sender, args);
			if (args.PropertyName == ImageCell.ImageSourceProperty.PropertyName)
				UpdateImage ();
		}

		protected virtual void UpdateImage ()
		{
			if (_view == null)
				return;

			//HACK: this code depends on XF putting the child at index 0, I saw no other way to grab the ImageView
			var imageView = (ImageView)_view.GetChildAt (0);
			imageView.LoadViaGlide ((ImageCell)Cell);
		}
	}
}