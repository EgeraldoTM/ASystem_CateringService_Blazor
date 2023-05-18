using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Catering.WebAssembly.Pages.Menus
{
	public partial class Edit
	{
		[Parameter]
		public int Id { get; set; }

		private string message = string.Empty;
		private MenuDto existingMenu = new();
		private MenuRequest menuRequest = new();
		private List<FoodItemDto> _foodItems = new();
		private List<int> foodIds = new();
		private string token = string.Empty;
		private ElementReference _selectReference;
		private string minDate = DateTime.Now.Date.ToString("yyyy-MM-dd");

		protected async override Task OnInitializedAsync()
		{
			token = await SessionStorage.GetItemAsStringAsync("token");

			var menuDto = await MenuService.Get(Id, token);

			if (menuDto is not null)
			{
				existingMenu = menuDto;
				menuRequest.Date = existingMenu.Date;

			}
			var items = await ItemService.GetAll(token);

			_foodItems = items.ToList();
		}

		protected async Task HandleValidRequest()
		{
			menuRequest.FoodIds = foodIds;

			var result = await MenuService.Update(Id, menuRequest, token);

			if (result)
				NavManager.NavigateTo("/Menu");
			else
				message = "Error occurred, could not update Menu.";
		}

		private void GoToMenu()
		{
			NavManager.NavigateTo("/menu");
		}

		protected async Task DeleteItem(int foodItemId)
		{
			bool confirmed = await Js.InvokeAsync<bool>("confirm", "Are you sure you want to remove this item from the menu?");

			if (confirmed)
			{
				var result = await MenuService.DeleteItem(Id, foodItemId, token);

				if (result)
					await Js.InvokeVoidAsync("deleteItem", foodItemId);
			}
		}

		private async Task OnSelectionChanged(ChangeEventArgs eventArgs)
		{
			var selectionList = new List<int>();
			var selection = await GetSelections(_selectReference);
			foreach (var id in selection)
			{
				int convertedId = int.Parse(id);
				selectionList.Add(convertedId);
			}
			foodIds = selectionList;
		}

		public async Task<HashSet<string>> GetSelections(ElementReference elementReference)
		{
			return (await Js.InvokeAsync<List<string>>("getSelectedValues", _selectReference)).ToHashSet();
		}
	}
}
