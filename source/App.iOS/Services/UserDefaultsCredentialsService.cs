using System.Threading.Tasks;
using Foundation;
using Shared.Services;

namespace App.iOS.Services
{
    public class UserDefaultsCredentialsService : ICredentialsService
    {
        public Task<Credentials> GetCredentialsAsync()
        {
            var account = NSUserDefaults.StandardUserDefaults.StringForKey("account");
            var token = NSUserDefaults.StandardUserDefaults.StringForKey("token");

            if (account == null || token == null)
                return Task.FromResult(Credentials.None);

            return Task.FromResult(new Credentials(account, token));
        }

        public Task StoreAsync(Credentials credentials)
        {
            NSUserDefaults.StandardUserDefaults.SetString(credentials.Account, "account");
            NSUserDefaults.StandardUserDefaults.SetString(credentials.Token, "token");

            return Task.FromResult(true);
        }
    }

}