using CateringApi.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CateringApi.DAL
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<FoodItem> FoodItems { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
		public DbSet<Order> Orders { get; set; } = null!;

		//public override int SaveChanges()
		//{
		//	var entries = ChangeTracker
		//	.Entries()
		//   .Where(e => e.State == EntityState.Deleted && e.Entity is FoodItem);

		//	foreach (var entry in entries)
		//	{
		//		var entity = (FoodItem)entry.Entity;
		//		entry.State = EntityState.Modified;
		//		entity.IsDeleted = true;
		//	}

		//		return base.SaveChanges();
		//}
	}
}
