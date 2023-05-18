using Catering.WebAssembly.Authorization;
using CateringApi.Helpers.Common.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Catering.WebAssembly.Pages.Orders
{
    public partial class Edit
	{
		[Parameter]
		public int Id { get; set; }
		private string token = string.Empty;
		private string message = string.Empty;
		private OrderDto? _order;
		private decimal total
		{
			get
			{
				decimal result = 0m;
				if (_order is not null)
					foreach (var detail in _order.Details)
						result += detail.Price * detail.Quantity;
				return result;
			}
		}

		protected async override Task OnInitializedAsync()
		{
			token = await SessionStorage.GetItemAsStringAsync("token");

			var customAuthState = (CustomAuthStateProvider)AuthState;
			var employeeId = await customAuthState.GetUserId();

			if (!string.IsNullOrEmpty(employeeId))
			{
				var order = await OrderService.Get(employeeId, token);
				if (order is not null)
					_order = order;
			}
			else
			{
				message = "Error occurred, please try again later.";
			}
		}

		//protected override void OnParametersSet()
		//{
		//	if (Order is not null)
		//		foreach (var detail in Order.Details)
		//			total += detail.Price * detail.Quantity;
		//}

		protected async Task AddQuantity(int detailId)
		{
			var result = await OrderService.AddQuantity(_order!.Id, detailId, token);

			if (result)
			{
				var detail = _order!.Details.First(d => d.Id == detailId);
				detail.Quantity++;
			}
		}

		protected async Task SubtractQuantity(int detailId)
		{
			var result = await OrderService.SubtractQuantity(_order!.Id, detailId, token);

			if (result)
			{
				var detail = _order!.Details.First(d => d.Id == detailId);
				detail.Quantity--;
			}
		}

		protected async Task RemoveDetail(int detailId)
		{
			bool confirmed = await Js.InvokeAsync<bool>("confirm", "Are you sure you want to remove this item from order?");

			if (confirmed)
			{
				var result = await OrderService.RemoveDetail(_order!.Id, detailId, token);

				if (result)
				{
					await Js.InvokeVoidAsync("deleteItem", detailId);
				}
			}
		}

		protected async Task DeleteOrder()
		{
			bool confirmed = await Js.InvokeAsync<bool>("confirm", "Are you sure you want to delete the menu?");

			if (confirmed)
			{
				var result = await OrderService.Delete(_order.Id, token);

				if (!result)
					message = "Error occurred, could not delete menu";

				NavManager.NavigateTo("/order", true);
			}
		}
	}
}
