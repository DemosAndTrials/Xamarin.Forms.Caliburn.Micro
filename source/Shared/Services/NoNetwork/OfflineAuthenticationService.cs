using System.Threading.Tasks;

namespace Shared.Services.NoNetwork
{
	public class OfflineAuthenticationService : IAuthenticationService
	{
		public Task<bool> AuthenticateCredentialsAsync(Credentials credentials)
		{
			return Task.FromResult(credentials.Account == "caliburn-micro");
		}
	}
}
