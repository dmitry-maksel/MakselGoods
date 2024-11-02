namespace Reviews.API.Core.Data;

public class User
{
    public int Id { get; set; }

    public string DisplayName { get; set; } = null!;

    public List<Review> Reviews { get; set; } = [];
}
