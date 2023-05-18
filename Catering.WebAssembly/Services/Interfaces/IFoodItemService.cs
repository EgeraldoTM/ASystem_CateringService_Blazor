using CateringApi.Helpers.Common.DTOs;

namespace Catering.WebAssembly.Services.Interfaces
{
	public interface IFoodItemService
    {
        Task<IEnumerable<FoodItemDto>> GetAll(string token);
        Task<FoodItemDto?> Get(int id, string token);
        Task<bool> Create(FoodItemDto foodItem, string token);
        Task<bool> Update(FoodItemDto foodItem, string token);
        Task<bool> Delete(int id, string token);
    }
}
