namespace WebAPI.Models.DeviceModel;

public class OrderHistory
{
    public int Id { get; set; }
    public long DeviceId { get; set; } = new Random().Next(0, 1000000);
	public int OrderId { get; set; } = new Random().Next(0,100000);
    public long Imei { get; set; }
    public string? OrderType { get; set; }
    public string? DeviceName { get; set; }= string.Empty;
    public short InSent { get; set; } = 0;
    public string ? OrderResult { get; set; }= string.Empty;

	public DateTime CreatedDate { get; set; } = DateTime.Now;
}