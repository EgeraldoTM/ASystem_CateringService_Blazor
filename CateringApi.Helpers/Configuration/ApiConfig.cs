namespace CateringApi.Helpers.Configuration
{
	public class ApiConfig
	{
        public string BaseUrl { get; set; } = null!;
        public string AuthEndpoint { get; set; } = null!;
        public string CategoriesEndpoint { get; set; } = null!;
        public string FoodItemsEndpoint { get; set; } = null!;
        public string MenusEndpoint { get; set; } = null!;
        public string OrdersEndpoint { get; set; } = null!;

    }
}
