namespace Web.BFF.Models;

public record Review(int Id, int ProductId, int UserId, string UserDisplayName,
    int Raiting, string Text, DateTimeOffset CreateAt, DateTimeOffset ModifiedAt);
