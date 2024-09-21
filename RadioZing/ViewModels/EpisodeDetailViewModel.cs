using RadioZing.Resources.Strings;

namespace RadioZing.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(ShowId), nameof(ShowId))]
public partial class EpisodeDetailViewModel : ViewModelBase
{
    private readonly ListenLaterService listenLaterService;
    private readonly GetDataService podcastService;
    private readonly PlayerService playerService;
    private readonly SubscriptionsService subscriptionsService;
    private readonly ImageProcessingService imageProcessingService;

    public string Id { get; set; }
    public string ShowId { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsInListenLater))]
    Episode episode;

    public bool IsInListenLater
    {
        get => episode?.IsInListenLater ?? false;
        set
        {
            if (episode is null)
                return;

            episode.IsInListenLater = value;
            OnPropertyChanged();
        }
    }

    [ObservableProperty]
    private ShowViewModel show;

    public EpisodeDetailViewModel(ListenLaterService listen, GetDataService shows, PlayerService player, SubscriptionsService subs, ImageProcessingService imageProcessing)
    {
        listenLaterService = listen;
        podcastService = shows;
        playerService = player;
        subscriptionsService = subs;
        imageProcessingService = imageProcessing;
    }

    internal async Task InitializeAsync()
    {
        if (Episode != null)
            return;

        await FetchAsync();
    }

    async Task FetchAsync()
    {
        var show = await podcastService.GetShowByIdAsync(new Guid(ShowId).ToString());

        var showVM = new ShowViewModel(show, subscriptionsService.IsSubscribed(show.Id), imageProcessingService);

        Show = showVM;
        Show.InitializeCommand.Execute(null);

        var eId = new Guid(Id).ToString();
        Episode = Show.Episodes.FirstOrDefault(e => e.episodeId == eId);

        if (Show == null || Episode == null)
        {
            await Shell.Current.DisplayAlert(
                AppResource.Error_Title,
                AppResource.Error_Message,
                AppResource.Close);

            return;
        }

        IsInListenLater = listenLaterService.IsInListenLater(Episode);
    }

    [RelayCommand]
    Task ListenLater()
    {
        if (listenLaterService.IsInListenLater(episode))
            listenLaterService.Remove(episode);
        else
            listenLaterService.Add(episode, show.Show);

        IsInListenLater = listenLaterService.IsInListenLater(episode);
        Show.Episodes.FirstOrDefault(x => x.episodeId == episode.episodeId).IsInListenLater = IsInListenLater;
        return Task.CompletedTask;
    }

    [RelayCommand]
    Task Play() => playerService.PlayAsync(Episode);

    [RelayCommand]
    Task Share() => 
        Microsoft.Maui.ApplicationModel.DataTransfer.Share.RequestAsync(new ShareTextRequest
    {
        Text = $"{Config.BaseWeb}cate/{show.Show.Id}",
        Title = "Share the episode uri"
    });
}
