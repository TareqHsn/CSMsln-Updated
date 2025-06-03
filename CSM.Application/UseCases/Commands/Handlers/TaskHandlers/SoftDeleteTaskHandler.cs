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
