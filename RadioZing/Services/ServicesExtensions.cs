using Radio.Components;

namespace RadioZing.Services;

public static class ServicesExtensions
{
    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddSingleton<SubscriptionsService>();
        builder.Services.AddSingleton<GetDataService>();
        builder.Services.AddSingleton<ListenLaterService>();
#if WINDOWS
        builder.Services.TryAddSingleton<SharedMauiLib.INativeAudioService, SharedMauiLib.Platforms.Windows.NativeAudioService>();
#elif ANDROID
        builder.Services.TryAddSingleton<SharedMauiLib.INativeAudioService, SharedMauiLib.Platforms.Android.NativeAudioService>();
#elif MACCATALYST
        builder.Services.TryAddSingleton<SharedMauiLib.INativeAudioService, SharedMauiLib.Platforms.MacCatalyst.NativeAudioService>();
        builder.Services.TryAddSingleton< Platforms.MacCatalyst.ConnectivityService>();
#elif IOS
        builder.Services.TryAddSingleton<SharedMauiLib.INativeAudioService, SharedMauiLib.Platforms.iOS.NativeAudioService>();
#endif

        builder.Services.TryAddTransient<WifiOptionsService>();
        builder.Services.TryAddSingleton<PlayerService>();

        builder.Services.AddScoped<ThemeInterop>();
        builder.Services.AddScoped<ClipboardInterop>();
        builder.Services.AddScoped<ListenTogetherHubClient>(_ =>
            new ListenTogetherHubClient(Config.ListenTogetherUrl));

        builder.Services.AddSingleton<ImageProcessingService>();


        return builder;
    }
}
