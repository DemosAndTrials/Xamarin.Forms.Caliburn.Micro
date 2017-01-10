using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Shared.Model;
using Shared.Services;
using Shared.ViewModels;
using Shared.ViewModels.Abstract;

namespace Shell.Services
{
    /// <summary>
    /// Navigation service
    /// </summary>
    public class ApplicationNavigationService : IApplicationNavigationService
    {
        #region Fields and Properties

        private readonly INavigationService _navigation;

        #endregion

        #region MyRegion

        public ApplicationNavigationService(INavigationService navigation)
        {
            _navigation = navigation;
        }

        #endregion

        #region Methods

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

        public void To<T>(object parameter) where T : class, IScreen, IHaveParameter
        {
            _navigation.For<T>()
                .WithParam(v => v.NavigationParameter, parameter)
                .Navigate();
        } 

        #endregion
    }
}
