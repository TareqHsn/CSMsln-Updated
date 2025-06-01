using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSM.Core.Interfaces;
using CSM.Core.UseCases.Commands;

namespace CSM.Application.Services
{
    public class TaskCommandService : ITaskCommandUseCase
    {
        private readonly ITaskCommandRepository _commandRepository;

        public TaskCommandService(ITaskCommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task<int> CreateTask(CreateTaskCommand command)
        {
            return await _commandRepository.SaveTask(command.Task);
        }

        public async Task<int> UpdateTask(UpdateTaskCommand command)
        {
            return await _commandRepository.UpdateTaskById(command.Task);
        }

        public async Task<int> UpdateTaskStatus(UpdateTaskStatusCommand command)
        {
            return await _commandRepository.UpdateTaskStatusById(new Core.Entities.Tasks { Id = command.Id, IsCompleted = command.IsCompleted });
        }

        public async Task<int> SoftDeleteTask(SoftDeleteTaskCommand command)
        {
            return await _commandRepository.SoftDeleteTaskById(command.TaskId);
        }

        public async Task<int> RestoreTask(RestoreTaskCommand command)
        {
            return await _commandRepository.RestoreTaskById(command.TaskId);
        }

        public async Task<int> PermanentDeleteTask(PermanentDeleteTaskCommand command)
        {
            return await _commandRepository.PermanentDeleteTaskById(command.TaskId);
        }
    }
}
