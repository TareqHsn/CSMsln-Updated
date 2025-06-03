using CSM.Core.Entities;
using CSM.Core.Interfaces.ITasks;
using CSM.Core.UseCases.Queries.TaskQueries;
using MediatR;

namespace CSM.Application.UseCases.Queries.Handlers.TaskQueriesHandlers
{
    public class GetTaskListHandler : IRequestHandler<GetTaskListQuery, (IEnumerable<Tasks> Tasks, int TotalCount)>
    {
        private readonly ITaskQueryRepository _queryRepository;

        public GetTaskListHandler(ITaskQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<(IEnumerable<Tasks> Tasks, int TotalCount)> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
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
