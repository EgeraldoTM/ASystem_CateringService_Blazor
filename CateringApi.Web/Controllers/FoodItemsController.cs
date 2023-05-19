using CateringApi.BLL.Services.Interfaces;
using CateringApi.Helpers;
using CateringApi.Helpers.Common.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CateringApi.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FoodItemsController : ControllerBase
	{
		private readonly IFoodItemService _service;

		public FoodItemsController(IFoodItemService service)
		{
			_service = service;
		}

		[HttpGet]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> GetAll(string? query = null)
		{
			var response = await _service.GetAll(query);

			return Ok(response);
		}

		[HttpGet("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> Get(int id)
		{
			var response = await _service.Get(id);

			return Ok(response);
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Cook)]

		public async Task<IActionResult> Create([FromBody] FoodItemDto foodItem)
		{
			var response = await _service.Create(foodItem);

			return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
		}

		[HttpPut("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Cook)]
		public async Task<IActionResult> Update(int id, [FromBody] FoodItemDto foodItem)
		{
			await _service.Update(id, foodItem);

			return NoContent();
		}

		[HttpDelete("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Cook)]
		public async Task<IActionResult> Delete(int id)
		{
			await _service.Delete(id);

			return NoContent();
		}
	}
}
