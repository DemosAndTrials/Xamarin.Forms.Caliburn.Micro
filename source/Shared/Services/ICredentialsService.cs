using System.Threading.Tasks;

namespace Shared.Services
{
    public interface ICredentialsService
    {
        Task<Credentials> GetCredentialsAsync();
        Task StoreAsync(Credentials credentials);
    }
}
