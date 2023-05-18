using Catering.WebAssembly.Authorization;
using CateringApi.Helpers.Common.Requests;

namespace Catering.WebAssembly.Pages.Account
{
    public partial class Register
	{
		private string message = string.Empty;
		private UserRegistrationRequest newUser = new();
		private string maxDate = DateTime.Now.Date.AddYears(-18).ToString("yyyy-MM-dd");
		private string minDate = DateTime.Now.Date.AddYears(-100).ToString("yyyy-MM-dd");

		protected void GoToLogin()
		{
			NavManager.NavigateTo("/Login");
		}

		protected async override Task OnInitializedAsync()
		{
			await Task.FromResult(newUser.Birthday = DateTime.Now.Date.AddYears(-18));
		}

		protected async Task HandleValidRequest()
		{
			var result = await AuthService.Register(newUser);

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
	}
}
