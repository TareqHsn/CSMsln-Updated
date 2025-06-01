// Core/UseCases/Commands/Handlers/RestoreTaskHandler.cs
using MediatR;
using CSM.Core.Interfaces;

namespace CSM.Core.UseCases.Commands.Handlers
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