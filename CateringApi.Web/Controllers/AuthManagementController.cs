using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CateringApi.DAL.Models;
using CateringApi.Helpers;
using CateringApi.Helpers.Common.Requests;
using CateringApi.Helpers.Common.Results;
using CateringApi.Helpers.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CateringApi.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthManagementController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly JwtConfig _jwtConfig;

		public AuthManagementController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtConfig> options)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_jwtConfig = options.Value;
		}

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] UserRegistrationRequest user)
		{
			if (ModelState.IsValid)
			{
				var existingUser = await _userManager.FindByEmailAsync(user.Email);
				if (existingUser != null)
				{
					return BadRequest(new AuthResult
					{
						Errors = new List<string> { "Email already in use" },
						Success = false
					});
				}
				var newUser = new User
				{
					FirstName = user.FirstName,
					LastName = user.LastName,
					Birthday = user.Birthday,
					Email = user.Email,
					UserName = user.Email
				};
				var isCreated = await _userManager.CreateAsync(newUser, user.Password);

				if (isCreated.Succeeded)
				{
					await _userManager.AddToRoleAsync(newUser, RoleName.Employee);

					var jwtToken = await GenerateJwtToken(newUser);

					return Ok(new AuthResult { Token = jwtToken, Success = true});
				}

				return BadRequest(new AuthResult
				{
					Errors = isCreated.Errors.Select(e => e.Description).ToList(),
					Success = false
				});
			}

			return BadRequest(new AuthResult
			{
				Errors = new List<string> { "Invalid payload" },
				Success = false
			});
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
		{
			if (ModelState.IsValid)
			{
				var existingUser = await _userManager.FindByEmailAsync(user.Email);

				if (existingUser == null)
				{
					return BadRequest(new AuthResult
					{
						Errors = new List<string> { "Invalid login request" },
						Success = false
					});
				}

				var validPassword = await _userManager.CheckPasswordAsync(existingUser, user.Password);

				if (!validPassword)
				{
					return BadRequest(new AuthResult
					{
						Errors = new List<string> { "Invalid login request" },
						Success = false
					});
				}

				var jwtToken = await GenerateJwtToken(existingUser);

				return Ok(new AuthResult { Token = jwtToken, Success = true });
			}

			return BadRequest(new AuthResult
			{
				Errors = new List<string> { "Invalid payload" },
				Success = false
			});
		}

		private async Task<string> GenerateJwtToken(User user)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();

			var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

			var claims = await GetAllValidClaims(user);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(6),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
			};

			var token = jwtTokenHandler.CreateToken(tokenDescriptor);

			var jwtToken = jwtTokenHandler.WriteToken(token);

			return jwtToken;
		}

		private async Task<List<Claim>> GetAllValidClaims(User user)
		{
			var options = new IdentityOptions();

			var claims = new List<Claim>
			{
					new Claim("Id", user.Id),
					new Claim(JwtRegisteredClaimNames.Email, user.Email!),
					new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
					new Claim(JwtRegisteredClaimNames.Name, user.FirstName!),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var userClaims = await _userManager.GetClaimsAsync(user);
			claims.AddRange(userClaims);

			var userRoles = await _userManager.GetRolesAsync(user);

			foreach (var userRole in userRoles)
			{
				var role = await _roleManager.FindByNameAsync(userRole);

				if (role != null)
				{
					claims.Add(new Claim(ClaimTypes.Role, userRole));

					var roleClaims = await _roleManager.GetClaimsAsync(role);
					foreach (var roleClaim in roleClaims)
						claims.Add(roleClaim);
				}
			}

			return claims;
		}
	}
}
