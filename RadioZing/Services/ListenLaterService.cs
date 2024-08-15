namespace RadioZing.Services;

public class ListenLaterService
{
    List<Tuple<Episode, Show>> episodes;

    public ListenLaterService()
    {
        episodes = new List<Tuple<Episode, Show>>();
    }

    public List<Tuple<Episode, Show>> GetEpisodes()
    {
        return episodes;
    }

    public void Add(Episode episode, Show Show)
    {
        if (episodes.Any(ep => ep.Item1.episodeId == episode.episodeId))
            
            return;
        
        episodes.Add(new Tuple<Episode, Show>(episode, Show));
    }

    public void Remove(Episode episode)
    {
        var episodeToRemove = episodes.First(ep => ep.Item1.episodeId == episode.episodeId);
        if (episodeToRemove != null)
        {
            episodes.Remove(episodeToRemove);
        }
    }

    public bool IsInListenLater(Episode episode)
    {
        return episodes.Any(ep => ep.Item1.episodeId == episode.episodeId);
    }
    
    public bool IsInListenLater(string id)
    {
        return episodes.Any(ep => ep.Item1.episodeId == id);
    }
}
