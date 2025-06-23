using CSM.Core.Common;
using CSM.Core.Entities;
using CSM.Core.Interfaces;
using CSM.Core.UseCases.Commands.TaskCommands;
using FluentValidation;
using MediatR;

namespace CSM.Core.UseCases.Commands.Handlers.TaskHandlers
{
   
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

    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, ResponseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResult> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            ResponseResult rr = new ResponseResult();

            try
            {
                var validator = new CreateTaskCommandValidator();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    rr.Message = "Error: " + string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                    rr.StatusCode = 1000;
                    rr.IsSuccessStatus = false;
                    return rr;
                }

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

                rr.Message = "Task created successfully.";
                rr.StatusCode = 2000;
                rr.IsSuccessStatus = true;
                rr.Data = taskId;
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
