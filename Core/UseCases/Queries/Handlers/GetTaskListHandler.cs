// Core/UseCases/Queries/Handlers/GetTaskListHandler.cs
using MediatR;
using CSM.Core.Interfaces;
using CSM.Core.Entities;

namespace CSM.Core.UseCases.Queries.Handlers
{
    public class GetTaskListHandler : IRequestHandler<GetTaskListQuery, (IEnumerable<Entities.Tasks> Tasks, int TotalCount)>
    {
        private readonly ITaskQueryRepository _queryRepository;

        public GetTaskListHandler(ITaskQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<(IEnumerable<Entities.Tasks> Tasks, int TotalCount)> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
        {
            return await _queryRepository.GetTaskList(
                request.SortOrder,
                request.FilterStatus,
                request.PageNumber,
                request.PageSize,
                request.UserId,
                request.IsDeleted
            );
        }
    }
}