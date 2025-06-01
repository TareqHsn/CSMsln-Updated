using CSM.Core.Interfaces;
using CSM.Core.UseCases.Queries;

namespace CSM.Application.Services
{
    public class TaskQueryService : ITaskQueryUseCase
    {
        private readonly ITaskQueryRepository _queryRepository;

        public TaskQueryService(ITaskQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<Core.Entities.Tasks> GetTaskById(GetTaskByIdQuery query)
        {
            return await _queryRepository.GetTaskById(query.TaskId);
        }

        public async Task<(IEnumerable<Core.Entities.Tasks> Tasks, int TotalCount)> GetTaskList(GetTaskListQuery query)
        {
            return await _queryRepository.GetTaskList(query.SortOrder, query.FilterStatus, query.PageNumber, query.PageSize, query.UserId, query.IsDeleted);
        }

        public async Task<int> GetTaskCount(GetTaskCountQuery query)
        {
            return await _queryRepository.GetTaskCount(query.UserId, query.IsDeleted);
        }
    }
}
