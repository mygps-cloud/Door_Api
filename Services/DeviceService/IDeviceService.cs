using WebAPI.Models;

namespace WebAPI.Services.DeviceService;

public interface IDeviceService
{
    Task<List<DeviceModel>> GetAllDevices();
    Task<List<OrderModel>> GetAllOrders();
    Task<List<OrderHistory>> GetOrderHistory();
    Task<List<ListenerModel>> GetListenerInfo();
    Task<List<DeviceModel>> AddDevice(DeviceModel device);
    Task<List<OrderModel>> AddOrder(OrderModel order);
    Task<List<OrderHistory>> AddOrderHistory(OrderHistory order);
    Task<List<ListenerModel>> AddListenerModel(ListenerModel order);
    
}