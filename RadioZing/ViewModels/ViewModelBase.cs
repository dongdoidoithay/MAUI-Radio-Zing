using RadioZing.Utils;

namespace RadioZing.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    private string _loadingKey = "";

    [ObservableProperty]
    string title;

    [ObservableProperty]
    string subtitle;

    [ObservableProperty]
    string icon;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !isBusy;

    [ObservableProperty]
    bool canLoadMore;

    [ObservableProperty]
    string header;

    [ObservableProperty]
    string footer;

    //loadding
    internal void Loading(string message)
    {
        //The same page is not allowed to load at the same time
        if (_loadingKey.IsNotEmpty())
        {
            return;
        }
        _loadingKey = GuidUtils.GetFormatN();
        //LoadingService.Loading(_loadingKey, message);
    }
    internal void LoadComplete()
    {
        if (_loadingKey.IsEmpty())
        {
            return;
        }
        //LoadingService.LoadComplete(_loadingKey);
        _loadingKey = "";
    }


}
