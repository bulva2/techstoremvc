using TechStoreMVC.Entities;

namespace TechStoreMVC.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }

        public string TotalPriceText => $"{TotalPrice} CZK";

        public OrderViewModel(int id, decimal totalPrice, string paymentMethod, string address, string status, List<OrderItem> orderItems)
        {
            Id = id;
            TotalPrice = totalPrice;
            PaymentMethod = paymentMethod;
            Address = address;
            Status = status;
            OrderItems = orderItems;
        }

        public OrderViewModel()
        {
            Id = 0;
            TotalPrice = 0;
            PaymentMethod = string.Empty;
            Address = string.Empty;
            Status = string.Empty;
            OrderItems = new List<OrderItem>();
        }
    }
}
