using CSM.Core.Entities;
using MediatR;

namespace CSM.Core.UseCases.Queries
{
    public class GetTaskByIdQuery : IRequest<Entities.Tasks>
    {
        public int TaskId { get; set; }
    }
}
