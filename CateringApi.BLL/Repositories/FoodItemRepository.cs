using CateringApi.BLL.Repositories.Interfaces;
using CateringApi.DAL;
using CateringApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CateringApi.BLL.Repositories
{
	public class FoodItemRepository : Repository<FoodItem>, IFoodItemRepository
	{
		public FoodItemRepository(ApplicationDbContext context) : base(context)
		{ }

		public async Task<IEnumerable<FoodItem>> GetAllWithCategoryAsync(string? query)
		{
			var foodItems = _context.FoodItems
				.Include(f => f.Category)
				.Where(f => f.IsDeleted == false);

			if (!string.IsNullOrWhiteSpace(query))
				foodItems = foodItems.Where(f => f.Name.Contains(query));

			return await foodItems.ToListAsync();
		}

		public async Task<FoodItem?> GetWithCategoryAsync(int id)
		{
			var foodItem = await _context.FoodItems
				.Include(f => f.Category)
				.Where(f => f.IsDeleted == false)
				.FirstOrDefaultAsync(f => f.Id == id);

			return foodItem;
		}
	}
}
