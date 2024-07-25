namespace RadioZing.Models;

public class ShowGroup : List<ShowViewModel>
{
    public string Name { get; private set; }

    public ShowGroup(string name, List<ShowViewModel> show) : base(show)
    {
        Name = name;
    }
}
