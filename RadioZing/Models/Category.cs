using RadioZing.Models.Responses;

namespace RadioZing.Models;

public partial class Category:ObservableObject
{
    public Category(CategoryResponse response)
    {
        CateId = response.cateId;
        CateParentId = response.cateParentId;
        Name = response.name;
        Description = response.description;
        Type = response.description;
        Image = response.image;
        IsActive = response.isActive;
    }

    [ObservableProperty]
    private string cateId;
    [ObservableProperty]
    private string? cateParentId;
    [ObservableProperty]
    private string? name;
    [ObservableProperty]
    private string? description;
    [ObservableProperty]
    private string? type;
    [ObservableProperty]
    private string? image;
    [ObservableProperty]
    private bool isActive;
    [ObservableProperty]
    private bool isSelected ;
}
