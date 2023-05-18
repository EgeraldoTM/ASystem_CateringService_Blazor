using Catering.WebAssembly.Authorization;
using CateringApi.Helpers.Common.DTOs;

namespace Catering.WebAssembly.Pages.Orders
{
    public partial class Order
	{
		private string message = string.Empty;
		private string token = string.Empty;
		private OrderDto? _order;
		private string employeeId = string.Empty;
		private DateTime? searchDate = DateTime.Now;

		protected async override Task OnInitializedAsync()
		{
			token = await SessionStorage.GetItemAsStringAsync("token");

			var customAuthState = (CustomAuthStateProvider)AuthState;
			employeeId = await customAuthState.GetUserId();

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

		protected async Task SearchForDay()
		{
			if (searchDate.HasValue)
			{
				var order = await OrderService.Get(employeeId, token, searchDate!.Value);

				if (order is not null)
				{
					_order = order;

					if (order.OrderPlaced != default(DateTime))
						searchDate = order.OrderPlaced;
				}
				else
					_order = null;
			}
		}
	}
}
