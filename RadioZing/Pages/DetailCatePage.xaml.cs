namespace RadioZing.Pages;

public partial class DetailCatePage : ContentPage
{
    private DateTime _lastScrollToTime = DateTime.Now;
    private DetailCateViewModel viewModel => BindingContext as DetailCateViewModel;
    public DetailCatePage(DetailCateViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        this.player.OnAppearing();
        await viewModel.InitializeAsync();
    }

    protected override void OnDisappearing()
    {
        this.player.OnDisappearing();
        base.OnDisappearing();
    }

    private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        if (DeviceInfo.Current.Platform != DevicePlatform.WinUI)
        {
            return;
        }
        if (_lastScrollToTime.Subtract(DateTime.Now).TotalMilliseconds > 0)
        {
            return;
        }
        _lastScrollToTime = DateTime.Now.AddSeconds(1);

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