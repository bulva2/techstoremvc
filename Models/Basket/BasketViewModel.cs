using TechStoreMVC.Entities;

namespace TechStoreMVC.Models.Basket
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public virtual List<BasketItem> BasketItems { get; set; }

        public BasketViewModel(int id, List<BasketItem> items)
        {
            Id = id;
            BasketItems = items;
        }
        public BasketViewModel()
        {
            Id = 0;
            BasketItems = new List<BasketItem>();
        }
    }
}
