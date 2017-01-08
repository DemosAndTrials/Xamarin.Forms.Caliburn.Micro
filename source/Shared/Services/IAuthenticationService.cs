using System.Threading.Tasks;

namespace Shared.Services
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateCredentialsAsync(Credentials credentials);
    }
}