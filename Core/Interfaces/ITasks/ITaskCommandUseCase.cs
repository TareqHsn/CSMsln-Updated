using CSM.Core.UseCases.Commands.TaskCommands;
using CSM.Core.UseCases.Commands.TasksCommands;

namespace CSM.Core.Interfaces.ITasks
{
    public interface ITaskCommandUseCase
    {
        Task<int> CreateTask(CreateTaskCommand command);
        Task<int> UpdateTask(UpdateTaskCommand command);
        Task<int> UpdateTaskStatus(UpdateTaskStatusCommand command);
        Task<int> SoftDeleteTask(SoftDeleteTaskCommand command);
        Task<int> RestoreTask(RestoreTaskCommand command);
        Task<int> PermanentDeleteTask(PermanentDeleteTaskCommand command);
    }
}
