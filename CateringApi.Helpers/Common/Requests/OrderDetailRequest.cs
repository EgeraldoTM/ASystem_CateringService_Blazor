namespace CateringApi.Helpers.Common.Requests
{
    public class OrderDetailRequest
    {
        public int FoodItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
