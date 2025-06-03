using MediatR;
using CSM.Core.Interfaces.ITasks;
using CSM.Core.Entities;
using CSM.Core.UseCases.Commands.TasksCommands;
using CSM.Core.Interfaces;

namespace CSM.Core.UseCases.Commands.Handlers
{
    public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var task = new Tasks { Id = request.TaskId, IsCompleted = request.IsCompleted };
                var result = await _unitOfWork.TaskCommandRepository.UpdateTaskStatusById(task);
                if (result == 0)
                {
                    throw new Exception("Failed to update task status.");
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