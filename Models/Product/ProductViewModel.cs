using System.ComponentModel.DataAnnotations;

namespace TechStoreMVC.Models.Product
{
    public class ProductViewModel
    {
        [Required]
        public int Id { get; set; }
        public string? Brand { get; set; }
        [Required]
        public string Model { get; set; }
        public string? Type { get; set; }
        [Required]
        [Range(0.01, 99_999_999)]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string PriceText => $"{Price} CZK";

        public ProductViewModel(int id, string? brand, string model, string? type, decimal price, string description, int quantity)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Type = type;
            Price = price;
            Description = description;
        }

        public ProductViewModel()
        {
            Id = 0;
            Brand = string.Empty;
            Model = string.Empty;
            Type = string.Empty;
            Price = 0;
            Description = string.Empty;
        }
    }
}
