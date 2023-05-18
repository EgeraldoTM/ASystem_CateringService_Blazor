using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Results;

namespace Catering_WebAssembly.Services
{
	public interface IFoodItemService
	{
		Task<IEnumerable<FoodItemDto>> GetAll();
		Task<FoodItemDto> Get(int id);
		Task<FoodItemDto> Create(FoodItemDto foodItem);
		Task<bool> Update(FoodItemDto foodItem);
		Task<bool> Delete(int id);
	}
}
