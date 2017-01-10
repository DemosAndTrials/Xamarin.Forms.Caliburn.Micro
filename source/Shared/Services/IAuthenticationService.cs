using System.Threading.Tasks;
using Xamarin.Auth;

namespace Shared.Services
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateCredentialsAsync(Credentials credentials);

        Account UserAccount { get; }
        void SetAccount(Account account);
    }
}