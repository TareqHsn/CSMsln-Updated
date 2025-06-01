using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CSM.Core.UseCases.Queries
{
    public class GetTaskCountQuery : IRequest<int>
    {
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
