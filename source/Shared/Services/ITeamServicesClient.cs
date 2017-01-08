using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Model;

namespace Shared.Services
{
    public interface ITeamServicesClient
    {
        Task<Project> GetProjectAsync(string id);
        Task<IReadOnlyCollection<Project>> GetProjectsAsync();
        Task<IReadOnlyCollection<BuildDetail>> GetBuildsAsync(Project project);
        Task<IReadOnlyCollection<Definition>> GetDefinitionsAsync(Project project);
        Task<BuildDetail> QueueBuildAsync(Project project, BuildRequest buildRequest);
    }
}