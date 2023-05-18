namespace CateringApi.Helpers.Common.Requests
{
    public class CreateOrderRequest
    {
        public string EmployeeId { get; set; } = null!;
        public List<OrderDetailRequest> Details { get; set; } = null!;

        public CreateOrderRequest()
        {
            Details = new();
        }
    }
}
