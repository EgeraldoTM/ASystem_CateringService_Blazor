using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Catering.WebAssembly.Pages.Menus
{
	public partial class Create
	{
		private string message = string.Empty;

		private MenuRequest _menu = new();
		private List<FoodItemDto> _foodItems = new();
		private List<int> foodIds = new();
		private string token = string.Empty;
		private ElementReference _selectReference;
		private readonly string minDate = DateTime.Now.Date.ToString("yyyy-MM-dd");

		protected override async Task OnInitializedAsync()
		{
			token = await SessionStorage.GetItemAsStringAsync("token");

			_menu.Date = DateTime.Now.Date;

			var items = await FoodItemService.GetAll(token);
			_foodItems = items.ToList();
		}

		protected async Task HandleValidRequest()
		{
			_menu.FoodIds = foodIds;

			var result = await MenuService.Create(_menu, token);

			if (result)
				NavigationManager.NavigateTo("/menu");
			else
				message = "Something unexpected happened, Menu could not be created.";
		}

		private void GoToMenu()
		{
			NavigationManager.NavigateTo("/menu");
		}

		//public void OptionClickEvent(int value, MouseEventArgs evnt)
		//{
		//	if (!foodIds.Contains(value))
		//	{
		//		foodIds.Add(value);
		//	}
		//	else
		//		foodIds.Remove(value);
		//}

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
