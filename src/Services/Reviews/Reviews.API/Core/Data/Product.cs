namespace Reviews.API.Core.Data;

public class Product
{
    public int Id { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public List<Review> Reviews { get; set; } = [];
}
