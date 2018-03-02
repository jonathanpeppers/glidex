using Android.Util;
using System;
using System.Timers;

namespace Android.Glide.Sample
{
	public class MemoryProfiler : IDisposable
	{
		const string Tag = "Glide";
		readonly string name;
		readonly Timer timer = new Timer ();
		long peakMemory;

		public MemoryProfiler (string name)
		{
			this.name = name;
			timer.Interval = 1000;
			timer.Elapsed += OnElapsed;
			timer.Start ();
		}

		void OnElapsed (object sender, ElapsedEventArgs e)
		{
			var runtime = Java.Lang.Runtime.GetRuntime ();
			long usedMemory = runtime.TotalMemory () - runtime.FreeMemory ();
			if (usedMemory > peakMemory)
				peakMemory = usedMemory;

			Log.Debug (Tag, "{0} Memory, Used: {1}, Peak: {2}", name, usedMemory, peakMemory);
		}

		public void Dispose ()
		{
			timer.Stop ();
		}
	}
}