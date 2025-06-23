using CSM.Core.Entities;
using MediatR;

namespace CSM.Core.UseCases.Queries.TaskQueries
{
    public class GetTaskByIdQuery : IRequest<Tasks>
    {
        public int TaskId { get; set; }
    }
}
