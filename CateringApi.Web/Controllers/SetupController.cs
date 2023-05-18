using CateringApi.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CateringApi.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SetupController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public SetupController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[HttpGet]
		[Route("GetAllRoles")]
		public async Task<IActionResult> GetAllRoles()
		{
			var roles = await _roleManager.Roles.ToListAsync();

			return Ok(roles);
		}

		[HttpPost]
		[Route("CreateRole")]
		public async Task<IActionResult> CreateRole(string name)
		{
			var roleExists = await _roleManager.RoleExistsAsync(name);

			if (!roleExists)
			{
				var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));

				if (roleResult.Succeeded)
					return Ok(new { result = $"The role {name} has been added successfully" });

				return BadRequest(new { error = $"Error occurred. The role {name} has not been added." });
			}

			return BadRequest(new { error = "Role already exists" });
		}

		[HttpGet]
		[Route("GetAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _userManager.Users.ToListAsync();

			return Ok(users);
		}

		[HttpPost]
		[Route("AddUserToRole")]
		public async Task<IActionResult> AddUserToRole(string email, string roleName)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user == null)
				return BadRequest(new { error = "User does not exist." });

			var roleExists = await _roleManager.RoleExistsAsync(roleName);

			if (!roleExists)
				return BadRequest(new { error = "Role does not exist" });

			var result = await _userManager.AddToRoleAsync(user, roleName);

			if(result.Succeeded)
				return Ok(new { result = "User successfully assigned to role." });
 
			return BadRequest(new { error = "Error occurred. User could not be assigned to role." });
		}

		[HttpGet]
		[Route("GetUserRoles")]
		public async Task<IActionResult> GetUserRoles(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user == null)
				return BadRequest(new { error = "User does not exist." });

			var roles = await _userManager.GetRolesAsync(user);

			return Ok(roles);
		}

		[HttpPost]
		[Route("RemoveUserFromRole")]
		public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user == null)
				return BadRequest(new { error = "User does not exist." });

			var roleExists = await _roleManager.RoleExistsAsync(roleName);

			if (!roleExists)
				return BadRequest(new { error = "Role does not exist" });

			var result = await _userManager.RemoveFromRoleAsync(user, roleName);

			if (result.Succeeded)
				return Ok(new { result = $"User {email} has been removed from role {roleName}." });

			return BadRequest(new { error = $"Error occurred. Unable to remove user {email} from role {roleName}." });
		}
	}
}
