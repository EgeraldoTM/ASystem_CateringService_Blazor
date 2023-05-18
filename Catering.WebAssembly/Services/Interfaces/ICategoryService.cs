using CateringApi.Helpers.Common.DTOs;

namespace Catering.WebAssembly.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryDto>> GetAll();
	}
}
