using System.ComponentModel.DataAnnotations;

namespace CateringApi.DAL.Models
{
	public class FoodItem
	{
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Menu>? Menus { get; set; }
    }
}
