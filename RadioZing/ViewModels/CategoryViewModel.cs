namespace RadioZing.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class CategoryViewModel : ViewModelBase
{
    private readonly GetDataService showsService;
    private readonly SubscriptionsService subscriptionsService;
    private readonly ImageProcessingService imageProcessingService;

    [ObservableProperty]
    string text;

    public string Id { get; set; }

    [ObservableProperty]
    Category category;

    [ObservableProperty]
    List<ShowViewModel> shows;


    public CategoryViewModel(GetDataService shows, SubscriptionsService subs, ImageProcessingService imageProcessing)
    {
        showsService = shows;
        subscriptionsService = subs;
        imageProcessingService = imageProcessing;
    }


    public async Task InitializeAsync()
    {
        await LoadCategoryAsync();
        var shows = await showsService.GetShowsByCategoryAsync(Id);

        Shows = LoadShows(shows);
    }

    async Task LoadCategoryAsync()
    {
        var allCategories = await showsService.GetAllCategories();
        Category = allCategories?.First();
    }

    [RelayCommand]
    async Task Subscribe(ShowViewModel vm)
    {
        await subscriptionsService.UnSubscribeFromShowAsync(vm.Show);
        OnPropertyChanged(nameof(vm.IsSubscribed));
    }

    [RelayCommand]
    async void Search()
    {
        var shows = await showsService.SearchShowsAsync(new Guid(Id).ToString(), Text);
        Shows = LoadShows(shows);
    }

    List<ShowViewModel> LoadShows(IEnumerable<Show> shows)
    {
        var showList = new List<ShowViewModel>();
        if (shows == null)
        {
            return showList;
        }

        foreach (var show in shows)
        {
            var showVM = new ShowViewModel(show, subscriptionsService.IsSubscribed(show.Id), imageProcessingService);
            showList.Add(showVM);
        }

        return showList;
    }
}
