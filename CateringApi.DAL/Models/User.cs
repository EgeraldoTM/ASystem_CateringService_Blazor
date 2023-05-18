using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CateringApi.DAL.Models
{
	public class User : IdentityUser
	{
        [StringLength(50)]
		public string FirstName { get; set; } = null!;

		[StringLength(50)]
		public string LastName { get; set; } = null!;

		[DataType(DataType.Date)]
		public DateTime Birthday { get; set; }

		[DataType(DataType.Date)]
		public DateTime RegistrationDate { get; set; } = DateTime.Now.Date;
		public bool IsDeleted { get; set; }
		public ICollection<Order>? Orders { get; set; }
	}
}
