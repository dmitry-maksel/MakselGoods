namespace Products.API.Core.Models
{
    public class ProductWithTagsModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public TagModel[] Tags { get; set; } = [];
    }
}
