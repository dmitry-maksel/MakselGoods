namespace Reviews.API.Models;

public record CreateReviewRequestModel(int UserId, int Rating, string Text);
