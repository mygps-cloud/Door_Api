using Microsoft.EntityFrameworkCore;
using WebAPI.Models.DeviceModel;
using WebAPI.Models.UserModel;

namespace WebAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<DeviceModel> DoorInformationUPDATED { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<OrderHistory> OrderHistory { get; set; }
    public DbSet<ListenerModel> DoorInformation { get; set; }
    public DbSet<UserModel>User { get; set; }
}