namespace Reviews.API.Core.Entities;

public class Product
{
    public int Id { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public List<Review> Reviews { get; set; } = [];
}
