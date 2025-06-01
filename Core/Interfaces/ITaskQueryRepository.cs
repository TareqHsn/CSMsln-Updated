using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.Interfaces
{
    public interface ITaskQueryRepository
    {
        Task<Entities.Tasks> GetTaskById(int taskId);
        Task<(IEnumerable<Entities.Tasks> Tasks, int TotalCount)> GetTaskList(string sortOrder, string filterStatus, int pageNumber, int pageSize, string userId, bool isDeleted);
        Task<int> GetTaskCount(string userId, bool isDeleted);
    }
}
