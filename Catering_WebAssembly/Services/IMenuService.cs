using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;

namespace Catering_WebAssembly.Services
{
	public interface IMenuService
	{
		Task<MenuDto?> GetForToday(); 
		Task<MenuDto?> Create(MenuRequest request);
	}
}
