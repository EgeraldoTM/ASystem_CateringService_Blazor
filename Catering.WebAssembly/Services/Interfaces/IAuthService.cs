using CateringApi.Helpers.Common.Requests;
using CateringApi.Helpers.Common.Results;

namespace Catering.WebAssembly.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> Login(UserLoginRequest user);
        Task<AuthResult> Register(UserRegistrationRequest user);
    }
}
