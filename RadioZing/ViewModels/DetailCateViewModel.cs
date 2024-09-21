
using MvvmHelpers;
using RadioZing.Models.Responses;
using RadioZing.Resources.Strings;

namespace RadioZing.ViewModels;

[QueryProperty(nameof(IdCate), nameof(IdCate))]
[QueryProperty(nameof(TypeCate), nameof(TypeCate))]
public partial class DetailCateViewModel : ViewModelBase
{
    private readonly PlayerService playerService;
    private readonly SubscriptionsService subscriptionsService;
    private readonly ListenLaterService listenLaterService;
    private readonly GetDataService dataService;
    private readonly ImageProcessingService imageProcessingService;

    public string IdCate { get; set; }
    public string TypeCate { get; set; }
    private int _currentPage = 0;


    [ObservableProperty]
    Category cate;

    [ObservableProperty]
    Episode episodeForPlaying;

    [ObservableProperty]
    ObservableCollection<Episode> episodes;


    [ObservableProperty]
    bool isPlaying;

    [ObservableProperty]
    string textToSearch;


    public event EventHandler? NewMusicAdded;
    public event EventHandler? IsPlayingChanged;
    public DetailCateViewModel(GetDataService data, PlayerService player, SubscriptionsService subs, ListenLaterService later, ImageProcessingService imageProcessing)
    {
        dataService = data;
        playerService = player;
        subscriptionsService = subs;
        listenLaterService = later;
        imageProcessingService = imageProcessing;

        episodes = new ObservableRangeCollection<Episode>();
    }

    internal async Task InitializeAsync()
    {
        await FetchListEpisodeByCate();
    }
    private async Task FetchListEpisodeByCate()
    {
        try
        {
            //Loading("loading....");
            CateEpisodes data = await dataService.GetEpisodeByCate(IdCate, _currentPage);

            if (data == null)
            {
                await Shell.Current.DisplayAlert(
                          AppResource.Error_Title,
                          AppResource.Error_Message,
                          AppResource.Close);

                return;
            }
            if (data.episodes?.Count() > 0)
            {
                Episodes.Clear();
                Episodes = new ObservableCollection<Episode>(data.episodes);
            }
        }
        catch (Exception ex)
        {
           // _logger.LogError(ex, $"Home Epi list failed to load,Cateid={cateId}");
        }

    }

    [RelayCommand]
    Task LoadNextPageAsyncCommand()
    {
        //if (_currentTagId.IsEmpty())
        //{
        //    return;
        //}

        try
        {
            // Loading("loading....");
            var page = _currentPage + 1;
            //var songMenus = await _musicNetPlatform.GetSongMenusFromTagAsync((NetMusicLib.Enums.PlatformEnum)Platform, _currentTagId, page);
            //_currentPage = page;
            //foreach (var songMenu in songMenus)
            //{
            //    SongMenus.Add(new SongMenuViewModel()
            //    {
            //        SongMenuType = SongMenuEnum.Tag,
            //        PlatformName = "xxx",
            //        Id = songMenu.Id,
            //        Name = songMenu.Name,
            //        ImageUrl = songMenu.ImageUrl,
            //        LinkUrl = songMenu.LinkUrl
            //    });
            //}
        }
        catch (Exception ex)
        {
           // _logger.LogError(ex, $"The song list rolling loading failed,id={_currentTagId}");
        }
        finally
        {
            LoadComplete();
        }
        return Task.FromResult(0); 
    }



    [RelayCommand]
    void SearchEpisode()
    {
        //var episodesList = cate.Episodes
        //    .Where(ep => ep.subtitle.Contains(TextToSearch, StringComparison.InvariantCultureIgnoreCase))
        //    .ToList();
        //Episodes.ReplaceRange(episodesList);
    }

    [RelayCommand]
    Task TapEpisode(Episode episode) => Shell.Current.GoToAsync($"{nameof(EpisodeDetailPage)}?Id={episode.episodeId}&ShowId={IdCate}");

    [RelayCommand]
    async Task Subscribe()
    {
        //if (Show is null || subscriptionsService is null)
        //    return;

        //if (Show.IsSubscribed)
        //{
        //    var isUnsubcribe = await subscriptionsService.UnSubscribeFromShowAsync(Show.Show);
        //    Show.IsSubscribed = !isUnsubcribe;
        //}
        //else
        //{
        //    subscriptionsService.SubscribeToShow(Show.Show);
        //    Show.IsSubscribed = true;
        //}
    }

    [RelayCommand]
    Task PlayEpisode(Episode episode) => playerService.PlayAsync(episode);

    [RelayCommand]
    Task AddToListenLater(Episode episode)
    {
        //var itemHasInListenLaterList = listenLaterService.IsInListenLater(episode);
        //if (itemHasInListenLaterList)
        //{
        //    listenLaterService.Remove(episode);
        //}
        //else
        //{
        //    listenLaterService.Add(episode, Show.Show);
        //}

       // episode.IsInListenLater = !itemHasInListenLaterList;

        return Task.CompletedTask;
    }
}
