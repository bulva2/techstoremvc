namespace TechStoreMVC.Models.Product
{
    public class ProductToBasketModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string? CategoryName { get; set; }

        public ProductToBasketModel(int productId, int quantity, string? categoryName = null)
        {
            Id = productId;
            Quantity = quantity;
            CategoryName = categoryName;
        }

        public ProductToBasketModel()
        {
            Id = 0;
            Quantity = 0;
            CategoryName = string.Empty;
        }

        public ProductToBasketModel(string? categoryName = null)
        {
            Id = 0;
            Quantity = 0;
            CategoryName = categoryName;
        }
    }
}
