using CSM.Core.Interfaces.ITasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskCommandRepository TaskCommandRepository { get; }
        IAuditLogRepository AuditLogRepository { get; }
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
