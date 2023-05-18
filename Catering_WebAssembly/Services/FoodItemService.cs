using System.Net.Http.Json;
using System.Text.Json;
using CateringApi.Helpers.Common.DTOs;

namespace Catering_WebAssembly.Services
{
	public class FoodItemService : IFoodItemService
	{
		private readonly HttpClient _client;
		const string _baseUrl = "https://localhost:7079/";
		const string _foodItemsEndpoint = "api/fooditems/";
		//const string _host = "localhost:7079";

		public FoodItemService(HttpClient httpClient)
		{
			_client = httpClient;
			_client.BaseAddress = new Uri(_baseUrl);
		}

		public async Task<IEnumerable<FoodItemDto>> GetAll()
		{

			var response = await _client.GetFromJsonAsync<IEnumerable<FoodItemDto>>(_foodItemsEndpoint);

			return response;
		}

		public async Task<FoodItemDto> Get(int id)
		{
			var response = await _client.GetFromJsonAsync<FoodItemDto>(_foodItemsEndpoint + id);

			return response;
		}

		public async Task<FoodItemDto> Create(FoodItemDto foodItem)
		{
			var response = await _client.PostAsJsonAsync(_foodItemsEndpoint, foodItem);

			var responseBody = await response.Content.ReadAsStreamAsync();

			var apiResponse = await JsonSerializer.DeserializeAsync<FoodItemDto>(responseBody, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return apiResponse;
		}

		public async Task<bool> Update(FoodItemDto foodItem)
		{
			var response = await _client.PutAsJsonAsync($"{_foodItemsEndpoint}{foodItem.Id}", foodItem);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Delete(int id)
		{
			var response = await _client.DeleteAsync(_foodItemsEndpoint + id);

			return response.IsSuccessStatusCode;
		}
	}
}
