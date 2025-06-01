using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CSM.Core.UseCases.Commands.Handlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly Interfaces.ITaskCommandRepository _commandRepository;

        public CreateTaskHandler(Interfaces.ITaskCommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            return await _commandRepository.SaveTask(request.Task);
        }
    }
}
