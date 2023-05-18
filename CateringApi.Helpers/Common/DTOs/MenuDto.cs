using System.ComponentModel.DataAnnotations;

namespace CateringApi.Helpers.Common.DTOs
{
	public class MenuDto
	{
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public IEnumerable<FoodItemDto> FoodItems { get; set; }
        public MenuDto()
        {
            FoodItems = Enumerable.Empty<FoodItemDto>();
        }
    }
}
