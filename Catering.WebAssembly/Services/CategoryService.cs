using System.Net.Http.Json;
using Catering.WebAssembly.Services.Interfaces;
using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Configuration;
using Microsoft.Extensions.Options;

namespace Catering.WebAssembly.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly HttpClient _client;
		private readonly ApiConfig _options;

		public CategoryService(HttpClient client, IOptions<ApiConfig> options)
        {
			_client = client;
			_options = options.Value;
		}
        public async Task<IEnumerable<CategoryDto>> GetAll()
		{
			var response = await _client.GetFromJsonAsync<IEnumerable<CategoryDto>>(_options.CategoriesEndpoint);

			return response ?? Enumerable.Empty<CategoryDto>();
		}
	}
}
