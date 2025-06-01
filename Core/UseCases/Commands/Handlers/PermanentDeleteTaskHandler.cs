// Core/UseCases/Commands/Handlers/PermanentDeleteTaskHandler.cs
using MediatR;
using CSM.Core.Interfaces;

namespace CSM.Core.UseCases.Commands.Handlers
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