using Shared.Model;

namespace Shared.ViewModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel(Project project)
        {
            Project = project;
        }

        public Project Project { get; }
    }
}
