namespace RadioZing.ViewModels;

public static class ViewModelExtensions
{
    public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
    {

        builder.Services.AddSingleton<HomePageViewModel>();

        builder.Services.AddSingleton<CategoriesViewModel>();
        builder.Services.AddTransient<CategoryViewModel>();
        builder.Services.AddSingleton<DiscoverViewModel>();
        builder.Services.AddTransient<EpisodeDetailViewModel>();
        builder.Services.AddSingleton<EpisodeViewModel>();
        builder.Services.AddSingleton<ListenLaterViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<ShellViewModel>();
        builder.Services.AddTransient<ShowDetailViewModel>();
        builder.Services.AddSingleton<ShowViewModel>();
        builder.Services.AddSingleton<SubscriptionsViewModel>();


        builder.Services.AddSingleton<DetailCateViewModel>();

        return builder;
    }
}
