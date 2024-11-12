namespace Products.API.Core.Entities;

public class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<Product> Products { get; set; } = [];
}
