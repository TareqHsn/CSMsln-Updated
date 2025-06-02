using MediatR;
using CSM.Core.Interfaces;
using CSM.Core.UseCases.Commands.TasksCommands;
using CSM.Core.Interfaces.ITasks;
using CSM.Core.Entities;

namespace CSM.Core.UseCases.Commands.Handlers.TaskHandlers
{
    public class PermanentDeleteTaskHandler : IRequestHandler<PermanentDeleteTaskCommand, int>
    {
        private readonly ITaskCommandRepository _commandRepository;

        public PermanentDeleteTaskHandler(ITaskCommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task<int> Handle(PermanentDeleteTaskCommand request, CancellationToken cancellationToken)
        {
            return await _commandRepository.PermanentDeleteTaskById(request.TaskId);
        }
    }
}