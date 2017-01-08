using Caliburn.Micro;
using Shared.Model;

namespace Shared.Services
{
    public interface IApplicationNavigationService
    {
        void ToProjects();
        void ToProject(Project project);

        void To<T>() where T : class, IScreen;
    }
}
