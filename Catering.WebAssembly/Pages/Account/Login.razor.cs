using Catering.WebAssembly.Authorization;
using CateringApi.Helpers.Common.Requests;

namespace Catering.WebAssembly.Pages.Account
{
    public partial class Login
	{
		private UserLoginRequest user = new();
		private string message = string.Empty;

		protected async Task HandleValidRequest()
		{
			var result = await AuthService.Login(user);

			if (result.Success)
			{
				var token = result.Token;
				await SessionStorage.SetItemAsStringAsync("token", result.Token);
				var customAuthStateProvider = (CustomAuthStateProvider)AuthStateProvider;
				await customAuthStateProvider.UpdateAuthenticationState(token);
				NavManager.NavigateTo("/");
			}
			else
				message = string.Join(",", result.Errors);
		}

		protected void GoToRegister()
		{
			NavManager.NavigateTo("/Register");
		}
	}
}
