using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro.Xamarin.Forms;
using Shared.Services;

namespace Core.Services
{
    public class ActionSheetDialogService : IDialogService
    {
        private readonly FormsApplication _application;

        public ActionSheetDialogService(FormsApplication application)
        {
            this._application = application;
        }

        public async Task<T> ShowSelectionDialogAsync<T>(string title, string header, IEnumerable<T> options)
        {
            var optionLabels = options.ToDictionary(o => o.ToString(), o => o);

            var selectedLabel = await _application.MainPage.DisplayActionSheet(title, "Cancel", null, optionLabels.Select(o => o.Key).ToArray());

            return optionLabels.ContainsKey(selectedLabel) ? optionLabels[selectedLabel] : default(T);
        }

        public async Task<bool> ShowDialogAsync(string title, string message, string accept, string cancel)
        {
            return await _application.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task ShowDialogAsync(string title, string message, string cancel)
        {
           await _application.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
