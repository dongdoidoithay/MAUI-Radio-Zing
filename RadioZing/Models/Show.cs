using RadioZing.Models.Responses;
using static Radio.Components.ListenTogether.ListenTogether;

namespace RadioZing.Models;

public class Show
{

    public Show(RoomPlayerState playerState)
    {
        Id = playerState.Episode.Show.Id;
        Title = playerState.Episode.Show.Title;
        Author = playerState.Episode.Show.Author;
        Image = playerState.Episode.Show.Image;
    }

    public Show(EpisodeResponse response, ListenLaterService listenLaterService)
    {
        Id = response.episodeId;
        Title = response.subtitle;
        Author = response.auth;
        Description = response.desc;
        Image = response.image;
        Updated = response.date;
        if (response.type != null && response.type== "radio-online") {
            Episodes = new List<Episode>();
            Episode ed= new Episode()
            {
                Id= response.episodeId,
                Title= response.subtitle,
                Description= response.desc,
                Duration="0:0",
                Url=new Uri(response.songUrl),
            };
            Episodes.Add(ed);
        }
       // Episodes = response.Episodes?.Select(episode => new Episode(episode, listenLaterService.IsInListenLater(episode.Id)));
        //Categories = response.Categories?.Select(category => new Category(category));
        //IsFeatured = response.IsFeatured;
    }

    public string Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public DateTime? Updated { get; set; }

    public List<Episode> Episodes { get; set; }

    public IEnumerable<Category> Categories { get; set; }

    public bool IsFeatured { get; set; }
}
