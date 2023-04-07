using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.DeviceModel;

namespace WebAPI.Services.DeviceService;

public class DeviceService : IDeviceService
{
    private readonly DataContextDevice _context;

    public DeviceService(DataContextDevice context)
    {
        _context = context;
    }

    public async Task<List<OrderModel>> GetAllOrders()
    {
        var orders = await _context.Orders.ToListAsync();
        return orders;
    }

    public async Task<List<DeviceModel>> GetAllDevices()
    {
        var orders = await _context.DoorInformationUPDATED.ToListAsync();
        return orders;
    }

    public async Task<List<OrderHistory>> GetOrderHistory()
    {
        var orders = await _context.OrderHistory.ToListAsync();
        return orders;
    }

    public async Task<List<ListenerModel>> GetListenerInfo()
    {
        var orders = await _context.DoorInformation.ToListAsync();
        return orders;
    }

    //public async Task<List<DeviceModel>> AddDevice(DeviceModel device)
    //{
    //    var a = _context.DoorInformationUPDATED.FirstOrDefault(x => x.IMEI == device.IMEI);
    //    if (a != null)
    //    {
    //        return new List<DeviceModel>();
    //    }
    //    //device.DeviceId =DateTimeOffset.Now.ToUnixTimeMilliseconds();
    //    _context.DoorInformationUPDATED.Add(device);
    //    await _context.SaveChangesAsync();
    //    return await _context.DoorInformationUPDATED.ToListAsync();
    //} 
    public async Task<List<OrderModel>> AddOrder(OrderModel order)
    {
	    if (order == null || order.OrderType == null || order.OrderId == null /* add more properties as needed */)
	    {
		    return null;
	    }

		_context.Orders.Add(order);
		await _context.SaveChangesAsync();
		var postedData = await _context.Orders
			.Where(o => o.OrderId == order.OrderId)
			.ToListAsync();
		return postedData;
	}

	public async Task<List<string>> AddOrderHistory(OrderHistory order)
	{
		List<string> postedData = new List<string>();
		_context.OrderHistory.Add(order);
		await _context.SaveChangesAsync();

		while (true)
		{
			// retrieve order results from the database where DeviceId matches
			postedData = await _context.OrderHistory
				.Where(o => o.DeviceId == order.DeviceId)
				.Select(o => o.OrderResult)
				.ToListAsync();

			// check if any of the order results are non-empty
			bool hasNonEmptyOrderResult = postedData.Any(o => !string.IsNullOrEmpty(o));

			if (hasNonEmptyOrderResult)
			{
				// break out of the loop if at least one order result is non-empty
				break;
			}

			// sleep for some time before checking again
			await Task.Delay(TimeSpan.FromSeconds(1));
		}

		// return the list of non-empty order results
		return postedData.Where(o => !string.IsNullOrEmpty(o)).ToList();
	}


	//public async Task<List<ListenerModel>> AddListenerModel(ListenerModel order)
	//{
	//    _context.DoorInformation.Add(order);
	//    await _context.SaveChangesAsync();
	//    return await _context.DoorInformation.ToListAsync();
	//}
}


