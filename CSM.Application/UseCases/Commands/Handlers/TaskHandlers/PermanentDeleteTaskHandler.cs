using CSM.Core.Interfaces.ITasks;
using CSM.Core.UseCases.Commands.TasksCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Application.UseCases.Commands.Handlers.TaskHandlers
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
