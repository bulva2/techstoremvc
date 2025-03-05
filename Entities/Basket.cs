using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStoreMVC.Entities
{
    [Table("tbBaskets")]
    public class Basket
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("accountId")]
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public virtual List<BasketItem> BasketItems { get; set; }

        public Basket(int id, Account account)
        {
            Id = id;
            AccountId = account.Id;
            Account = account;
            BasketItems = new List<BasketItem>();
        }

        public Basket()
        {
            Id = 0;
            AccountId = 0;
            Account = null!;
            BasketItems = new List<BasketItem>();
        }
    }
}
