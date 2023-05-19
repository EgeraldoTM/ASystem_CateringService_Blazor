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
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _service;

		public OrdersController(IOrderService service)
		{
			_service = service;
		}

		[HttpGet]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Employee)]
		public async Task<IActionResult> Get(string employeeId, DateTime? date = null)
		{
			var order = await _service.Get(employeeId, date);
			//var order = await _repository.GetWithDetailsAsync(id);

			if (order == null)
				return NotFound("No order found");

			return Ok(order);
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Employee)]
		public async Task<IActionResult> Create([FromBody] CreateOrderRequest order)
		{
			var result = await _service.Create(order);

			if (result)
				return Ok();

			return Problem("Something went wrong, could not create Order");
		}

		[HttpPut("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Employee)]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderRequest order)
		{
			var response = await _service.Update(id, order);

			if (response)
				return NoContent();

			return Problem("Something went wrong, could not update Order.");
		}

		[HttpPut("{id}/AddQuantity")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Employee)]
		public async Task<IActionResult> AddQuantity(int id, [FromBody] int detailId)
		{
			var result = await _service.AddQuantity(id, detailId);

			if (result)
				return NoContent();

			return Problem("Something went wrong, could not update Order.");
		}
		
		[HttpPut("{id}/SubtractQuantity")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Employee)]
		public async Task<IActionResult> SubtractQuantity(int id, [FromBody] int detailId)
		{
			var result = await _service.SubtractQuantity(id, detailId);

			if (result)
				return NoContent();

			return Problem("Something went wrong, could not update Order.");
		}

		[HttpPut("{id}/RemoveDetail")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Employee)]
		public async Task<IActionResult> RemoveDetail(int id, [FromBody] int detailId)
		{
			var result = await _service.RemoveDetail(id, detailId);

			if(result)
				return NoContent();

			return Problem("Something went wrong, could not update Order.");
		}

		[HttpDelete("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleName.Employee)]
		public async Task<IActionResult> Delete(int id)
		{
			var response = await _service.Delete(id);

			if (response)
				return NoContent();

			return Problem("Something went wrong, could not delete Order");
		}
	}
}

