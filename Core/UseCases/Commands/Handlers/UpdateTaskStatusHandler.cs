using MediatR;
using CSM.Core.Interfaces;
using CSM.Core.Entities;

namespace CSM.Core.UseCases.Commands.Handlers
{
    public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusCommand, int>
    {
        private readonly ITaskCommandRepository _commandRepository;

        public UpdateTaskStatusHandler(ITaskCommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task<int> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = new Entities.Tasks { Id = request.Id, IsCompleted = request.IsCompleted };
            return await _commandRepository.UpdateTaskStatusById(task);
        }
    }
}