using RadioZing.ViewModels;

namespace RadioZing.Pages;

public partial class HomePage : ContentPage
{
    private bool _isFirstAppearing = true;
    //Control each time when scrolling, only one page of data is loaded
    private DateTime _lastScrollToTime = DateTime.Now;
    HomePageViewModel viewModel => BindingContext as HomePageViewModel;

    public HomePage(HomePageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //player.OnAppearing();
        await viewModel.InitializeAsync();
    }


    protected override void OnDisappearing()
    {
        //player.OnDisappearing();
        base.OnDisappearing();
    }

    private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        //TODO In Win UI, the Remaining Items Threshold Reached Command cannot be triggered at present, so implement it yourself first
        if (DeviceInfo.Current.Platform != DevicePlatform.WinUI)
        {
            return;
        }
        if (_lastScrollToTime.Subtract(DateTime.Now).TotalMilliseconds > 0)
        {
            return;
        }
        _lastScrollToTime = DateTime.Now.AddMilliseconds(20);

        if (sender is CollectionView cv && cv is IElementController element)
        {
            var count = element.LogicalChildren.Count;
            if (e.LastVisibleItemIndex + 1 - count + cv.RemainingItemsThreshold >= 0)
            {
                if (cv.RemainingItemsThresholdReachedCommand.CanExecute(null))
                {
                    cv.RemainingItemsThresholdReachedCommand.Execute(null);
                }
            }
        }
    }
}