using System.ComponentModel.DataAnnotations.Schema;

namespace TechStoreMVC.Entities
{
    [Table("tbOrderItems")]
    public class OrderItem
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("productId")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [Column("orderId")]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }

        public OrderItem(int id, Product product, int quantity, Order order)
        {
            Id = id;
            ProductId = product.Id;
            Quantity = quantity;
            Product = product;
            OrderId = order.Id;
            Order = order;
        }
        public OrderItem()
        {
            Id = 0;
            ProductId = 0;
            Quantity = 0;
            Product = null!;
            OrderId = 0;
            Order = null!;
        }
    }
}
