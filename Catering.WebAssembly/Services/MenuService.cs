using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using Catering.WebAssembly.Services.Interfaces;
using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;
using CateringApi.Helpers.Configuration;
using Microsoft.Extensions.Options;

namespace Catering.WebAssembly.Services
{
    public class MenuService : IMenuService
	{
		private readonly HttpClient _client;
		private readonly ApiConfig _options;

        public MenuService(HttpClient client, IOptions<ApiConfig> options)
        {
			_client = client;
			_options = options.Value;
        }

        public async Task<MenuDto?> GetForSpecificDay(string token, DateTime? date = null)
		{
			var uriBuilder = new UriBuilder(_options.BaseUrl + _options.MenusEndpoint + "forSpecificDay");
			var query = HttpUtility.ParseQueryString(string.Empty);
			if (date != null)
				query["date"] = date.Value.ToString();
			uriBuilder.Query = query.ToString();

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _client.GetFromJsonAsync<MenuDto> (uriBuilder.Uri);
			
			return response;
		}

		public async Task<MenuDto?> Get(int id, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.GetFromJsonAsync<MenuDto>(_options.MenusEndpoint + id);

			return response;
		}

		public async Task<bool> Create(MenuRequest request, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PostAsJsonAsync(_options.MenusEndpoint, request);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Update(int id, MenuRequest request, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PutAsJsonAsync(_options.MenusEndpoint + id, request);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteItem(int id, int foodItemId, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PutAsJsonAsync(_options.MenusEndpoint + $"{id}/removefooditem", foodItemId);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Delete(int id, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.DeleteAsync(_options.MenusEndpoint + id);

			return response.IsSuccessStatusCode;
		}
	}
}
