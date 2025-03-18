namespace TechStoreMVC.Models.Basket
{
    public class OrderCreateModel
    {
        public int Id { get; set; }
        public int PaymentMethodId { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
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
