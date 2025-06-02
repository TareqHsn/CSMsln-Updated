using MediatR;
using CSM.Core.Interfaces;
using CSM.Core.UseCases.Commands.TasksCommands;
using CSM.Core.Interfaces.ITasks;
using CSM.Core.Entities;

namespace CSM.Core.UseCases.Commands.Handlers.TaskHandlers
{
    public class RestoreTaskHandler : IRequestHandler<RestoreTaskCommand, int>
    {
        private readonly ITaskCommandRepository _commandRepository;

        public RestoreTaskHandler(ITaskCommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task<int> Handle(RestoreTaskCommand request, CancellationToken cancellationToken)
        {
            return await _commandRepository.RestoreTaskById(request.TaskId);
        }
    }
}