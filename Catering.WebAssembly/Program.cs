using Blazored.SessionStorage;
using Catering.WebAssembly;
using Catering.WebAssembly.Authorization;
using Catering.WebAssembly.Services;
using Catering.WebAssembly.Services.Interfaces;
using CateringApi.Helpers.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("ApiConfig"));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiConfig = builder.Configuration.GetSection("ApiConfig").Get<ApiConfig>();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(apiConfig!.BaseUrl) });

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFoodItemService, FoodItemService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddBlazoredSessionStorage();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
