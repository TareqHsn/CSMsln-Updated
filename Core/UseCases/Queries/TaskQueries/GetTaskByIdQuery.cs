using CSM.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.UseCases.Queries.TaskQueries
{
    public class GetTaskByIdQuery : IRequest<Tasks>
    {
        public int TaskId { get; set; }
    }
}
