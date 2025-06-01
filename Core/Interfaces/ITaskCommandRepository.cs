using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.Interfaces
{
    public interface ITaskCommandRepository
    {
        Task<int> SaveTask(Entities.Tasks task);
        Task<int> UpdateTaskById(Entities.Tasks task);
        Task<int> UpdateTaskStatusById(Entities.Tasks task);
        Task<int> SoftDeleteTaskById(int taskId);
        Task<int> RestoreTaskById(int taskId);
        Task<int> PermanentDeleteTaskById(int taskId);
    }
}
