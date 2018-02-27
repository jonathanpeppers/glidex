namespace Com.Bumptech.Glide.Load.Data
{
	partial class AssetFileDescriptorLocalUriFetcher
	{
		protected override Java.Lang.Object LoadResource (global::Android.Net.Uri p0, global::Android.Content.ContentResolver p1)
		{
			return LoadFile (p0, p1);
		}
	}

	partial class FileDescriptorAssetPathFetcher
	{
		protected override Java.Lang.Object LoadResource (Android.Content.Res.AssetManager p0, string p1)
		{
			return LoadFile (p0, p1);
		}
	}

	partial class FileDescriptorLocalUriFetcher
	{
		protected override Java.Lang.Object LoadResource (global::Android.Net.Uri p0, global::Android.Content.ContentResolver p1)
		{
			return LoadFile (p0, p1);
		}
	}

	partial class StreamAssetPathFetcher
	{
		protected override Java.Lang.Object LoadResource (Android.Content.Res.AssetManager p0, string p1)
		{
			var handle = Android.Runtime.InputStreamAdapter.ToLocalJniHandle (LoadFile (p0, p1));
			try {
				return new Java.Lang.Object (handle, Android.Runtime.JniHandleOwnership.TransferLocalRef);
			} finally {
				Android.Runtime.JNIEnv.DeleteLocalRef (handle);
			}
		}
	}

	partial class StreamLocalUriFetcher
	{
		protected override Java.Lang.Object LoadResource (global::Android.Net.Uri p0, global::Android.Content.ContentResolver p1)
		{
			var handle = Android.Runtime.InputStreamAdapter.ToLocalJniHandle (LoadFile (p0, p1));
			try {
				return new Java.Lang.Object (handle, Android.Runtime.JniHandleOwnership.TransferLocalRef);
			} finally {
				Android.Runtime.JNIEnv.DeleteLocalRef (handle);
			}
		}
	}
}