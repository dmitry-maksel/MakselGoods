namespace Reviews.API.Core.Data;

public class Review
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public int Rating { get; set; }

    public string Text { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset ModifiedAt { get; set; }

    // change to enum status: PendingForReview, Approved, Banned
    public bool IsApproved { get; set; }


    public Product Product { get; set; } = null!;

    public User User { get; set; } = null!;
}
