using System.Text.Json.Serialization;

namespace CateringApi.DAL.Models
{
	public class OrderDetail
	{
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public int FoodItemId { get; set; }
        public Order? Order { get; set; }
        public FoodItem? FoodItem { get; set; }
    }
}
