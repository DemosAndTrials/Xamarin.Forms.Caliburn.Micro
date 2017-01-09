using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro.Xamarin.Forms;
using Shared.Services;

namespace Shell.Services
{
    /// <summary>
    /// Dialog service
    /// </summary>
    public class ApplicationDialogService : IDialogService
    {
        private readonly FormsApplication _application;

        public ApplicationDialogService(FormsApplication application)
        {
            _application = application;
        }

        /// <summary>
        /// Presents an alert dialog to the application user with a single cancel button.
        /// </summary>
        /// <param name="title">The title of the alert dialog</param>
        /// <param name="message">The body text of the alert dialog.</param>
        /// <param name="cancel">Text to be displayed on the 'Cancel' button</param>
        /// <returns></returns>
        public async Task ShowDialogAsync(string title, string message, string cancel)
        {
            await _application.MainPage.DisplayAlert(title, message, cancel);
        }

        /// <summary>
        /// Presents an alert dialog to the application user with an accept and a cancel button
        /// </summary>
        /// <param name="title">The title of the alert dialog</param>
        /// <param name="message">The body text of the alert dialog.</param>
        /// <param name="accept">Text to be displayed on the 'Accept' button.</param>
        /// <param name="cancel">Text to be displayed on the 'Cancel' button.</param>
        /// <returns></returns>
        public async Task<bool> ShowDialogAsync(string title, string message, string accept, string cancel)
        {
            return await _application.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        /// <summary>
        /// Displays a native platform action sheet, allowing the application user to choose from several buttons
        /// Ex: "ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook"
        /// </summary>
        /// <param name="title">Title of the displayed action sheet. Must not be null.</param>
        /// <param name="cancel">Text to be displayed in the 'Cancel' button. Can be null to hide the cancel action.</param>
        /// <param name="destruction">Text to be displayed in the 'Destruct' button. Can be null to hide the destructive option.</param>
        /// <param name="buttons">Text labels for additional buttons. Must not be null.</param>
        public async Task<string> ShowActionSheetAsync(string title, string cancel, string destruction, params string[] buttons)
        {
            return await _application.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }

        public async Task<T> ShowSelectionDialogAsync<T>(string title, string header, IEnumerable<T> options)
        {
            var optionLabels = options.ToDictionary(o => o.ToString(), o => o);

            var selectedLabel = await _application.MainPage.DisplayActionSheet(title, "Cancel", null, optionLabels.Select(o => o.Key).ToArray());

            return optionLabels.ContainsKey(selectedLabel) ? optionLabels[selectedLabel] : default(T);
        }
    }
}
