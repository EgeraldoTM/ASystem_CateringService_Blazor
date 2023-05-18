using CateringApi.DAL.Models;

namespace CateringApi.BLL.Repositories.Interfaces
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<Menu?> GetWithFoodItems(int id);
        Task<Menu?> GetForSpecificDay(DateTime? date = null);
        Task<IEnumerable<Menu>> GetAllWithFoodItems();

    }
}
