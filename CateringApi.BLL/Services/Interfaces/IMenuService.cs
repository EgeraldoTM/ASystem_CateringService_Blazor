using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;

namespace CateringApi.BLL.Services.Interfaces
{
	public interface IMenuService
	{
		Task<MenuDto?> GetForSpecificDay(DateTime? date = null);
		Task<MenuDto?> Get(int id);
		Task<bool> Create(MenuRequest request);
		Task<bool> Update(int id, MenuRequest request);
		Task<bool> RemoveFoodItem (int  id, int foodItemId);
		Task<bool> Delete(int id);
	}
}
