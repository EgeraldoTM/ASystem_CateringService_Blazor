using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;

namespace Catering.WebAssembly.Services.Interfaces
{
	public interface IOrderService
	{
		Task<OrderDto?> Get(string employeeId, string token, DateTime? date =  null);
		Task<bool> Create(CreateOrderRequest request, string token);
		Task<bool> Update(int id, UpdateOrderRequest request, string token);
		Task<bool> AddQuantity(int id, int detailId, string token);
		Task<bool> SubtractQuantity(int id, int detailId, string token);
		Task<bool> RemoveDetail(int id, int detailId, string token);
		Task<bool> Delete(int id, string token);
	}
}
