namespace RadioZing
{
    class MauiWindow : Window
    {
        private PlayerService playerService;

        public MauiWindow() : base() { }

        public MauiWindow(Page page) : base(page) { }


        protected override void OnCreated()
        {
            base.OnCreated();

            if (playerService == null)
            {
                this.playerService = this.Handler.MauiContext.Services.GetService<PlayerService>();
            }

            if (Settings.EpisodeId != null)
            {
                var episode = new Episode()
                {
                    episodeId =Settings.EpisodeId,
                    subtitle = Settings.EpisodeTitle,
                    desc = Settings.EpisodeDescription,
                    Duration = Settings.EpisodeDuration,
                    Url = new Uri(Settings.EpisodeUrl),
                    date = Settings.EpisodePublished
                };

                this.playerService.CurrentEpisode = episode;

                Task.Run(async () =>
                {
                    await this.playerService.resumeEpisode(Settings.CurrentPositionPlayer);
                });
            }
        }

        protected override void OnDestroying()
        {
            if (this.playerService.IsPlaying)
            {
                Settings.CurrentPositionPlayer = this.playerService.CurrentPosition;
                Settings.EpisodeId = this.playerService.CurrentEpisode.episodeId.ToString();
                Settings.EpisodeTitle = this.playerService.CurrentEpisode.subtitle;
                Settings.EpisodeDescription = this.playerService.CurrentEpisode.desc;
                Settings.EpisodeDuration = this.playerService.CurrentEpisode.Duration;
                Settings.EpisodeUrl = this.playerService.CurrentEpisode.Url.OriginalString;
                Settings.EpisodePublished = this.playerService.CurrentEpisode.date;
            }
            else
            {
                Settings.CurrentPositionPlayer = 0.0;
                Settings.EpisodeId = null;
                Settings.EpisodeTitle = null;
                Settings.EpisodeDescription = null;
                Settings.EpisodeDuration = null;
                Settings.EpisodeUrl = null;
                Settings.EpisodePublished = new DateTime();
            }

            base.OnDestroying();
        }
    }
}
