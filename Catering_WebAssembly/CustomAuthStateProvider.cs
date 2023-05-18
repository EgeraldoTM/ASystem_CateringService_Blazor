using System.Security.Claims;

namespace Catering_WebAssembly
{
	public class CustomAuthStateProvider : AuthenticationStateProvider
	{
		public override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			string token = string.Empty;

			var identity = new ClaimsIdentity();
			var user = new ClaimsPrincipal(identity);
			var state = new AuthenticationState(user);

			//NotifyAuthenticationStateChanged(Task.FromResult(state));

			return Task.FromResult(state);
		}
	}
}
