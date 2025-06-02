using CSM.Core.Entities;
using MediatR;

namespace CSM.Core.UseCases.Commands.TasksCommands
{

    public class UpdateTaskCommand : IRequest<int>
    {
        public Tasks Task { get; set; }
    }

}
