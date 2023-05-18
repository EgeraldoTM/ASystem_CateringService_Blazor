namespace CateringApi.Helpers.Common.DTOs
{
	public class OrderDto
	{
        public int Id { get; set; }
        public DateTime OrderPlaced { get; set; }
        public string EmployeeId { get; set; } = null!;
        public IEnumerable<OrderDetailDto> Details { get; set; } = null!;
    }
}
