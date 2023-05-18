using System.ComponentModel.DataAnnotations;

namespace CateringApi.Helpers.Common.DTOs
{
	public class NewUserDto
	{
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		//public string RoleName { get; set; } = null!;

		[EmailAddress]
		public string Email { get; set; } = null!;

		[DataType(DataType.Date)]
		public DateTime Birthday { get; set; }

		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
	}
}
