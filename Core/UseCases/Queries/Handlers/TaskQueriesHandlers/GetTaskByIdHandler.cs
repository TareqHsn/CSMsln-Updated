using CSM.Core.Entities;
using CSM.Core.Interfaces.ITasks;
using CSM.Core.UseCases.Queries.TaskQueries;
using MediatR;

namespace CSM.Core.UseCases.Queries.Handlers.TaskQueriesHandlers
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, Tasks>
    {
        private readonly ITaskQueryRepository _queryRepository;

        public GetTaskByIdHandler(ITaskQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<Tasks> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _queryRepository.GetTaskById(request.TaskId);
        }
    }
}
