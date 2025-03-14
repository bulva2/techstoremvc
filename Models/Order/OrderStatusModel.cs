namespace TechStoreMVC.Models.Order
{
    public class OrderStatusModel
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public OrderStatusModel(int id, string status)
        {
            Id = id;
            Status = status;
        }

        public OrderStatusModel()
        {
            Id = 0;
            Status = string.Empty;
        }
    }
}
