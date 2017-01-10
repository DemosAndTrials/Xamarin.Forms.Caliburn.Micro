using System.Threading.Tasks;
using Xamarin.Auth;

namespace Shared.Services.NoNetwork
{
	public class OfflineAuthenticationService : IAuthenticationService
	{
		public Task<bool> AuthenticateCredentialsAsync(Credentials credentials)
		{
			return Task.FromResult(credentials.Account == "caliburn-micro");
		}

	    public Account UserAccount { get; private set; }
	    public void SetAccount(Account account)
	    {
	        UserAccount = account;
	    }
	}
}
