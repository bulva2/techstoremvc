using TechStoreMVC.Entities;

namespace TechStoreMVC.Models.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
        public int Quantity { get; set; }

        public CategoryViewModel(int id, string name, List<Product> products)
        {
            Id = id;
            Name = name;
            Products = products;
        }

        public CategoryViewModel()
        {
            Id = 0;
            Name = null!;
            Products = new List<Product>();
        }
    }
}
