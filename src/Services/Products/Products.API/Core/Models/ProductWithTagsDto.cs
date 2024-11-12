namespace Products.API.Core.Models
{
    public class ProductWithTagsDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public TagDto[] Tags { get; set; } = [];
    }
}
