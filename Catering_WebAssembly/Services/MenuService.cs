using System.Net.Http.Json;
using CateringApi.DAL.Models;
using System.Text.Json;
using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;

namespace Catering_WebAssembly.Services
{
	public class MenuService : IMenuService
	{
		private readonly HttpClient _client;
		const string _baseUrl = "https://localhost:7079/";
		const string _menusEndpoint = "api/menus/";

        public MenuService(HttpClient client)
        {
			_client = client;
			_client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<MenuDto?> GetForToday()
		{
			var response = await _client.GetFromJsonAsync<MenuDto> (_menusEndpoint + "fortoday");

			return response;
		}

		public async Task<MenuDto?> Create(MenuRequest request)
		{
			var response = await _client.PostAsJsonAsync(_menusEndpoint, request);

			var responseBody = await response.Content.ReadAsStreamAsync();

			var apiResponse = await JsonSerializer.DeserializeAsync<MenuDto>(responseBody, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return apiResponse;
		}
	}
}
