
using MediatR;

namespace CSM.Core.UseCases.Commands.TasksCommands
{
    public class PermanentDeleteTaskCommand : IRequest<int>
    {
        public int TaskId { get; set; }
       
    }
}