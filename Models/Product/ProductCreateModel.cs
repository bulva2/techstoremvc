
namespace TechStoreMVC.Models.Product
{
    public class ProductCreateModel
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string? Brand { get; set; }
        public string Model { get; set; }
        public string? Type { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? LargeImage { get; set; }

        public ProductCreateModel(int id, int? categoryId, string? brand, string model, string? type, decimal price, string description, IFormFile? image, IFormFile? largeImage)
        {
            Id = id;
            CategoryId = categoryId;
            Brand = brand;
            Model = model;
            Type = type;
            Price = price;
            Description = description;
            Image = image;
            LargeImage = largeImage;
        }

        public ProductCreateModel()
        {
            Id = 0;
            CategoryId = 0;
            Brand = string.Empty;
            Model = string.Empty;
            Type = string.Empty;
            Price = 0;
            Description = string.Empty;
            Image = null;
            LargeImage = null;
        }
    }
}
