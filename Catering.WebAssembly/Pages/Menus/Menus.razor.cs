using Catering.WebAssembly.Authorization;
using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;
using Microsoft.JSInterop;

namespace Catering.WebAssembly.Pages.Menus
{
    public partial class Menus
	{
		private string message = string.Empty;
		private MenuDto _menu = new();
		private DateTime? searchDate = DateTime.Now;
		private string token = string.Empty;
		private CreateOrderRequest order = new();
		private Dictionary<int, int> detailQuantity = new();

		protected async override Task OnInitializedAsync()
		{
			token = await SessionStorage.GetItemAsStringAsync("token");

			var menu = await MenuService.GetForSpecificDay(token);

			if (menu is not null)
			{
				_menu = menu;
				foreach (var item in menu.FoodItems)
					detailQuantity[item.Id] = 0;
			}
		}

		protected async Task SearchForDay()
		{
			if (searchDate.HasValue)
			{
				var menu = await MenuService.GetForSpecificDay(token, searchDate!.Value);

				if (menu is not null)
				{
					_menu = menu;

					if (menu.Date != default(DateTime))
						searchDate = menu.Date;
				}
			}
		}

		//protected async override void OnAfterRender(bool firstRender)
		//{
		//	await Js.InvokeVoidAsync("hideIndicator");
		//}

		protected async Task CreateOrder()
		{
			var customAuthState = (CustomAuthStateProvider)AuthState;
			var employeeId = await customAuthState.GetUserId();

			var orderInDb = await OrderService.Get(token, employeeId);
			if (orderInDb is null)
			{
				order.EmployeeId = employeeId;
				var result = await OrderService.Create(order, token);

				if (result)
					NavManager.NavigateTo("/order");
				else
					message = "Could not place order, please try again later";
			}
			else
			{
				var updateOrder = new UpdateOrderRequest { Details = order.Details };
				var result = await OrderService.Update(orderInDb.Id, updateOrder, token);

				if (result)
					NavManager.NavigateTo("/order");
				else
					message = "Could not place order, please try again later";
			}
		}

		protected void AddDetail(int foodItemId, decimal price)
		{
			detailQuantity[foodItemId]++;
			var detail = order.Details.FirstOrDefault(d => d.FoodItemId == foodItemId);
			if (detail is not null)
			{
				detail.Quantity++;
			}
			else
			{
				order.Details.Add(new OrderDetailRequest { FoodItemId = foodItemId, Price = price, Quantity = 1 });
			}
		}

		protected void RemoveDetail(int foodItemId)
		{
			var detail = order.Details.FirstOrDefault(d => d.FoodItemId == foodItemId);
			if (detail is not null)
			{
				if (detail.Quantity >= 2)
				{
					detail.Quantity--;
				}
				else
				{
					order.Details.Remove(detail);
				}
			}
		}

		protected async Task DeleteMenu()
		{
			bool confirmed = await Js.InvokeAsync<bool>("confirm", "Are you sure you want to delete the menu?");

			if (confirmed)
			{
				var result = await MenuService.Delete(_menu.Id, token);

				if (result)
					NavManager.NavigateTo("/Menu", true);
				else
					message = "Error occurred, could not delete menu";
			}
		}
	}
}
