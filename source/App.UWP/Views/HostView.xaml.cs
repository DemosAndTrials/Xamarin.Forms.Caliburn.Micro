using Caliburn.Micro;
using Core;
using Shell;

namespace App.UWP.Views
{
    public sealed partial class HostView
    {
        public HostView()
        {
            InitializeComponent();

            LoadApplication(new Application(IoC.Get<SimpleContainer>()));
        }
    }
}
