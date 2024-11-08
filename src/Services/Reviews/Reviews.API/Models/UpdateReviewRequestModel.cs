namespace Reviews.API.Models;

public record UpdateReviewRequestModel(int Id, int Rating, string Text);
