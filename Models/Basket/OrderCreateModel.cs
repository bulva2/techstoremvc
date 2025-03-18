using System.ComponentModel.DataAnnotations;

namespace TechStoreMVC.Models.Basket
{
    public class OrderCreateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PaymentMethodId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Email { get; set; }

        public OrderCreateModel(int id, int paymentMethodId, string address, string firstName, string lastName, string email)
        {
            Id = id;
            PaymentMethodId = paymentMethodId;
            Address = address;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public OrderCreateModel()
        {
            Id = 0;
            PaymentMethodId = 0;
            Address = null!;
            FirstName = null!;
            LastName = null!;
            Email = null!;
        }

        public OrderCreateModel(string email)
        {
            Id = 0;
            PaymentMethodId = 0;
            Address = null!;
            FirstName = null!;
            LastName = null!;
            Email = email;
        }
    }
}
