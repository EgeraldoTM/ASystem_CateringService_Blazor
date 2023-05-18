using AutoMapper;
using CateringApi.DAL.Models;
using CateringApi.Helpers.Common.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CateringApi.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IMapper _mapper;

		public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IMapper mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var users = await _userManager.Users
				.Where(u => u.IsDeleted == false)
				.ToListAsync();

			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null || user.IsDeleted)
				return NotFound();

			//if(user.IsDeleted)
			//	return BadRequest("User is deleted");

			return Ok(user);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(string id, [FromBody] NewUserDto newUserDto)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
				return NotFound();

			_mapper.Map<User>(newUserDto);

			var result = await _userManager.UpdateAsync(user);

			if (result.Succeeded)
				return NoContent();

			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
				return NotFound();

			user.IsDeleted = true;

			var result = await _userManager.UpdateAsync(user);

			if (result.Succeeded)
				return NoContent();

			return StatusCode(StatusCodes.Status500InternalServerError);
		}
	}
}
