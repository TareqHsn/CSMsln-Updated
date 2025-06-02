using MediatR;
using CSM.Core.UseCases.Commands.TasksCommands;
using CSM.Core.Interfaces.ITasks;
using CSM.Core.Entities;
using CSM.Core.Interfaces;

namespace CSM.Core.UseCases.Commands.Handlers.TaskHandlers
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var task = request.Task;
                var result = await _unitOfWork.TaskCommandRepository.UpdateTaskById(task);
                if (result == 0)
                {
                    throw new Exception("Failed to update task.");
                }

                var auditLog = new AuditLog
                {
                    Action = "TaskUpdated",
                    UserId = task.UserId,
                    Timestamp = DateTime.UtcNow,
                    EntityName = "Task",
                    EntityId = task.Id
                };
                var auditResult = await _unitOfWork.AuditLogRepository.LogAuditAsync(auditLog);
                if (auditResult == 0)
                {
                    throw new Exception("Failed to log audit entry.");
                }

                await _unitOfWork.CommitTransactionAsync();
                return result;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}