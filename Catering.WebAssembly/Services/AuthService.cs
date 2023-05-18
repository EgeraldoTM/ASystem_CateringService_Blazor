using System.Net.Http.Json;
using System.Text.Json;
using Catering.WebAssembly.Services.Interfaces;
using CateringApi.Helpers.Common.Requests;
using CateringApi.Helpers.Common.Results;
using CateringApi.Helpers.Configuration;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;

namespace Catering.WebAssembly.Services
{
    public class AuthService : IAuthService
	{
		private readonly HttpClient _client;
		private readonly ApiConfig _options;

        public AuthService(HttpClient client, IOptions<ApiConfig> options)
        {
            _client = client;
			_options = options.Value;
		}

        public async Task<AuthResult> Login(UserLoginRequest user)
		{
			var response = await _client.PostAsJsonAsync(_options.AuthEndpoint + "login", user);

			var responseBody = await response.Content.ReadAsStreamAsync();

			var result = await JsonSerializer.DeserializeAsync<AuthResult>(responseBody, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return result;
		}

		public async Task<AuthResult> Register(UserRegistrationRequest user)
		{
			var response = await _client.PostAsJsonAsync(_options.AuthEndpoint + "register", user);

			var responseBody = await response.Content.ReadAsStreamAsync();

			var result = await JsonSerializer.DeserializeAsync<AuthResult>(responseBody, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return result!;
		}
	}
}
