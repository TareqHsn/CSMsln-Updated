using CSM.Core.Entities;

namespace CSM.Core.Interfaces.ITasks
{
    public interface ITaskQueryRepository
    {
        Task<Tasks> GetTaskById(int taskId);
        Task<(IEnumerable<Tasks> Tasks, int TotalCount)> GetTaskList(string sortOrder, string filterStatus, int pageNumber, int pageSize, string userId, bool isDeleted);
        Task<int> GetTaskCount(string userId, bool isDeleted);
    }
}
