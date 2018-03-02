# GlideX
GlideX is a minimalist Xamarin binding of Glide found at https://github.com/bumptech/glide

We don't want or care to bind the entirety of Glide's public API surface. Our goal here is to just bind the "useful" APIs for Glide.

For example take the following C#:
```csharp
var image = FindViewById<ImageView> (Resource.Id.testImage);
Glide.With (this)
    .Load ("https://botlist.co/system/BotList/Bot/logos/000/002/271/medium/chuck_norris.jpg")
    .Apply (RequestOptions.CircleCropTransform ().Placeholder (Android.Resource.Drawable.IcMenuCamera))
    .Into (image);
```

This code loads an image from a URL dynamically, taking care of all of Glide's cool caching functionality. These are the only APIs we need to make the library useful.

If you have a "classic" Xamarin.Android app that is not Xamarin.Forms, you can use the `glidex` NuGet package if desired.

# GlideX in Xamarin.Forms

The entire goal is to get fast Images for Xamarin.Forms on Android by using Glide.

I created two custom renderers to achieve this:
- `Android.Glide.ImageRenderer` - ported from the "fast" XF `ImageRenderer`
- `Android.Glide.ImageCellRenderer` - a standard `CellRenderer` that hooks into Glide for images

This library won't use `IImageSourceHandler` at all, it flat out ignores it. `IImageSourceHandler`'s return value of `Task<Android.Graphics.Bitmap>` makes it impossible to achieve our goal.

But to set this library up in your existing project, merely:
- Add the `glidex.forms` NuGet package
- Add this one liner after your app's `Forms.Init` call:

```csharp
Xamarin.Forms.Forms.Init (this, bundle);
//This forces the custom renderers to be used
Android.Glide.Forms.Init ();
LoadApplication (new App ());
```

If you want to customize how Glide is used in your app, right now you can:
- Subclass `Android.Glide.ImageRenderer` or `Android.Glide.ImageCellRenderer`
- Override `UpdateImage` and use the various `protected` members as needed
- Use the `glidex` Java binding directly as you prefer

# Comparing Performance

It turns out it is quite difficult to measure performance improvements specifically for images in Xamarin.Forms. Due to the asynchronous nature of how images load, I've yet to figure out good points at which to clock times via a `Stopwatch`.

So instead, I found it much easier to measure memory usage. 

Here is a table of peak memory used via the different samples:
| Page             | Loaded by     | Peak Memory Usage |
| ---------------- | ------------- | ----------------- |
| HugeImagePage    | Xamarin.Forms |         267309792 |
| HugeImagePage    | Glide         |           9943184 |


_NOTE: I believe these numbers are in bytes._

However, the stock XF performance of images is very poor due to the amount of images on each page. Disabling the Glide library in the sample app causes "out of memory" errors to happen as images load. You will see empty white squares where this occurs and get console output. To try this, you can remove the references to `glidex` and `glidex.forms` and comment out the `Android.Glide.Forms.Init()` line.