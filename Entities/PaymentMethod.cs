using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStoreMVC.Entities
{
    [Table("tbPaymentMethods")]
    public class PaymentMethod
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public PaymentMethod(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public PaymentMethod()
        {
            Id = 0;
            Name = string.Empty;
        }
    }
}
