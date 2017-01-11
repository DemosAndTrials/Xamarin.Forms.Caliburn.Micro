using System.Diagnostics;
using Caliburn.Micro;
using Shared.Services;
using Xamarin.Forms;

namespace Shared.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>
    {
        private readonly IApplicationNavigationService _navigation;

        public ShellViewModel(IApplicationNavigationService navigation)
        {
            _navigation = navigation;
        }

        protected override void OnInitialize()
        {
           
            base.OnInitialize();
            //var vm = IoC.Get<MainViewModel>();
            ////DeactivateItem(ActiveItem, true);
            //ActivateItem(vm);
            var t = GetView();
        
        }

        public ContentPage DetailsView { get; set; }

        public void ShowDetails(string details)
        {
            Debug.WriteLine(details);

            if ("home".Equals(details))
            {
                var vm = IoC.Get<MainViewModel>();
                
                DeactivateItem(ActiveItem, true);
                ActivateItem(vm);
            }
            else if ("agenda".Equals(details))
            {

            }
            else if ("accounts".Equals(details))
            {

            }
            else if ("tasks".Equals(details))
            {

            }
            else if ("Product".Equals(details))
            {

            }
            else if ("Promotion".Equals(details))
            {

            }
            else if ("Settings".Equals(details))
            {

            }
        }
    }
}
