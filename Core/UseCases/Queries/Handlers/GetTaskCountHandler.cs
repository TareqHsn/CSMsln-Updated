// Core/UseCases/Queries/Handlers/GetTaskCountHandler.cs
using MediatR;
using CSM.Core.Interfaces;

namespace CSM.Core.UseCases.Queries.Handlers
{
    public class GetTaskCountHandler : IRequestHandler<GetTaskCountQuery, int>
    {
        private readonly ITaskQueryRepository _queryRepository;

        public GetTaskCountHandler(ITaskQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<int> Handle(GetTaskCountQuery request, CancellationToken cancellationToken)
        {
            return await _queryRepository.GetTaskCount(request.UserId, request.IsDeleted);
        }
    }
}