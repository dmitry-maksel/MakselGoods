namespace Reviews.API.Core.Models
{
    public class GetReviewResponseModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public string DisplayName { get; set; } = null!;

        public int Rating { get; set; }

        public string Text { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }
    }
}
