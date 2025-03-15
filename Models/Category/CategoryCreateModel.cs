namespace TechStoreMVC.Models.Category
{
    public class CategoryCreateModel
    {
        public int Id { get; set; }
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
