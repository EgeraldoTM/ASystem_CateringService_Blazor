using System.ComponentModel.DataAnnotations;

namespace CateringApi.Helpers.Common.Requests
{
    public class MenuRequest
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public List<int> FoodIds { get; set; }
        public MenuRequest()
        {
            FoodIds = new();
        }
    }
}
