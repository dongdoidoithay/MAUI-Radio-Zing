namespace Radio.Components;

public record RoomEpisode(
    string Id,
    string Title,
    string Description,
    string Url,
    DateTime Published,
    TimeSpan? Duration,
    RoomShow Show);
