using System.ComponentModel.DataAnnotations.Schema;

namespace TechStoreMVC.Entities
{
    [Table("tbBasketItems")]
    public class BasketItem
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("productId")]
        public int ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Column("basketId")]
        public int BasketId { get; set; }

        [ForeignKey("BasketId")]
        public virtual Basket Basket { get; set; }

        public string CalculatedPrice => $"{Quantity * Product.Price} CZK";

        public BasketItem(int id, Product product, int quantity, Basket basket)
        {
            Id = id;
            ProductId = product.Id;
            Quantity = quantity;
            Product = product;
            BasketId = basket.Id;
            Basket = basket;
        }

        public BasketItem()
        {
            Id = 0;
            ProductId = 0;
            Quantity = 0;
            Product = null!;
            BasketId = 0;
            Basket = null!;
        }
    }
}
