namespace Products.API.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }

        public List<Tag> Tags { get; set; } = [];
    }
}
