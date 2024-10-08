﻿using Microsoft.Extensions.Logging;
using RadioZing.Models.Responses;
using RadioZing.Resources.Strings;

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
    private readonly GetDataService showsService;
    private readonly SubscriptionsService subscriptionsService;
    private readonly ImageProcessingService imageProcessingService;

    //private readonly SearchPage _searchPage;
    private readonly ILogger<HomePageViewModel> _logger;
    public HomePageViewModel(GetDataService shows, SubscriptionsService subs, CategoriesViewModel categories, ImageProcessingService imageProcessing, ILogger<HomePageViewModel> logger)
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
       
        if (_currentRootCateId == "")
        {
            HomeCates.Clear();
            int _index = 0;
            foreach (var category in rootCate) {
                if (_index == 0)
                {
                    _currentRootCateId = category.CateId;
                   category.IsSelected = true;
                }
                else
                {
                    category.IsSelected = false;
                }
                HomeCates.Add(category);
            }
            //lay phan tu dau tien
            await SelectTab(_currentRootCateId);
        }

    }
    private async Task FetchCateByParentIdAsync(string cateParentId)
    {
        var cates = await showsService.GetCateByParent(cateParentId);
        HomeCateChild.Clear();
        if (cates != null && cates.Count() > 0)
        {
            //add to list
            foreach (var child in cates)
            {
                child.Image =Config.APIUrl+ child.Image;
                //episode.image = await imageProcessingService.ProcessRemoteImage(new Uri(episode.ImageUrl));
                HomeCateChild.Add(child);
            }
        }
        //load Episode
       // await FetchListEpisodeByCate(cateParentId);
    }

    private async Task FetchListEpisodeByCate(string cateId)
    {
        try
        {
            //Loading("loading....");
            CateEpisodes data = await showsService.GetEpisodeByCate(cateId, _currentPage);
            //if (data != null && data.episodes.Count() > 0)
            //{
            //    //add to list
            //    foreach (var episode in data.episodes)
            //    {
            //        //episode.image = await imageProcessingService.ProcessRemoteImage(new Uri(episode.ImageUrl));
            //        HomeEpisodes.Add(episode);
            //    }
            //}
            if (data.episodes?.Count() > 0)
            {
                HomeEpisodes.Clear();
                HomeEpisodes = new ObservableCollection<Episode>(data.episodes);
            }
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
            if (tab.CateId == id)
            {
                tab.IsSelected = true;
            }
            else
            {
                tab.IsSelected = false;
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
                    //episode.image = await imageProcessingService.ProcessRemoteImage(new Uri(episode.ImageUrl));
                    HomeEpisodes.Add(episode);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"The song list rolling loading failed");
        }
       
    }



    [RelayCommand]
    private async void GoToDetailCateAsync(Category cate)
    {
        //if(cate.)
        await Shell.Current.GoToAsync($"{nameof(DetailCatePage)}?IdCate={cate.CateId}&TypeCate={cate.Type}");
    }

    //[RelayCommand]
    //private async void GoToSearchPageAsync()
    //{
    //    await App.Current.MainPage.Navigation.PushAsync(_searchPage, true);
    //}
}