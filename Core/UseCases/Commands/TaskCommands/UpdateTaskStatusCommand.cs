using MediatR;

namespace CSM.Core.UseCases.Commands.TasksCommands
{
    public class UpdateTaskStatusCommand : IRequest<int>
    {
        public int TaskId { get; set; }
        public bool IsCompleted { get; set; }
       
    }
}
