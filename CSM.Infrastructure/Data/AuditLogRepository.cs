using CSM.Core.Entities;
using CSM.Core.Interfaces;

namespace CSM.Infrastructure.Data
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> LogAuditAsync(AuditLog auditLog)
        {
            await _context.AuditLogs.AddAsync(auditLog);
            return await _context.SaveChangesAsync();
        }
    }
}
