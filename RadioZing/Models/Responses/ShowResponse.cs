using static Microsoft.Maui.Controls.Internals.Profile;

namespace RadioZing.Models.Responses;

public class ShowResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Description { get; set; }

    public Uri Image { get; set; }

    public DateTime Updated { get; set; }

    public Uri Link { get; set; }

    public string Email { get; set; }

    public string Language { get; set; }

    public bool IsFeatured { get; set; }
    
    public CategoryResponse[] Categories { get; set; }

    public EpisodeResponse[] Episodes { get; set; }
}


public class DataResponse
{
    public string idDoc { get; set; }
    public string name { get; set; }
    public string nameOther { get; set; }
    public string nameSeo { get; set; }
    public string image { get; set; }
    public string desc { get; set; }
    public string sortDesc { get; set; }
    public string auth { get; set; }
    public string authName { get; set; }
    public string genres { get; set; }
    public string genresName { get; set; }
    public string year { get; set; }
    public int? view { get; set; }
    public string art { get; set; }
    public string artName { get; set; }
    public string status { get; set; }
    public string statusName { get; set; }
    public DateTime? date { get; set; }
    public string type { get; set; }
    public string typeName { get; set; }
    public string url { get; set; }
    public string tags { get; set; }
    public int? rate { get; set; }
    public string postedBy { get; set; }
    public string serialization { get; set; }
    public string lang { get; set; }
    public string idDocRef { get; set; }
    public int? upVote { get; set; }
    public int? downVote { get; set; }
    public int? commentCount { get; set; }
    public int? followCount { get; set; }
    public List<string> detail_documents { get; set; }
}

public class RootDataResponse
{
    public bool? success { get; set; }
    public string error { get; set; }
    public string message { get; set; }
    public string code { get; set; }
    public List<DataResponse> data { get; set; }
    public int? totalRecode { get; set; }
    public int? currentPage { get; set; }
    public int? totalPage { get; set; }
}