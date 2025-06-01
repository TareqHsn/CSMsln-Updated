using MediatR;
using CSM.Core.Interfaces;

namespace CSM.Core.UseCases.Commands.Handlers
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, int>
    {
        private readonly ITaskCommandRepository _commandRepository;

        public UpdateTaskHandler(ITaskCommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task<int> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            return await _commandRepository.UpdateTaskById(request.Task);
        }
    }
}