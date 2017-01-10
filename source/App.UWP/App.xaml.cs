using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using App.UWP.CustomViews;
using App.UWP.Services;
using App.UWP.Views;
using Caliburn.Micro;
using Shared.Services;
using Shared.ViewModels;
using LoginView = Shell.Views.LoginOldView;

namespace App.UWP
{
    public sealed partial class App
    {
        private WinRTContainer _container;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            _container = new WinRTContainer();
            _container.RegisterWinRTServices();

            _container
               .Singleton<ICredentialsService, SettingsCredentialsService>();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Initialize();

            // Skip the whole DisplayRoot etc, it's not needed in XF

            Xamarin.Forms.Forms.Init(e);

            Window.Current.Content = new HostView();
            Window.Current.Activate();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            yield return typeof(LoginView).GetTypeInfo().Assembly;
            yield return typeof(LoginOldViewModel).GetTypeInfo().Assembly;
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
