using CSM.Core.Common;
using CSM.Core.Entities;
using MediatR;

namespace CSM.Core.UseCases.Commands.TasksCommands
{

    public class UpdateTaskCommand : IRequest<ResponseResult>
    {
        public Tasks Task { get; set; }
    }

}
