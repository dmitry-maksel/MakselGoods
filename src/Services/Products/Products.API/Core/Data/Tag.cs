namespace Products.API.Core.Data;

public class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<Product> Products { get; set; } = [];
}
