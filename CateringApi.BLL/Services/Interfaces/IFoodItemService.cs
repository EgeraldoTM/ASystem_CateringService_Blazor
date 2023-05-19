using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;
using CateringApi.Helpers.Common.Results;

namespace CateringApi.BLL.Services.Interfaces
{
	public interface IFoodItemService
	{
		Task<IEnumerable<FoodItemDto>> GetAll(string? query);
		Task<FoodItemDto> Get(int id);
		Task<FoodItemDto> Create(FoodItemDto foodItem);
		Task Update(int id, FoodItemDto foodItem);
		Task Delete(int id);
	}
}
