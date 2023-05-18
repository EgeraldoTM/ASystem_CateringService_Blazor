using CateringApi.BLL.Repositories.Interfaces;
using CateringApi.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CateringApi.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly IRepository<Category> _repository;

		public CategoriesController(IRepository<Category> repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var categories = await _repository.GetAllAsync();

			return Ok(categories);
		}
	}
}
