// Core/UseCases/Commands/PermanentDeleteTaskCommand.cs
using MediatR;

namespace CSM.Core.UseCases.Commands
{
    public class PermanentDeleteTaskCommand : IRequest<int>
    {
        public int TaskId { get; set; }
    }
}