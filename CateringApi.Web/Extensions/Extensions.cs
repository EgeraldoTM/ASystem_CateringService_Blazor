using CateringApi.DAL;
using CateringApi.DAL.Seed;

namespace CateringApi.Web.Extensions
{
	public static class Extensions
	{
		public static void CreateDbIfNotExists(this IHost host)
		{
			using var scope = host.Services.CreateScope();
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<ApplicationDbContext>();
			context.Database.EnsureCreated();
			DbInitializer.Initialize(context);
		}
	}
}
