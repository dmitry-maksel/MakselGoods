namespace Reviews.API.Core.Models;

public record UpdateReviewRequestModel(int Id, int Rating, string Text);
