using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CSM.Core.UseCases.Commands
{
    public class UpdateTaskStatusCommand : IRequest<int>
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
    }
}
