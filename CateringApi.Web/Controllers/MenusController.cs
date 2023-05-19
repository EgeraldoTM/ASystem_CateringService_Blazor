using CateringApi.BLL.Services.Interfaces;
using CateringApi.Helpers;
using CateringApi.Helpers.Common.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CateringApi.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MenusController : ControllerBase
	{
		private readonly IMenuService _service;

		public MenusController(IMenuService service)
		{
			_service = service;
		}

		[HttpGet("/api/Menus/forSpecificDay")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> GetMenuForToday(DateTime? date)
		{
			var menu = await _service.GetForSpecificDay(date);

			return Ok(menu);
		}

		[HttpGet("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> Get(int id)
		{
			var result = await _service.Get(id);

			return Ok(result);
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Cook)]
		public async Task<IActionResult> Create([FromBody] MenuRequest menu)
		{
			if (menu.Date.Date < DateTime.Now.Date)
				return BadRequest("Cannot create Menu for a day in the past");

			await _service.Create(menu);

			return Ok();
		}

		[HttpPut("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Cook)]
		public async Task<IActionResult> Update(int id, [FromBody] MenuRequest menu)
		{
			await _service.Update(id, menu);

			return NoContent();
		}

		[HttpPut("{id}/RemoveFoodItem")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Cook)]
		public async Task<IActionResult> RemoveFoodItem(int id, [FromBody] int foodItemId)
		{
			var result = await _service.RemoveFoodItem(id, foodItemId);

			if (result)
				return NoContent();

			return Problem("Something went wrong, could not remove food item.");
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
