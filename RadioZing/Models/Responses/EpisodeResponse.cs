namespace RadioZing.Models.Responses;

public class EpisodeResponse
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime Published { get; set; }

    public string Duration { get; set; }

    public Uri Url { get; set; }
}
