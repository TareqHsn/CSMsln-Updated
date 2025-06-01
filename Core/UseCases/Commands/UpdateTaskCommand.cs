using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CSM.Core.UseCases.Commands
{

    public class UpdateTaskCommand : IRequest<int>
    {
        public Entities.Tasks Task { get; set; }
    }

}
