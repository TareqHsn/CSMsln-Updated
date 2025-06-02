using CSM.Core.Entities;

namespace CSM.Core.Interfaces.ITasks
{
    public interface ITaskCommandRepository
    {
        Task<int> SaveTask(Tasks task);
        Task<int> UpdateTaskById(Tasks task);
        Task<int> UpdateTaskStatusById(Tasks task);
        Task<int> SoftDeleteTaskById(int taskId);
        Task<int> RestoreTaskById(int taskId);
        Task<int> PermanentDeleteTaskById(int taskId);
    }
}
