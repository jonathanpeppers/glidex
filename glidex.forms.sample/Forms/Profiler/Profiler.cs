using System.Collections.Concurrent;
using System.Diagnostics;
using Android.Util;

namespace Android.Glide.Sample
{
	public static class Profiler
	{
		const string Tag = "XFProfiler";
		static readonly ConcurrentDictionary<string, Stopwatch> watches = new ConcurrentDictionary<string, Stopwatch> ();

		public static void Start (object view)
		{
			Start (view.GetType ().Name);
		}

		public static void Start (string tag)
		{
			Log.Debug (Tag, "Starting Stopwatch {0}", tag);

			var watch =
				watches [tag] = new Stopwatch ();
			watch.Start ();
		}

		public static void Stop (string tag)
		{
			Stopwatch watch;
			if (watches.TryRemove (tag, out watch)) {
				Log.Debug (Tag, "Stopwatch {0} took {1}", tag, watch.Elapsed);
			}
		}
	}
}
