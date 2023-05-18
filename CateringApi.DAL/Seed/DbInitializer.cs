using CateringApi.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace CateringApi.DAL.Seed
{
	public static class DbInitializer
	{
		public static void Initialize(ApplicationDbContext context)
		{
			if (context.Categories.Any() && context.Roles.Any())
				return; //DB has been seeded

			var categories = new Category[]
			{
				new Category { Id = 1, Name = "Salads" },
				new Category { Id = 2, Name = "Entrees" },
				new Category { Id = 3, Name = "Deserts" },
				new Category { Id = 4, Name = "Beverages" },
				new Category { Id = 5, Name = "Fruits" },
				new Category { Id = 5, Name = "Fruits" }
			};

			var employeeRole = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "employee" };
			var cookRole = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "cook" };
			//var admin = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "admin" };

			var roles = new IdentityRole[]
			{
				cookRole,
				employeeRole
			};

			var hasher = new PasswordHasher<User>();
			var cook = new User { Id = Guid.NewGuid().ToString(), FirstName = "Cook", LastName = string.Empty, Birthday = new DateTime(1980, 06, 05) };
			cook.PasswordHash = hasher.HashPassword(cook, "Coding@2023");

			var userRole = new IdentityUserRole<string> { UserId = cook.Id, RoleId = cookRole.Id };

			context.Categories.AddRange(categories);
			context.Roles.AddRange(roles);
			context.Users.Add(cook);
			context.UserRoles.Add(userRole);
		}
	}
}
