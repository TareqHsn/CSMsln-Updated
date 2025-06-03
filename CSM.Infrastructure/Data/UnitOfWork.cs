using CSM.Core.Interfaces.ITasks;
using CSM.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CSM.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        public ITaskCommandRepository TaskCommandRepository { get; }
        public IAuditLogRepository AuditLogRepository { get; }

        public UnitOfWork(ApplicationDbContext context, ITaskCommandRepository taskCommandRepository, IAuditLogRepository auditLogRepository)
        {
            _context = context;
            TaskCommandRepository = taskCommandRepository;
            AuditLogRepository = auditLogRepository;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
