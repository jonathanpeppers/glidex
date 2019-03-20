# glidex and glidex.forms
glidex is a minimalist Xamarin.Android binding of [Glide](https://github.com/bumptech/glide). Google recommends Glide for simplifying the complexity of managing `Android.Graphics.Bitmap` within your apps ([docs here](https://developer.android.com/topic/performance/graphics/manage-memory.html)).

glidex.forms is a prototype of what we can do to improve Xamarin.Forms image performance on Android by taking a dependency on Glide. See my post on the topic [here](http://jonathanpeppers.com/Blog/xamarin-forms-performance-on-android).

Download from NuGet (use the Prerelease checkbox):

| glidex | glidex.forms |
|---|---|
| [![NuGet](https://img.shields.io/nuget/dt/glidex.svg)](https://www.nuget.org/packages/glidex) | [![NuGet](https://img.shields.io/nuget/dt/glidex.forms.svg)](https://www.nuget.org/packages/glidex.forms) |

Learn more on this episode of the Xamarin Show:

[![Super Fast Image Loading for Android Apps with GlideX | The Xamarin Show](https://img.youtube.com/vi/IYF2ChHTTWc/maxresdefault.jpg)](https://youtu.be/IYF2ChHTTWc)

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

If you have a "classic" Xamarin.Android app that is not Xamarin.Forms, it could be useful to use the `glidex` NuGet package directly.

_glidex is currently using the 4.7.0 release of Glide from Github_

# glidex.forms for Xamarin.Forms on Android

My goal with this repo is to get fast Images for Xamarin.Forms on Android by using Glide.

The new `IImageViewHandler` API in Xamarin.Forms 3.3.x, allows glidex.forms to operate without using *any* custom renderers!

But to set this library up in your existing project, merely:
- Add the `glidex.forms` NuGet package
- Add this one liner after your app's `Forms.Init` call:

```csharp
Xamarin.Forms.Forms.Init (this, bundle);
//This forces the custom renderers to be used
Android.Glide.Forms.Init ();
LoadApplication (new App ());
```

## How do I know my app is using Glide?

On first use, you may want to enable debug logging:
```csharp
Android.Glide.Forms.Init (debug: true);
```
glidex.forms will print out log messages in your device log as to what is happening under the hood.

If you want to customize how Glide is used in your app, currently your option is to implement your own `IImageViewHandler`. See the [GlideExtensions](https://github.com/jonathanpeppers/glidex/blob/master/glidex.forms/GlideExtensions.cs) class for details.

# Comparing Performance

It turns out it is quite difficult to measure performance improvements specifically for images in Xamarin.Forms. Due to the asynchronous nature of how images load, I've yet to figure out good points at which to clock times via a `Stopwatch`.

So instead, I found it much easier to measure memory usage. I wrote a quick class that runs a timer and calls the Android APIs to grab memory usage.

Here is a table of peak memory used via the different sample pages I've written:

_NOTE: this was a past comparison with Xamarin.Forms 2.5.x_

| Page             | Loaded by     | Peak Memory Usage |
| ---              | ---           | ---:              |
| GridPage         | Xamarin.Forms |       268,387,112 |
| GridPage         | glidex.forms  |        16,484,584 |
| ViewCellPage     | Xamarin.Forms |        94,412,136 |
| ViewCellPage     | glidex.forms  |        12,698,112 |
| ImageCellPage    | Xamarin.Forms |        24,413,600 |
| ImageCellPage    | glidex.forms  |         9,977,272 |
| HugeImagePage    | Xamarin.Forms |       267,309,792 |
| HugeImagePage    | glidex.forms  |         9,943,184 |

_NOTE: I believe these numbers are in bytes. I restarted the app (release mode) before recording the numbers for each page. Pages with ListViews I scrolled up and down a few times._

Stock XF performance of images is poor due to the amount of `Android.Graphics.Bitmap` instances created on each page. Disabling the Glide library in the sample app causes "out of memory" errors to happen as images load. You will see empty white squares where this occurs and get console output.

To try stock Xamarin.Forms behavior yourself, you can remove the references to `glidex` and `glidex.forms` in the `glide.forms.sample` project and comment out the `Android.Glide.Forms.Init()` line.

# Features

In my samples, I tested the following types of images:
- `ImageSource.FromFile` with a temp file
- `ImageSource.FromFile` with `AndroidResource`
- `ImageSource.FromResource` with `EmbeddedResource`
- `ImageSource.FromUri` with web URLs
- `ImageSource.FromStream` with `AndroidAsset`

For example, the `GridPage` loads 400 images into a grid with a random combination of all of the above:

![GridPage](docs/GridPage.png)
