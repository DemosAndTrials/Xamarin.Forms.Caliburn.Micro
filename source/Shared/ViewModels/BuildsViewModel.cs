using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Shared.Model;
using Shared.Services;

namespace Shared.ViewModels
{
    public class BuildsViewModel : Screen
    {
        private readonly ITeamServicesClient _teamServices;
        private readonly IDialogService _dialogs;
        private IReadOnlyCollection<Definition> _definitions;

        public BuildsViewModel(ITeamServicesClient teamServices, IDialogService dialogs)
        {
            this._teamServices = teamServices;
            this._dialogs = dialogs;

            Builds = new BindableCollection<BuildViewModel>();
        }

        public string ProjectId { get; set; }

        protected override async void OnInitialize()
        {
            Project = await _teamServices.GetProjectAsync(ProjectId);

            _definitions = await _teamServices.GetDefinitionsAsync(Project);

            var builds = await _teamServices.GetBuildsAsync(Project);

            Builds.AddRange(builds.OrderByDescending(b => b.QueueTime).Select(b => new BuildViewModel(b)));
        }
        
        public async void QueueBuild()
        {
            var selectedDefinition = await _dialogs.ShowSelectionDialogAsync("Queue a new build", "Definitions", _definitions);

            if (selectedDefinition == null)
                return;

            var queuedBuild = await _teamServices.QueueBuildAsync(Project, new BuildRequest
            {
                Definition = selectedDefinition,
                SourceBranch = "master"
            });

            Builds.Insert(0, new BuildViewModel(queuedBuild));
        }

        public BindableCollection<BuildViewModel> Builds { get; }

        public Project Project { get; set; }
    }
}
