using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;

namespace CateringApi.BLL.Services.Interfaces
{
	public interface IMenuService
	{
		Task<MenuDto?> GetForSpecificDay(DateTime? date = null);
		Task<MenuDto?> Get(int id);
		Task Create(MenuRequest request);
		Task Update(int id, MenuRequest request);
		Task<bool> RemoveFoodItem (int  id, int foodItemId);
		Task Delete(int id);
	}
}
