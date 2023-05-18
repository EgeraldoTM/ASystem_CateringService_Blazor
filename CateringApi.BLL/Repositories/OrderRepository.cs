using CateringApi.BLL.Repositories.Interfaces;
using CateringApi.DAL;
using CateringApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CateringApi.BLL.Repositories
{
	public class OrderRepository : Repository<Order>, IOrderRepository
	{
		public OrderRepository(ApplicationDbContext context) : base(context)
		{ }

		public async Task<Order?> GetFull(string employeeId, DateTime? date = null)
		{
			var filter = date == null ? DateTime.Now.Date : date.Value.Date;

			var order = await _context.Orders
			.Include(o => o.Details)
			.ThenInclude(d => d.FoodItem)
			.Where(o => o.OrderPlaced.Date == filter && o.EmployeeId == employeeId && o.IsDeleted == false)
			.FirstOrDefaultAsync();

			return order;
		}

		public async Task<Order?> GetWithDetails(int id)
		{
			var order = await _context.Orders
				.Include(o => o.Details)
				.Where(o => o.IsDeleted == false)
				.FirstOrDefaultAsync(o => o.Id == id);

			return order;
		}

		//public async Task<IEnumerable<Order>> GetAlllWithDetailsAsync()
		//{
		//	var orders = await _context.Orders
		//		.Include(o => o.Employee)
		//		.Include(o => o.Details)
		//		.Where(o => o.IsDeleted == false)
		//		.ToListAsync();

		//	return orders;
		//}
	}
}
