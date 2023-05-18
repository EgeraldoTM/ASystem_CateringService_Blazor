using System.Net.Http.Json;
using System.Text.Json;
using CateringApi.Helpers.Common.Requests;
using CateringApi.Helpers.Common.Results;

namespace Catering_WebAssembly.Services
{
	public class AuthService : IAuthService
	{
		private readonly HttpClient _client;
		const string _baseUrl = "https://localhost:7079/";
		const string _authEndpoint = "api/authmanagement/";

        public AuthService(HttpClient client)
        {
            _client = client;
			_client.BaseAddress = new Uri(_baseUrl);
		}

        public async Task<AuthResult> Login(UserLoginRequest user)
		{
			var response = await _client.PostAsJsonAsync(_authEndpoint + "login", user);

			var responseBody = await response.Content.ReadAsStreamAsync();

			var result = await JsonSerializer.DeserializeAsync<AuthResult>(responseBody, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return result;
		}

		public async Task<AuthResult> Register(UserRegistrationRequest user)
		{
			var response = await _client.PostAsJsonAsync(_authEndpoint + "register", user);

			var responseBody = await response.Content.ReadAsStreamAsync();

			var result = await JsonSerializer.DeserializeAsync<AuthResult>(responseBody, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return result;
		}
	}
}
