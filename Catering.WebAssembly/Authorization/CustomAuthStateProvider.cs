using System.Security.Claims;
using System.Text.Json;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Catering.WebAssembly.Authorization
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;

        public CustomAuthStateProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;
            var token = await _sessionStorage.GetItemAsStringAsync("token");

            if (token is null)
                identity = new ClaimsIdentity(); //Anonymous
            else
            {

                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        public async Task UpdateAuthenticationState(string? token)
        {
            ClaimsPrincipal claimsPrincipal;

            if (token != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
                await _sessionStorage.SetItemAsStringAsync("token", token);
            }
            else
            {
                claimsPrincipal = new(new ClaimsIdentity());
                await _sessionStorage.RemoveItemAsync("token");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task<string> GetUserId()
        {
            var token = await _sessionStorage.GetItemAsStringAsync("token");
            var claims = ParseClaimsFromJwt(token);
            var userIdClaim = claims.FirstOrDefault(c => c.Type == "Id");

            return userIdClaim != null ? userIdClaim.Value : string.Empty;
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            var defaultClaims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())).ToList();
            var nameClaim = defaultClaims.FirstOrDefault(c => c.Type == "name");
            var roleClaim = defaultClaims.FirstOrDefault(c => c.Type == "role");
            defaultClaims.Add(new Claim(ClaimTypes.Name, nameClaim!.Value));
            defaultClaims.Add(new Claim(ClaimTypes.Role, roleClaim!.Value));
            return defaultClaims;
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
