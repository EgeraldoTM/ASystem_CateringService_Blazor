using System.ComponentModel.DataAnnotations;

namespace CateringApi.DAL.Models
{
	public class Menu
	{
        public int Id { get; set; }

		[DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<FoodItem> FoodItems { get; set; }

		public Menu()
		{
			FoodItems = new List<FoodItem>();
		}
	}
}
