// Core/UseCases/Commands/Handlers/SoftDeleteTaskHandler.cs
using MediatR;
using CSM.Core.Interfaces;

namespace CSM.Core.UseCases.Commands.Handlers
{
    public class SoftDeleteTaskHandler : IRequestHandler<SoftDeleteTaskCommand, int>
    {
        private readonly ITaskCommandRepository _commandRepository;

        public SoftDeleteTaskHandler(ITaskCommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task<int> Handle(SoftDeleteTaskCommand request, CancellationToken cancellationToken)
        {
            return await _commandRepository.SoftDeleteTaskById(request.TaskId);
        }
    }
}