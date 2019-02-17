using Android.Util;

namespace Picasso
{
	/// <summary>
	/// Class for initializing glidex.forms
	/// </summary>
	public static class Forms
	{
		static bool initialized;

		/// <summary>
		/// Initializes glidex.forms, put this after your `Xamarin.Forms.Forms.Init (this, bundle);` call.
		/// </summary>
		/// <param name="debug">Enables debug logging. Turn this on to verify Glide is being used in your app.</param>
		public static void Init (bool debug = false)
		{
			if (initialized)
				return;

			IsDebugEnabled = debug;

			initialized = true;
		}

		/// <summary>
		/// A flag indicating if Debug logging is enabled
		/// </summary>
		public static bool IsDebugEnabled {
			get;
			private set;
		}

		const string Tag = "glidex";

		internal static void Warn (string format, params object [] args)
		{
			Log.Warn (Tag, format, args);
		}

		internal static void Debug (string format, params object [] args)
		{
			if (IsDebugEnabled)
				Log.Debug (Tag, format, args);
		}
	}
}