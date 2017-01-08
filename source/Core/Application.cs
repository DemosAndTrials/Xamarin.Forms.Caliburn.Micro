using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Core.Services;
using Core.Views;
using Shared.Services;
using Shared.Services.NoNetwork;
using Shared.ViewModels;
using Xamarin.Forms;
using LoginView = Core.Views.LoginView;

namespace Core
{
    public class Application : FormsApplication
    {
        private readonly SimpleContainer _container;

        public Application(SimpleContainer container)
        {
            _container = container;

            Initialize();

            ViewModelLocator.AddNamespaceMapping("Core.Views", "Shared.ViewModels");
            ViewLocator.AddNamespaceMapping("Shared.ViewModels", "Core.Views");

            MessageBinder.SpecialValues.Add("$tappedItem", GetTappedItem);

            container.Instance<FormsApplication>(this);

            container
               .Singleton<ITeamServicesClient, OfflineTeamServicesClient>()
               .Singleton<IAuthenticationService, OfflineAuthenticationService>()
               .Singleton<IApplicationNavigationService, ApplicationNavigationService>()
               .Singleton<IDialogService, ActionSheetDialogService>();

            container
                .PerRequest<MainViewModel>()
                .PerRequest<LoginViewModel>()
                .PerRequest<ProjectsViewModel>()
                .PerRequest<BuildsViewModel>();

            DisplayRootView<MainView>();
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
