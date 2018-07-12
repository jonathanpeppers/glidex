using Android.Content;
using Android.Runtime;
using Android.Views;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AImageView = Android.Widget.ImageView;
using AView = Android.Views.View;

[assembly: ExportRenderer (typeof (Image), typeof (Android.Glide.ImageRenderer))]

namespace Android.Glide
{
	public class ImageRenderer : AImageView, IVisualElementRenderer
	{
		protected bool _disposed;
		protected Image _element;
		bool _skipInvalidate;
		int? _defaultLabelFor;
		VisualElementTracker _visualElementTracker;

		public ImageRenderer(Context context) : base (context) { }

		public ImageRenderer(IntPtr javaReference, JniHandleOwnership transfer): base (javaReference, transfer) { }

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
			UpdateImage ();
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

		public ImageRenderer () : base (Xamarin.Forms.Forms.Context)
		{
		}

		void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == Image.SourceProperty.PropertyName)
				UpdateImage ();
			else if (e.PropertyName == Image.AspectProperty.PropertyName)
				UpdateAspect ();

			ElementPropertyChanged?.Invoke (this, e);
		}

		protected virtual void UpdateAspect ()
		{
			if (_element == null || _disposed)
				return;

			ScaleType type = _element.Aspect.ToScaleType ();
			SetScaleType (type);
		}

		protected virtual void UpdateImage ()
		{
			if (_element == null || _disposed)
				return;

			this.LoadViaGlide (_element);
		}
	}
}