using CSM.Core.Interfaces.ITasks;
using CSM.Core.UseCases.Queries.TaskQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Application.UseCases.Queries.Handlers.TaskQueriesHandlers
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
