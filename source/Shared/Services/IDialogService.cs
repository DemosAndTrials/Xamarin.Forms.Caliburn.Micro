using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Services
{
    public interface IDialogService
    {
        Task<T> ShowSelectionDialogAsync<T>(string title, string header, IEnumerable<T> options);
        Task<bool> ShowDialogAsync(string title, string message, string accept, string cancel);
        Task ShowDialogAsync(string title, string message, string cancel);
    }
}
