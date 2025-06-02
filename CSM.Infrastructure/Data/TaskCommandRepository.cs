using CSM.Core.Entities;
using CSM.Core.Interfaces;

namespace CSM.Infrastructure.Data
{
    public class TaskCommandRepository : ITaskCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskCommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveTask(Tasks task)
        {
            await _context.Tasks.AddAsync(task);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateTaskById(Tasks task)
        {
            var existingTask = await _context.Tasks.FindAsync(task.Id);
            if (existingTask == null) return 0;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.IsCompleted = task.IsCompleted;
            existingTask.UserId = task.UserId;

            _context.Tasks.Update(existingTask);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateTaskStatusById(Tasks task)
        {
            var existingTask = await _context.Tasks.FindAsync(task.Id);
            if (existingTask == null) return 0;

            existingTask.IsCompleted = task.IsCompleted;
            _context.Tasks.Update(existingTask);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SoftDeleteTaskById(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null) return 0;

            task.IsDeleted = 1;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RestoreTaskById(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null) return 0;

            task.IsDeleted = 0;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> PermanentDeleteTaskById(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null) return 0;

            _context.Tasks.Remove(task);
            return await _context.SaveChangesAsync();
        }
    }
}