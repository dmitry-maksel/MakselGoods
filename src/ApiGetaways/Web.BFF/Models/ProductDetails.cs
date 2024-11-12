namespace Web.BFF.Models;

public record ProductDetails(Product Product, IEnumerable<Tag> Tags, IEnumerable<Review> Reviews);
