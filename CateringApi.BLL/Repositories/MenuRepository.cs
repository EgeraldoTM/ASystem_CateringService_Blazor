using CateringApi.BLL.Repositories.Interfaces;
using CateringApi.DAL;
using CateringApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CateringApi.BLL.Repositories
{
	public class MenuRepository : Repository<Menu>, IMenuRepository
	{
		public MenuRepository(ApplicationDbContext context) : base(context)
		{ }

		public async Task<Menu?> GetForSpecificDay(DateTime? date = null)
		{
			var filter = date == null ? DateTime.Now.Date : date.Value.Date;

			var menu = await _context.Menus
			.Include(m => m.FoodItems.Where(f => f.IsDeleted == false))
			.ThenInclude(f => f.Category)
		  .FirstOrDefaultAsync(m => m.Date.Date == filter && m.IsDeleted == false);

			return menu;
		}

		public async Task<Menu?> GetWithFoodItems(int id)
		{
			return await _context.Menus
				.Include(m => m.FoodItems.Where(f => f.IsDeleted == false))
				.FirstOrDefaultAsync(m => m.Id == id && m.IsDeleted == false);
		}

		public async Task<IEnumerable<Menu>> GetAllWithFoodItems()
		{
			return await _context.Menus
				.Include(m => m.FoodItems.Where(f => f.IsDeleted == false))
				.ThenInclude(f => f.Category)
				.Where(m => m.IsDeleted == false)
				.ToListAsync();
		}
	}
}
