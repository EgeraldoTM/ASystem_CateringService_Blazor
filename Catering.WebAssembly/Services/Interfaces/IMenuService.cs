using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;

namespace Catering.WebAssembly.Services.Interfaces
{
    public interface IMenuService
    {
        Task<MenuDto?> GetForSpecificDay(string token, DateTime? date = null);
        Task<MenuDto?> Get(int id, string token);
        Task<bool> Create(MenuRequest request, string token);
        Task<bool> Update(int id, MenuRequest request, string token);
        Task<bool> DeleteItem(int id, int foodItemId, string token);
        Task<bool> Delete(int id, string token);
    }
}
