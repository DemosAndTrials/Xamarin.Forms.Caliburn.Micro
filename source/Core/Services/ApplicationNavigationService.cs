using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Shared.Model;
using Shared.Services;
using Shared.ViewModels;

namespace Core.Services
{
    public class ApplicationNavigationService : IApplicationNavigationService
    {
        private readonly INavigationService _navigation;

        public ApplicationNavigationService(INavigationService navigation)
        {
            _navigation = navigation;
        }

        public void ToProjects()
        {
            _navigation.For<ProjectsViewModel>()
                .Navigate();
        }

        public void ToProject(Project project)
        {
            _navigation.For<BuildsViewModel>()
                .WithParam(v => v.ProjectId, project.Id)
                .Navigate();
        }

        public void To<T>() where T : class, IScreen
        {
            _navigation.For<T>()
               .Navigate();
        }
    }
}
