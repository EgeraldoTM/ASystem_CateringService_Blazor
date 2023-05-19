using CateringApi.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.DependencyInjection;

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
				new Category {  Name = "Salads" },
				new Category {  Name = "Entrees" },
				new Category { Name = "Deserts" },
				new Category { Name = "Beverages" },
				new Category { Name = "Fruits" }
			};

			//var adminRole = "admin";

			var roles = new string[] { "cook", "employee" };

			foreach (var role in roles)
			{
				var roleStore = new RoleStore<IdentityRole>(context);
				roleStore.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToUpper()});
			}

			var cook = new User
			{
				FirstName = "Cook",
				LastName = string.Empty,
				Email = "cook@catering.com",
				NormalizedEmail = "COOK@CATERING.COM",
				UserName = "chef",
				NormalizedUserName = "CHEF",
				EmailConfirmed = true,
				Birthday = new DateTime(1980, 06, 05),
				SecurityStamp = Guid.NewGuid().ToString("D")
			};
			var hasher = new PasswordHasher<User>();
			cook.PasswordHash = hasher.HashPassword(cook, "Coding@2023");

			var userStore = new UserStore<User>(context);
			userStore.CreateAsync(cook);
			userStore.AddToRoleAsync(cook, roles[0]);

			context.Categories.AddRange(categories);

			context.SaveChanges();
		}
	}
}
