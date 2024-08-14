using Radio.Components;
using RadioZing.Models.Responses;
using static Radio.Components.ListenTogether.ListenTogether;

namespace RadioZing.Models;

public partial class Episode : ObservableObject
{
    public Episode() { }
    public Episode(RoomPlayerState playerState)
    {
        Id = playerState.Episode.Id;
        Title = playerState.Episode.Title;
        Description = playerState.Episode.Description;
        Published = playerState.Episode.Published;
        Duration = playerState.Episode.Duration.ToString();
        Url = new Uri(playerState.Episode.Url);
    }

    public Episode(EpisodeResponse response, bool isListenLater)
    {
        Id = response.episodeId;
        Title = response.subtitle;
        Description = response.desc;
        Published = response.date;
        Duration = response.Duration;
        Url = new Uri(response?.songUrl);
        isInListenLater = isListenLater;
    }

    public string Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? Published { get; set; }

    public string Duration { get; set; }

    public Uri Url { get; set; }

    [ObservableProperty]
    private bool isInListenLater;
}
