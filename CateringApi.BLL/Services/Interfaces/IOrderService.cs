using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;

namespace CateringApi.BLL.Services.Interfaces
{
	public interface IOrderService
	{
		Task<OrderDto?> Get(string employeeId, DateTime? date = null);
		Task<bool> Create(CreateOrderRequest request);
		Task<bool> Update(int id, UpdateOrderRequest request);
		Task<bool> AddQuantity(int id, int detailId);
		Task<bool> SubtractQuantity(int id, int detailId);
		Task<bool> RemoveDetail(int id, int detailId);
		Task<bool> Delete(int id);
	}
}
