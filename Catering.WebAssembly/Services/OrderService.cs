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
	public class OrderService : IOrderService
	{
		private readonly HttpClient _client;
		private readonly ApiConfig _options;

		public OrderService(HttpClient client, IOptions<ApiConfig> options)
		{
			_client = client;
			_options = options.Value;
		}
		public async Task<OrderDto?> Get(string employeeId, string token, DateTime? date = null)
		{
			var uriBuilder = new UriBuilder(_options.BaseUrl + _options.OrdersEndpoint);

			var query = HttpUtility.ParseQueryString(string.Empty);
			query["employeeId"] = employeeId;
			if (date != null)
				query["date"] = date.Value.ToString();
			uriBuilder.Query = query.ToString();

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _client.GetAsync(uriBuilder.Uri);

			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStreamAsync();
				var apiResponse = await JsonSerializer.DeserializeAsync<OrderDto?>(responseBody, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				return apiResponse;
			}
			return null;
		}

		public async Task<bool> Create(CreateOrderRequest request, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PostAsJsonAsync(_options.OrdersEndpoint, request);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Update(int id, UpdateOrderRequest request, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PutAsJsonAsync(_options.OrdersEndpoint + id, request);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> AddQuantity(int id, int detailId, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PutAsJsonAsync(_options.OrdersEndpoint + id + "/AddQuantity", detailId);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> SubtractQuantity(int id, int detailId, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PutAsJsonAsync(_options.OrdersEndpoint + id + "/SubtractQuantity", detailId);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> RemoveDetail(int id, int detailId, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PutAsJsonAsync(_options.OrdersEndpoint + id + "/RemoveDetail", detailId);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Delete(int id, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.DeleteAsync(_options.OrdersEndpoint + id);

			return response.IsSuccessStatusCode;
		}
	}
}
