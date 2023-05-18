namespace CateringApi.DAL.Models
{
	public class Order
	{
        public int Id { get; set; }
        public DateTime OrderPlaced { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public string EmployeeId { get; set; } = null!;
        public User Employee { get; set; } = null!;
        public ICollection<OrderDetail> Details { get; set; } = null!;
    }
}
