using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechStoreMVC.Models.Basket;

namespace TechStoreMVC.Entities
{
    [Table("tbOrders")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("paymentMethodId")]
        public int PaymentMethodId { get; set; }

        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; }

        [Column("accountId")]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("firstname")]
        public string FirstName { get; set; }

        [Column("lastname")]
        public string LastName { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("totalPrice")]
        public decimal TotalPrice { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }

        public Order(int id, PaymentMethod paymentMethod, Account account, string address, string firstName, string lastName, string status, decimal totalPrice)
        {
            Id = id;
            PaymentMethodId = paymentMethod.Id;
            PaymentMethod = paymentMethod;
            AccountId = account.Id;
            Account = account;
            Address = address;
            FirstName = firstName;
            LastName = lastName;
            Status = status;
            TotalPrice = totalPrice;
            OrderItems = new List<OrderItem>();
        }

        public Order()
        {
            Id = 0;
            PaymentMethodId = 0;
            PaymentMethod = null!;
            AccountId = 0;
            Account = null!;
            Address = null!;
            FirstName = null!;
            LastName = null!;
            Status = "nová";
            TotalPrice = 0;
            OrderItems = new List<OrderItem>();
        }

        public Order(OrderCreateModel o, PaymentMethod paymentMethod, Account account, decimal totalPrice)
        {
            Id = 0;
            PaymentMethodId = paymentMethod.Id;
            PaymentMethod = paymentMethod;
            AccountId = account.Id;
            Account = account;
            Address = o.Address;
            FirstName = o.FirstName;
            LastName = o.FirstName;
            Status = "nová";
            TotalPrice = totalPrice;
            OrderItems = new List<OrderItem>();
        }
    }
}
