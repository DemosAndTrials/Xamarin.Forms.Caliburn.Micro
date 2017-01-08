using System;
using System.Collections.Generic;
using System.Reflection;
using Android.App;
using Android.Runtime;
using App.Droid.Services;
using Caliburn.Micro;
using Shared.Services;
using Shared.ViewModels;
using LoginView = Core.Views.LoginView;

namespace App.Droid
{
    [Application]
    public class Application : CaliburnApplication
    {
        private SimpleContainer _container;

        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Initialize();
        }

        protected override void Configure()
        {
            LogManager.GetLog = t => new DebugLog(t);

            _container = new SimpleContainer();
            _container.Instance(_container);

            _container
               .Singleton<ICredentialsService, PreferencesCredentialsService>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                typeof (LoginView).Assembly,
                typeof (LoginViewModel).Assembly
            };
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }
    }
}