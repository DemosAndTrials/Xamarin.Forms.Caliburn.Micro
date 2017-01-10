using Caliburn.Micro;
using Shared.Model;
using Shared.ViewModels.Abstract;

namespace Shared.Services
{
    public interface IApplicationNavigationService
    {
        void ToProjects();
        void ToProject(Project project);

        void To<T>() where T : class, IScreen;
        void To<T>(object parameter) where T : class, IScreen, IHaveParameter;
    }
}
