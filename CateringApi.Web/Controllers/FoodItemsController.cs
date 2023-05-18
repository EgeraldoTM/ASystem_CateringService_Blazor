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
			var response = await _service.GetAllAsync(query);

			return Ok(response);
		}

		[HttpGet("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> Get(int id)
		{
			var response = await _service.GetAsync(id);

			return Ok(response);
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Cook)]

		public async Task<IActionResult> Create([FromBody] FoodItemDto foodItem)
		{
			var response = await _service.CreateAsync(foodItem);

			return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
		}

		[HttpPut("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Cook)]
		public async Task<IActionResult> Update(int id, [FromBody] FoodItemDto foodItem)
		{
			var response = await _service.UpdateAsync(id, foodItem);

			if (response)
				return NoContent();

			return Problem("Something went wrong, could not update the Food Item");
		}

		[HttpDelete("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Cook)]
		public async Task<IActionResult> Delete(int id)
		{
			var response = await _service.DeleteAsync(id);

			if (response)
				return NoContent();

			return Problem("Something went wrong, could not delete the Food Item");
		}
	}
}
