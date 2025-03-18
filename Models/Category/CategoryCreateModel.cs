using System.ComponentModel.DataAnnotations;

namespace TechStoreMVC.Models.Category
{
    public class CategoryCreateModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public CategoryCreateModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public CategoryCreateModel()
        {
            Id = 0;
            Name = string.Empty;
        }
    }
}
