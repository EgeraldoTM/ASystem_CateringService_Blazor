using CateringApi.Helpers.Common.DTOs;

namespace Catering.WebAssembly.Pages.FoodItems
{
	public partial class FoodItems
	{
		private List<FoodItemDto>? foodItems = new();
		private string token = string.Empty;

		protected async override Task OnInitializedAsync()
		{
			token = await SessionStorage.GetItemAsStringAsync("token");

			var response = await FoodItemService.GetAll(token);

			foodItems = response.ToList();
		}
	}
}
