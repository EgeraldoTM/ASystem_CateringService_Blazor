using CateringApi.DAL.Models;

namespace CateringApi.BLL.Repositories.Interfaces
{
    public interface IFoodItemRepository : IRepository<FoodItem>
    {
        Task<FoodItem?> GetWithCategoryAsync(int id);
        Task<IEnumerable<FoodItem>> GetAllWithCategoryAsync(string? query);
    }
}
