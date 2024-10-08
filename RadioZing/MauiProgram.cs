﻿using FFImageLoading.Maui;
using MonkeyCache.FileStore;

namespace RadioZing;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            
            .ConfigureEssentials()
            .ConfigureServices()
            .ConfigurePages()
            .ConfigureViewModels()
            .UseFFImageLoading()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Segoe-Ui-Bold.ttf", "SegoeUiBold");
                fonts.AddFont("Segoe-Ui-Regular.ttf", "SegoeUiRegular");
                fonts.AddFont("Segoe-Ui-Semibold.ttf", "SegoeUiSemibold");
                fonts.AddFont("Segoe-Ui-Semilight.ttf", "SegoeUiSemilight");
            });

        Barrel.ApplicationId = "radiozing";

        builder.Services.AddMauiBlazorWebView();
        return builder.Build();
    }
}
