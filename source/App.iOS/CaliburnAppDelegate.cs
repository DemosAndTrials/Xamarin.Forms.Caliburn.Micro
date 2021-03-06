﻿using System;
using System.Collections.Generic;
using System.Reflection;
using App.iOS.Services;
using Caliburn.Micro;
using Shared.Services;
using Shared.ViewModels;
using LoginView = Shell.Views.LoginOldView;

namespace App.iOS
{
    public class CaliburnAppDelegate : CaliburnApplicationDelegate
    {
        private SimpleContainer _container;

        public CaliburnAppDelegate()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();
            _container.Instance(_container);

            _container
              .Singleton<ICredentialsService, UserDefaultsCredentialsService>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                typeof (LoginView).Assembly,
                typeof (LoginOldViewModel).Assembly
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
