using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.Interfaces
{
    public interface ITaskCommandUseCase
    {
        Task<int> CreateTask(UseCases.Commands.CreateTaskCommand command);
        Task<int> UpdateTask(UseCases.Commands.UpdateTaskCommand command);
        Task<int> UpdateTaskStatus(UseCases.Commands.UpdateTaskStatusCommand command);
        Task<int> SoftDeleteTask(UseCases.Commands.SoftDeleteTaskCommand command);
        Task<int> RestoreTask(UseCases.Commands.RestoreTaskCommand command);
        Task<int> PermanentDeleteTask(UseCases.Commands.PermanentDeleteTaskCommand command);
    }
}
