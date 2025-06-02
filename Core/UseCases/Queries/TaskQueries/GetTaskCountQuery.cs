using MediatR;

namespace CSM.Core.UseCases.Queries.TaskQueries
{
    public class GetTaskCountQuery : IRequest<int>
    {
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
