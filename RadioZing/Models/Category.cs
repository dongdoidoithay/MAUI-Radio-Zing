using RadioZing.Models.Responses;

namespace RadioZing.Models;

public class Category
{
    public Category(CategoryResponse response)
    {
        cateId= response.cateId;
        cateParentId = response.cateParentId;
        name = response.name;
        description = response.description;
        type = response.description;
        image = response.image;
        isActive = response.isActive;
    }

    public string cateId { get; set; }
    public string? cateParentId { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
    public string? type { get; set; }
    public string? image { get; set; }
    public bool isActive { get; set; }
    
    public bool isSelected {  get; set; }=false;
}
