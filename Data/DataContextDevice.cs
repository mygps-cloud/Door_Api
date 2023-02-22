using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data;

public class DataContextDevice : DbContext
{
    public DataContextDevice(DbContextOptions<DataContextDevice> options) : base(options)
    {
    }
    public DbSet<DeviceModel> DoorInformationUPDATED { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<OrderHistory> OrderHistory { get; set; }
    public DbSet<ListenerModel> DoorInformation { get; set; }
}