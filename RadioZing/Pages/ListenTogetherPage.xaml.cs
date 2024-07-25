
using CommunityToolkit.Mvvm.Messaging;
using RadioZing.Messaging;

namespace RadioZing.Pages
{
    public partial class ListenTogetherPage
    {
        public ListenTogetherPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            player.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            player.OnDisappearing();

            WeakReferenceMessenger.Default.Send<LeaveRoomNotification>();

            base.OnDisappearing();
        }
    }
}
