using CateringApi.Helpers.Common.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Catering.WebAssembly.Pages.FoodItems
{
	public partial class Details
	{
		[Parameter]
		public int? Id { get; set; }

		private string message = string.Empty;

		private FoodItemDto _foodItem = new();

		private List<CategoryDto> _categories = new();

		private string token = string.Empty;

		protected async override Task OnInitializedAsync()
		{
			token = await SessionStorage.GetItemAsStringAsync("token");

			var categories = await CategoryService.GetAll();
			_categories = categories.ToList();

			if (Id is not null)
			{
				var foodItemDto = await FoodItemService.Get(Id.Value, token);

				if (foodItemDto is not null)
					_foodItem = foodItemDto;
			}
		}

		protected void GoToFoodItems()
		{
			NavigationManager.NavigateTo("/FoodItems");
		}

		protected async Task DeleteItem()
		{
			bool confirmed = await Js.InvokeAsync<bool>("confirm", "Are you sure you want to delete this item?");

			if (confirmed)
			{
				if (Id is not null)
				{
					var result = await FoodItemService.Delete(Id.Value, token);

					if (result)
						NavigationManager.NavigateTo("/fooditems");
					else
						message = "Something unexpected happened, could not delete item.";
				}
			}
		}

		protected async Task HandleValidRequest()
		{
			if (Id is null)
			{
				var result = await FoodItemService.Create(_foodItem, token);

				if (result)
					NavigationManager.NavigateTo("/fooditems");
				else
					message = "Something went wrong, Item could not be added.";

			}
			else
			{
				var result = await FoodItemService.Update(_foodItem, token);

				if (result)
					NavigationManager.NavigateTo("/fooditems");
				else
					message = "Something went wrong, Item could not be updated.";
			}
		}
	}
}
