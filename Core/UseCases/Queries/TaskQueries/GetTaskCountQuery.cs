using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.UseCases.Queries.TaskQueries
{
    public class GetTaskCountQuery : IRequest<int>
    {
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
