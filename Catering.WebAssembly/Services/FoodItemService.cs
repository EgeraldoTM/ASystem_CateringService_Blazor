using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Catering.WebAssembly.Services.Interfaces;
using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Configuration;
using Microsoft.Extensions.Options;

namespace Catering.WebAssembly.Services
{
    public class FoodItemService : IFoodItemService
	{
		private readonly HttpClient _client;
		private readonly ApiConfig _options;

		public FoodItemService(HttpClient httpClient, IOptions<ApiConfig> options)
		{
			_client = httpClient;
			_options = options.Value;
		}

		public async Task<IEnumerable<FoodItemDto>> GetAll(string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.GetFromJsonAsync<IEnumerable<FoodItemDto>>(_options.FoodItemsEndpoint) ?? Enumerable.Empty<FoodItemDto>();

			return response;
		}

		public async Task<FoodItemDto?> Get(int id, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.GetFromJsonAsync<FoodItemDto>(_options.FoodItemsEndpoint + id);

			return response;
		}

		public async Task<bool> Create(FoodItemDto foodItem, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PostAsJsonAsync(_options.FoodItemsEndpoint, foodItem);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Update(FoodItemDto foodItem, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PutAsJsonAsync(_options.FoodItemsEndpoint + foodItem.Id, foodItem);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Delete(int id, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.DeleteAsync(_options.FoodItemsEndpoint + id);

			return response.IsSuccessStatusCode;
		}
	}
}
