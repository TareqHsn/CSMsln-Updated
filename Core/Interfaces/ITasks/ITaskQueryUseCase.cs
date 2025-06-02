using CSM.Core.Entities;
using CSM.Core.UseCases.Queries.TaskQueries;


namespace CSM.Core.Interfaces.ITasks
{
    public interface ITaskQueryUseCase
    {
        Task<Tasks> GetTaskById(GetTaskByIdQuery query);
        Task<(IEnumerable<Tasks> Tasks, int TotalCount)> GetTaskList(GetTaskListQuery query);
        Task<int> GetTaskCount(GetTaskCountQuery query);
    }
}
