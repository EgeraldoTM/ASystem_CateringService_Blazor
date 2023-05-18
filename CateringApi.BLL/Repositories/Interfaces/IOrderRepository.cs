using CateringApi.DAL.Models;

namespace CateringApi.BLL.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        //Task<Order?> GetWithDetailsAndFoodItemsAsync(string employeeId, DateTime? date = null);
        Task<Order?> GetWithDetails(int id);
        Task<Order?> GetFull(string employeeId, DateTime? date = null);
        //Task<IEnumerable<Order>> GetAlllWithDetailsAsync();
    }
}
