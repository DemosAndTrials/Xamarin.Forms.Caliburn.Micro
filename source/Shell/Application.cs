using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Shared.Services;
using Shared.Services.NoNetwork;
using Shared.ViewModels;
using Shell.Services;
using Shell.Views;
using Xamarin.Forms;
using MainView = Shell.Views.MainView;

namespace Shell
{
    public class Application : FormsApplication
    {
        private readonly SimpleContainer _container;

        public Application(SimpleContainer container)
        {
            _container = container;

            Initialize();

            ViewModelLocator.AddNamespaceMapping("Shell.Views", "Shared.ViewModels");
            ViewLocator.AddNamespaceMapping("Shared.ViewModels", "Shell.Views");

            MessageBinder.SpecialValues.Add("$tappedItem", GetTappedItem);

            container.Instance<FormsApplication>(this);

            container
               .Singleton<ITeamServicesClient, OfflineTeamServicesClient>()
               .Singleton<IAuthenticationService, AuthenticationService>()
               .Singleton<IApplicationNavigationService, ApplicationNavigationService>()
               .Singleton<IDialogService, ApplicationDialogService>();

            container
                .PerRequest<RootViewModel>()
                .PerRequest<LoginViewModel>()
                .PerRequest<LoginPopupViewModel>()
                .PerRequest<MainViewModel>()
                .PerRequest<LoginOldViewModel>()
                .PerRequest<ProjectsViewModel>()
                .PerRequest<BuildsViewModel>();

            DisplayRootView<RootView>();
        }

        private object GetTappedItem(ActionExecutionContext c)
        {
            var listView = (ListView) c.Source;

            var selectedItem = listView.SelectedItem;

            listView.SelectedItem = null;

            return selectedItem;
        }
        
        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            _container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }
    }
}
