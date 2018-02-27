using Android.Views;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AImageView = Android.Widget.ImageView;
using AView = Android.Views.View;

[assembly: ExportRenderer (typeof (Image), typeof (Android.Glide.ImageRenderer))]

namespace Android.Glide
{
	internal sealed class ImageRenderer : AImageView, IVisualElementRenderer
	{
		bool _disposed;
		Image _element;
		bool _skipInvalidate;
		int? _defaultLabelFor;
		VisualElementTracker _visualElementTracker;

		protected override void Dispose (bool disposing)
		{
			if (_disposed)
				return;

			_disposed = true;

			if (disposing) {
				if (_visualElementTracker != null) {
					_visualElementTracker.Dispose ();
					_visualElementTracker = null;
				}

				if (_element != null) {
					_element.PropertyChanged -= OnElementPropertyChanged;
				}
			}

			base.Dispose (disposing);
		}

		public override void Invalidate ()
		{
			if (_skipInvalidate) {
				_skipInvalidate = false;
				return;
			}

			base.Invalidate ();
		}

		void OnElementChanged (ElementChangedEventArgs<Image> e)
		{
			UpdateSource ();
			UpdateAspect ();
			this.EnsureId ();

			ElementChanged?.Invoke (this, new VisualElementChangedEventArgs (e.OldElement, e.NewElement));
		}

		Size MinimumSize ()
		{
			return new Size ();
		}

		SizeRequest IVisualElementRenderer.GetDesiredSize (int widthConstraint, int heightConstraint)
		{
			if (_disposed) {
				return new SizeRequest ();
			}

			Measure (widthConstraint, heightConstraint);
			return new SizeRequest (new Size (MeasuredWidth, MeasuredHeight), MinimumSize ());
		}

		void IVisualElementRenderer.SetElement (VisualElement element)
		{
			if (element == null)
				throw new ArgumentNullException (nameof (element));

			var image = element as Image;
			Image oldElement = _element;
			_element = image ?? throw new ArgumentException ("Element is not of type " + typeof (Image), nameof (element));

			if (oldElement != null)
				oldElement.PropertyChanged -= OnElementPropertyChanged;

			element.PropertyChanged += OnElementPropertyChanged;

			if (_visualElementTracker == null)
				_visualElementTracker = new VisualElementTracker (this);

			OnElementChanged (new ElementChangedEventArgs<Image> (oldElement, _element));
		}

		void IVisualElementRenderer.SetLabelFor (int? id)
		{
			if (_defaultLabelFor == null)
				_defaultLabelFor = LabelFor;

			LabelFor = (int)(id ?? _defaultLabelFor);
		}

		void IVisualElementRenderer.UpdateLayout () => _visualElementTracker?.UpdateLayout ();

		VisualElement IVisualElementRenderer.Element => _element;

		VisualElementTracker IVisualElementRenderer.Tracker => _visualElementTracker;

		AView IVisualElementRenderer.View => this;

		ViewGroup IVisualElementRenderer.ViewGroup => null;

		AImageView Control => this;

		public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
		public event EventHandler<PropertyChangedEventArgs> ElementPropertyChanged;

		public ImageRenderer () : base (Forms.Context)
		{
		}

		void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == Image.SourceProperty.PropertyName)
				UpdateSource ();
			else if (e.PropertyName == Image.AspectProperty.PropertyName)
				UpdateAspect ();

			ElementPropertyChanged?.Invoke (this, e);
		}

		void UpdateAspect ()
		{
			if (_element == null || _disposed)
				return;

			ScaleType type = _element.Aspect.ToScaleType ();
			SetScaleType (type);
		}

		async void UpdateSource ()
		{
			if (_element == null || _disposed)
				return;

			var source = _element.Source;
			if (source == null) {
				SetImageBitmap (null);
				return;
			}

			RequestManager request = Glide.With (Context);
			RequestBuilder builder = null;

			if (source is FileImageSource fileSource) {
				var drawable = ResourceManager.GetDrawableByName (fileSource.File);
				if (drawable != 0) {
					builder = request.Load (drawable);
				} else {
					builder = request.Load (fileSource.File);
				}
			} else if (source is UriImageSource uriSource) {
				builder = request.Load (uriSource.Uri.OriginalString);
			} else if (source is StreamImageSource streamSource) {
				var token = new CancellationToken ();
				using (var memoryStream = new MemoryStream ())
				using (var stream = await streamSource.Stream (token)) {
					if (token.IsCancellationRequested || source != _element.Source)
						return;
					stream.CopyTo (memoryStream);
					builder = request.Load (memoryStream.ToArray ());
				}
			}

			builder?.Into (this);
		}
	}
}