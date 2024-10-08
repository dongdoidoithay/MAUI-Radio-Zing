namespace RadioZing.Controls;

public partial class Player : ContentView
{
    private PlayerService playerService;

    public Player()
    {
        InitializeComponent();
        AutomationProperties.SetIsInAccessibleTree(this, true);
        this.IsVisible = false;
#if WINDOWS || MACCATALYST
        this.HeightRequest = 90;
#elif ANDROID || IOS
        this.HeightRequest = 70;
#endif
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        if (playerService == null)
        {
            this.playerService = this.Handler.MauiContext.Services.GetService<PlayerService>();
            InitPlayer();
        }
    }

    private async void PlayGesture_Tapped(object sender, EventArgs e)
    {
        await playerService.PlayAsync(playerService.CurrentEpisode);
    }

    internal void OnAppearing()
    {
        InitPlayer();
    }

    void InitPlayer()
    {
        if (playerService == null)
            return;
        this.playerService.IsPlayingChanged += PlayerService_IsPlayingChanged;
        IsVisible = playerService.CurrentEpisode != null;
        if (this.playerService.CurrentEpisode != null)
            UpdatePlayPause();
    }

    private void UpdatePlayPause()
    {
        this.IsVisible = true;
        BindingContext = new
        {
            PlayButtonSource = this.playerService.IsPlaying ? "player_pause.png" : "player_play.png",
            EpisodeTitle = this.playerService.CurrentEpisode.subtitle,
            AuthorText = $"{this.playerService.CurrentEpisode?.auth} - {this.playerService.CurrentEpisode?.date:MMM, d yyy}",
            PodcastImage = this.playerService.CurrentEpisode?.ImageUrl,
            Duration = this.playerService.CurrentEpisode?.Duration?.ToString()
        };
    }

    private void PlayerService_IsPlayingChanged(object sender, EventArgs e)
    {
        if (this.playerService.CurrentEpisode == null)
        {
            IsVisible = false;
        }
        else
        {
            UpdatePlayPause();
        }
    }

    internal void OnDisappearing()
    {
        this.playerService.IsPlayingChanged -= PlayerService_IsPlayingChanged;
    }
}