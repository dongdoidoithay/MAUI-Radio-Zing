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

    public Show(DataResponse response, ListenLaterService listenLaterService)
    {
        Id = response.idDoc;
        Title = response.name;
        Author = response.authName;
        Description = response.desc;
        Image = response.image;
        Updated = response.date;
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

    public IEnumerable<Episode> Episodes { get; set; }

    public IEnumerable<Category> Categories { get; set; }

    public bool IsFeatured { get; set; }
}
