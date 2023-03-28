using WebAPI.Models.DeviceModel;

namespace WebAPI.Services.DeviceService;

public interface IDeviceService
{
	Task<List<OrderModel>> AddOrder(OrderModel order);                  //ამატებს ორდერს თეიბლში (შესაქმნელი)
	Task<List<OrderHistory>> AddOrderHistory(OrderHistory order);       //ხდება OrderHistory თებლში ორდერის დამატება (რას ნიშნავს ორდერი?)
	Task<List<DeviceModel>> GetAllDevices();                            //მოაქ ყველა მონაცემი განახლებადი თეიბლიდან
    Task<List<OrderModel>> GetAllOrders();                              //შესაქმნელია
    Task<List<OrderHistory>> GetOrderHistory();                         //მოაქ ბრძანებათა ისტორია (გასარკვევია)
    Task<List<ListenerModel>> GetListenerInfo();                        //ყველა მიღებული მონაცემის წამოღება
    //Task<List<DeviceModel>> AddDevice(DeviceModel device);              //ამატებს  ახალ დევაისს ბაზაში სადაც ხდება განახლება და თავზე გადაწერა შემოსული ინფორმაციის
    
    //Task<List<ListenerModel>> AddListenerModel(ListenerModel order);    //წერს იმ თეიბლში სადაც ყველა მონაცემია (?)
}