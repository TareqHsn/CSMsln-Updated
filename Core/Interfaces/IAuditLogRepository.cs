using CSM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<int> LogAuditAsync(AuditLog auditLog);
    }
}
