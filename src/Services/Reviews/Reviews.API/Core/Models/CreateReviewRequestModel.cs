namespace Reviews.API.Core.Models;

public record CreateReviewRequestModel(int UserId, int Rating, string Text);
