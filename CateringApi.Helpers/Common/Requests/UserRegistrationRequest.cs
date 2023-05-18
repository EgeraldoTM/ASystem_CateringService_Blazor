using System.ComponentModel.DataAnnotations;

namespace CateringApi.Helpers.Common.Requests
{
    public class UserRegistrationRequest
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
