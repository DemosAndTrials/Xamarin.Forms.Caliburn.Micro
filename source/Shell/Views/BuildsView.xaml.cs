using System;
using Shared.ViewModels;
using Xamarin.Forms;

namespace Core.Views
{
    public partial class BuildsView
    {
        public BuildsView()
        {
            InitializeComponent();
        }

        private void OnQueueBuild(object sender, EventArgs e)
        {
            ((BuildsViewModel)BindingContext).QueueBuild();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }
    }
}
