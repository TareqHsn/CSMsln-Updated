using MediatR;
using CSM.Core.UseCases.Commands.TasksCommands;
using CSM.Core.Entities;
using CSM.Core.Interfaces;
using FluentValidation;
using CSM.Core.Common;

namespace CSM.Core.UseCases.Commands.Handlers.TaskHandlers
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
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
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, ResponseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResult> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            ResponseResult rr = new ResponseResult();

            try
            {
                var validator = new UpdateTaskCommandValidator();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    rr.Message = "Validation Error: " + string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                    rr.StatusCode = 1000;
                    rr.IsSuccessStatus = false;
                    return rr;
                }

                //Start Transaction
                await _unitOfWork.BeginTransactionAsync();

                var task = request.Task;

                var result = await _unitOfWork.TaskCommandRepository.UpdateTaskById(task);
                if (result == 0)
                {
                    throw new Exception("Failed to update task.");
                }

                // Log
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
                //comit transaction
                await _unitOfWork.CommitTransactionAsync();

                rr.Message = "Task updated successfully.";
                rr.StatusCode = 2000;
                rr.IsSuccessStatus = true;
                rr.Data = task.Id;
                return rr;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();

                rr.Message = "Failed! " + ex.Message;
                rr.StatusCode = 4000;
                rr.IsSuccessStatus = false;
                return rr;
            }
        }
    }
}