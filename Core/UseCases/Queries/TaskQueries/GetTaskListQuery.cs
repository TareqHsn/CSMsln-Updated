using CSM.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.UseCases.Queries.TaskQueries
{
    public class GetTaskListQuery : IRequest<(IEnumerable<Tasks> Tasks, int TotalCount)>
    {
        public string SortOrder { get; set; }
        public string FilterStatus { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
