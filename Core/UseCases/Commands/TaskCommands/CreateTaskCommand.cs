using CSM.Core.Common;
using CSM.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.UseCases.Commands.TaskCommands
{
    public class CreateTaskCommand : IRequest<ResponseResult>
    {
        public Tasks Task { get; set; }
    }
}
