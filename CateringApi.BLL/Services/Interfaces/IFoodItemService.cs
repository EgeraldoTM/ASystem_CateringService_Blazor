using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;
using CateringApi.Helpers.Common.Results;

namespace CateringApi.BLL.Services.Interfaces
{
	public interface IFoodItemService
	{
		Task<IEnumerable<FoodItemDto>> GetAllAsync(string? query);
		Task<FoodItemDto> GetAsync(int id);
		Task<FoodItemDto> CreateAsync(FoodItemDto foodItem);
		Task<bool> UpdateAsync(int id, FoodItemDto foodItem);
		Task<bool> DeleteAsync(int id);
	}
}
