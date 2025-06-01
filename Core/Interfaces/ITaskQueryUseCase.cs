using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.Interfaces
{
    public interface ITaskQueryUseCase
    {
        Task<Entities.Tasks> GetTaskById(UseCases.Queries.GetTaskByIdQuery query);
        Task<(IEnumerable<Entities.Tasks> Tasks, int TotalCount)> GetTaskList(UseCases.Queries.GetTaskListQuery query);
        Task<int> GetTaskCount(UseCases.Queries.GetTaskCountQuery query);
    }
}
