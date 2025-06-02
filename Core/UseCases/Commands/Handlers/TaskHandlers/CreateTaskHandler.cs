using CSM.Core.Entities;
using CSM.Core.Interfaces;
using CSM.Core.UseCases.Commands.TasksCommands;
using FluentValidation;
using MediatR;

namespace CSM.Core.UseCases.Commands.Handlers.TaskHandlers
{

    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;


        public CreateTaskHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
        {
            public CreateTaskCommandValidator()
            {
                RuleFor(x => x.Task)
                    .NotNull()
                    .WithMessage("Task cannot be null.");

                RuleFor(x => x.Task.Title)
                    .NotEmpty()
                    .WithMessage("Task title is required.")
                    .MaximumLength(100)
                    .WithMessage("Task title cannot exceed 100 characters.");

               

                
            }
        }


        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var taskId = await _unitOfWork.TaskCommandRepository.SaveTask(request.Task);
                if (taskId == 0)
                {
                    throw new Exception("Failed to create task.");
                }

                var auditLog = new AuditLog
                {
                    Action = "TaskCreated",
                    UserId = request.Task.UserId,
                    Timestamp = DateTime.UtcNow,
                    EntityName = "Task",
                    EntityId = request.Task.Id
                };
                var auditResult = await _unitOfWork.AuditLogRepository.LogAuditAsync(auditLog);
                if (auditResult == 0)
                {
                    throw new Exception("Failed to log audit entry.");
                }

                await _unitOfWork.CommitTransactionAsync();
                return taskId;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }



}
