namespace WebAPI.Models.DeviceModel
{
    public class OrderModel
    {
        public int Id { get; set; }
        public long OrderId { get; set; }
        public string? OrderType { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
