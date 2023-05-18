using System.Text.Json.Serialization;

namespace CateringApi.Helpers.Common.DTOs
{
	public class OrderDetailDto
	{
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public int FoodItemId { get; set; }

        [JsonIgnore]
        public OrderDto Order { get; set; } = null!;
        public FoodItemDto FoodItem { get; set; } = null!;
    }
}