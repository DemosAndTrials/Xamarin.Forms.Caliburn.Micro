using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Services
{
    public interface IDialogService
    {
        Task ShowDialogAsync(string title, string message, string cancel);
        Task<bool> ShowDialogAsync(string title, string message, string accept, string cancel);
        Task<string> ShowActionSheetAsync(string title, string cancel, string destruction, params string[] buttons);
    }
}
