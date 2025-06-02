using CSM.Core.Entities;
using CSM.Core.Interfaces.ITasks;
using CSM.Core.UseCases.Queries.TaskQueries;

namespace CSM.Application.Services.TaskServices
{
    public class TaskQueryService : ITaskQueryUseCase
    {
        private readonly ITaskQueryRepository _queryRepository;

        public TaskQueryService(ITaskQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<Tasks> GetTaskById(GetTaskByIdQuery query)
        {
            return await _queryRepository.GetTaskById(query.TaskId);
        }

        public async Task<(IEnumerable<Tasks> Tasks, int TotalCount)> GetTaskList(GetTaskListQuery query)
        {
            return await _queryRepository.GetTaskList(query.SortOrder, query.FilterStatus, query.PageNumber, query.PageSize, query.UserId, query.IsDeleted);
        }

        public async Task<int> GetTaskCount(GetTaskCountQuery query)
        {
            return await _queryRepository.GetTaskCount(query.UserId, query.IsDeleted);
        }
    }
}
