using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

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

    public async Task<List<DeviceModel>> AddDevice(DeviceModel device)
    {
        var a = _context.DoorInformationUPDATED.FirstOrDefault(x => x.IMEI == device.IMEI);
        if (a != null)
        {
            return new List<DeviceModel>();
        }
        //device.DeviceId =DateTimeOffset.Now.ToUnixTimeMilliseconds();
        _context.DoorInformationUPDATED.Add(device);
        await _context.SaveChangesAsync();
        return await _context.DoorInformationUPDATED.ToListAsync();
    } 
    public async Task<List<OrderModel>> AddOrder(OrderModel order)
    {

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return await _context.Orders.ToListAsync();
    }

    public async Task<List<OrderHistory>> AddOrderHistory(OrderHistory order)
    {
        _context.OrderHistory.Add(order);
        await _context.SaveChangesAsync();
        return await _context.OrderHistory.ToListAsync();
    }

    public async Task<List<ListenerModel>> AddListenerModel(ListenerModel order)
    {
        _context.DoorInformation.Add(order);
        await _context.SaveChangesAsync();
        return await _context.DoorInformation.ToListAsync();
    }
}


