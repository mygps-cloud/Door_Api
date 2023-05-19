using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.DeviceModel;

namespace WebAPI.Services.DeviceService;

public class DeviceService : IDeviceService
{
    private readonly AppDbContext _context;
	private static System.Timers.Timer _timer;

    public DeviceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderModel>> GetAllOrders()
    {
        var orders = await _context.Orders.ToListAsync();
        if (orders == null || orders.Count == 0)
	        throw new ArgumentException("There is no data");
        return orders;
    }

    public async Task<List<DeviceModel>> GetAllDevices()
    {
        var orders = await _context.DoorInformationUPDATED.ToListAsync();

        if (orders == null || orders.Count == 0)
	        throw new ArgumentException("There is no data");

		return orders;
    }

    public async Task<List<OrderHistory>> GetOrderHistory()
    {
        var orders = await _context.OrderHistory.ToListAsync();
        if (orders == null || orders.Count == 0)
	        throw new ArgumentException("There is no data");

		return orders;
    }

    public async Task<List<ListenerModel>> GetListenerInfo()
    {
        var orders = await _context.DoorInformation.ToListAsync();
        if (orders == null || orders.Count == 0)
	        throw new ArgumentException("There is no data");

		return orders;
    }

    public async Task<string> GetStatus(long imei)
    {
        string status = "example";

        var DoorStatus = await _context.DoorInformationUPDATED
            .Where(i => i.IMEI == imei)
            .Select(n => new { n.DOOR_CLOSE, n.DOOR_OPEN, n.LEFT_DOOR, n.RIGHT_DOOR,n.DOOR_STATUS })
            .ToListAsync();


        if (DoorStatus[0].DOOR_STATUS == 1 && DoorStatus[0].DOOR_CLOSE == 1 && DoorStatus[0].DOOR_OPEN == 1)
            return "1";
        if (DoorStatus[0].DOOR_STATUS == 2)
            return "2";
        if (DoorStatus[0].DOOR_CLOSE == 2)
            return "3";
        if (DoorStatus[0].DOOR_OPEN == 2)
            return "4";
        return "Please Try Again";
    }
    public async Task<List<string?>> AddOrderHistory(OrderHistory order)
    {
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        var timeout = TimeSpan.FromSeconds(5);

        List<string?> postedData = new List<string?>();

        _context.OrderHistory.Add(order);
        if (await _context.SaveChangesAsync() == 0)
        {
            throw new DbUpdateException("Failed to remember. Try again");
        }

        var getDataTask = Task.Run(async () =>
        {
            while (true)
            {
                postedData = await _context.OrderHistory
                    .Where(o => o.DeviceId == order.DeviceId)
                    .Select(o => o.OrderResult)
                    .ToListAsync(cancellationToken: cancellationToken);

                bool hasNonEmptyOrderResult = postedData.Any(o => !string.IsNullOrEmpty(o));

                if (hasNonEmptyOrderResult)
                {
                    return postedData;
                }
            }
        }, cancellationToken);

        var completedTask = await Task.WhenAny(getDataTask, Task.Delay(timeout, cancellationToken));

        if (completedTask != getDataTask)
        {
            cancellationTokenSource.Cancel();
            throw new TimeoutException("The command failed. Please try again");
        }

        return postedData.Where(o => !string.IsNullOrEmpty(o)).ToList();
    }

}


