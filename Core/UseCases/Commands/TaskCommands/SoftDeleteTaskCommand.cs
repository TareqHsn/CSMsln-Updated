using MediatR;

namespace CSM.Core.UseCases.Commands.TasksCommands
{
    public class SoftDeleteTaskCommand : IRequest<int>
    {
        public int TaskId { get; set; }
        
    }
}
