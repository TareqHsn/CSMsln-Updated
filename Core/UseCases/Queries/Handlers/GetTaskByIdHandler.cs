using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSM.Core.Entities;
using CSM.Core.Interfaces;
using MediatR;

namespace CSM.Core.UseCases.Queries.Handlers
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, Entities.Tasks>
    {
        private readonly ITaskQueryRepository _queryRepository;

        public GetTaskByIdHandler(ITaskQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<Entities.Tasks> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _queryRepository.GetTaskById(request.TaskId);
        }
    }
}
