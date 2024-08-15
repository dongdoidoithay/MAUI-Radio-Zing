using Radio.Components;
using RadioZing.Models.Responses;
using static Radio.Components.ListenTogether.ListenTogether;

namespace RadioZing.Models;

public partial class Episode : ObservableObject
{
    public Episode()
    {
       
    }

    public Episode(RoomPlayerState playerState)
    {
        //Id = playerState.Episode.Id;
        //Title = playerState.Episode.Title;
        //Description = playerState.Episode.Description;
        //Published = playerState.Episode.Published;
        //Duration = playerState.Episode.Duration.ToString();
       // Image = playerState.Episode.image;
        Url = new Uri(playerState.Episode.Url);
    }



    public string episodeId { get; set; }
    public object albumId { get; set; }
    public string cateId { get; set; }
    public string subtitle { get; set; }
    public string type { get; set; }
    public string image { get; set; }
    public object auth { get; set; }
    public string desc { get; set; }
    public object language { get; set; }
    public object year { get; set; }
    public object lyrics { get; set; }
    public string downloadUrl { get; set; }
    public string songUrl { get; set; }
    public DateTime? date { get; set; }
    public string refUrl { get; set; }
    public bool isActive { get; set; }


    public Uri Url { get; set; }
    public string Duration{ get; set; }

    [ObservableProperty]
    private bool isInListenLater;
    public Uri ImageUrl =>new Uri(image);

}
