namespace WebAPI.Models.DeviceModel;

public class ListenerModel
{

    public int ID { get; set; }
    public char? STARTBYTE { get; set; }
    public long IMEI { get; set; }
    public string? DATE { get; set; }
    public short? ADMISSON_LEVEL { get; set; }
    public short? PING { get; set; }
    public short? DOOR_STATUS { get; set; }
    public short? DOOR_CLOSE { get; set; }
    public short? DOOR_OPEN { get; set; }
    public short? RIGHT_DOOR { get; set; }
    public short? LEFT_DOOR { get; set; }
    public DateTime RECORDING_TIME { get; set; } = DateTime.Now;


}