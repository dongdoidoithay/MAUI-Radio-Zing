namespace RadioZing.ViewModels;

public partial class CategoriesViewModel : ViewModelBase
{
    private readonly GetDataService showsService;

    [ObservableProperty]
    List<Category> categories;

    public CategoriesViewModel(GetDataService shows)
    {
        showsService = shows;
    }

    public async Task InitializeAsync()
    {   
        var categories = await showsService.GetAllCategories();
        
        Categories = categories?.ToList();
    }

    [RelayCommand]
    Task Selected(Category category)
    {
        return Shell.Current.GoToAsync($"{nameof(CategoryPage)}?Id={category.CateId}");
    }
}
