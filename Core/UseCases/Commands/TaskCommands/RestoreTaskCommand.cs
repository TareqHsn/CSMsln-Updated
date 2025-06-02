using MediatR;

namespace CSM.Core.UseCases.Commands.TasksCommands
{
    public class RestoreTaskCommand : IRequest<int>
    {
        public int TaskId { get; set; }
       
    }
}
