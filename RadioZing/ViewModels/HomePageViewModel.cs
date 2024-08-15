using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using RadioZing.Models.Responses;
using RadioZing.Resources.Strings;
using System.Collections.ObjectModel;
using System.Web;

namespace RadioZing.ViewModels;

public partial class HomePageViewModel : ViewModelBase
{
    /// <summary>
    /// The page number of the current tag singing list
    /// </summary>
    private int _currentPage = 0;
    private string _currentRootCateId = "";
    private string _currentChildCateId = "";

    [ObservableProperty]
    private ObservableCollection<Category> _homeCates;

    [ObservableProperty]
    private ObservableCollection<Category> _homeCateChild;

    [ObservableProperty]
    private ObservableCollection<Episode> _homeEpisodes;

    private static readonly object LockPlatformMusicTags = new object();
    private readonly ShowsService showsService;
    private readonly SubscriptionsService subscriptionsService;
    private readonly ImageProcessingService imageProcessingService;

    //private readonly SearchPage _searchPage;
    private readonly ILogger<HomePageViewModel> _logger;
    public HomePageViewModel(ShowsService shows, SubscriptionsService subs, CategoriesViewModel categories, ImageProcessingService imageProcessing, ILogger<HomePageViewModel> logger)
    {
        showsService = shows;
        subscriptionsService = subs;
        imageProcessingService = imageProcessing;

        _logger = logger;
        HomeCates = new ObservableCollection<Category>();
        HomeCateChild = new ObservableCollection<Category>();
        HomeEpisodes = new ObservableCollection<Episode>();
    }
    internal async Task InitializeAsync()
    {
        //Delay on first load until window loads
        await FetchRootCateAsync();
    }
    private async Task FetchRootCateAsync()
    {
        var rootCate = await showsService.GetRootCate();

        if (rootCate == null)
        {
            await Shell.Current.DisplayAlert(
                AppResource.Error_Title,
                AppResource.Error_Message,
                AppResource.Close);
            return;
        }
        HomeCates.Clear();
        HomeCates = new ObservableCollection<Category>(rootCate); //khong dung foreach
        if (_currentRootCateId == "")
        {
            _currentRootCateId = rootCate.LastOrDefault().cateId;
            //lay phan tu dau tien
            await FetchCateByParentIdAsync(_currentRootCateId);
            //await FetchListEpisodeByCate(_currentRootCateId);
        }

    }
    private async Task FetchCateByParentIdAsync(string cateParentId)
    {
        var cates = await showsService.GetCateByParent(cateParentId);
        HomeCateChild.Clear();
        HomeCateChild = new ObservableCollection<Category>(cates);
        //load Episode
        await FetchListEpisodeByCate(cateParentId);
    }

    private async Task FetchListEpisodeByCate(string cateId)
    {
        try
        {
            //Loading("loading....");
            CateEpisodes data = await showsService.GetEpisodeByCate(cateId, _currentPage);
            if (data != null && data.episodes.Count() > 0)
            {
                //add to list
                foreach (var episode in data.episodes)
                {
                    HomeEpisodes.Add(episode);
                }
            }
            //if (data.episodes?.Count() > 0)
            //{
            //    HomeEpisodes.Clear();
            //    HomeEpisodes = new ObservableCollection<Episode>(data.episodes);
            //}
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Home Epi list failed to load,Cateid={cateId}");
        }
       
    }



    [RelayCommand]
    private async void TabChangedAsync(string id)
    {
        
        await SelectTab(id);
    }
    private async Task SelectTab(string id)
    {
        foreach (var tab in HomeCates)
        {
            if (tab.cateId == id)
            {
                tab.isSelected = true;
            }
            else
            {
                tab.isSelected = false;
            }
        }
        await FetchCateByParentIdAsync(id);
    }


    [RelayCommand]
    private async void LoadLastPageTagSongMenusAsync()
    {
        if (_currentRootCateId.IsEmpty())
        {
            return;
        }

        try
        {
            // Loading("loading....");
            _currentPage = _currentPage + 1;
            CateEpisodes data = await showsService.GetEpisodeByCate(_currentRootCateId, _currentPage);
            if (data!=null && data.episodes.Count() > 0)
            {
                //add to list
                foreach(var episode in data.episodes)
                {
                    HomeEpisodes.Add(episode);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"The song list rolling loading failed");
        }
       
    }

  

    //[RelayCommand]
    //private async void GotoSongMenuPageAsync(SongMenuViewModel songMenu)
    //{
    //    string json = HttpUtility.UrlEncode(songMenu.ToJson());
    //    await Shell.Current.GoToAsync($"{nameof(SongMenuPage)}?Json={json}&PlatformString={Platform}");
    //}

    //[RelayCommand]
    //private async void GoToSearchPageAsync()
    //{
    //    await App.Current.MainPage.Navigation.PushAsync(_searchPage, true);
    //}
}