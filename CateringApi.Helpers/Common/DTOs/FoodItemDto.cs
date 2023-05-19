using System.ComponentModel.DataAnnotations;

namespace CateringApi.Helpers.Common.DTOs
{
	public class FoodItemDto
	{
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = null!;

        [Range(0, 200)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue - 10, ErrorMessage = "Please select a valid category")]
        public int CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
    }
}
