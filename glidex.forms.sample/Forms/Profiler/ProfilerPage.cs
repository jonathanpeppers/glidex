using Xamarin.Forms;

namespace Android.Glide.Sample
{
	public class ProfilerPage : ContentPage
	{
		readonly string name;
		MemoryProfiler memoryProfiler;

		public ProfilerPage ()
		{
			name = GetType ().Name;
			Profiler.Start (name + " Appearing");
			memoryProfiler = new MemoryProfiler (name);
		}

		protected override void OnAppearing ()
		{
			Device.BeginInvokeOnMainThread (() => Profiler.Stop (name + " Appearing"));
		}

		protected override void OnDisappearing ()
		{
			memoryProfiler.Dispose ();
		}
	}
}