namespace CateringApi.Helpers.Common.Requests
{
    public class UpdateOrderRequest
    {
        public IEnumerable<OrderDetailRequest> Details { get; set; } = null!;
    }
}
