using System.ComponentModel.DataAnnotations;

namespace CateringApi.Shared.DTOs
{
	public class MenuDto
	{
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public IEnumerable<FoodItemDto> FoodItems { get; set; }
    }
}
