using System.Threading.Tasks;
using Caliburn.Micro;
using Shared.Services;
using Xamarin.Forms;

namespace Shared.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly IApplicationNavigationService _navigation;
        private readonly IDialogService _dialogs;

        public string DialogMessage { get; private set; }

        public MainViewModel(IApplicationNavigationService navigation, IDialogService dialogs)
        {
            _navigation = navigation;
            _dialogs = dialogs;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        public override string DisplayName
        {
            get { return "MainView"; }
            set { }
        }

        public void Login()
        {
            _navigation.To<LoginOldViewModel>();
        }

        public async Task Popup()
        {
            var answer = await _dialogs.ShowDialogAsync("Question?", "Would you like to play a game", "Yes", "No");
            var msg = "You clicked ";
            msg += answer ? "Yes" : "No";
            DialogMessage = msg;
        }
    }
}
