using RadioZing.Models.Responses;

namespace RadioZing.Models;

public class Category
{
    public Category(CategoryResponse response)
    {
        Id = response.id;
        Name = response.name;
    }   

    public string Id { get; set; }

    public string Name { get; set; }
}
