// Infrastructure/Data/TaskQueryRepository.cs
using CSM.Core.Entities;
using CSM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CSM.Infrastructure.Data
{
    public class TaskQueryRepository : ITaskQueryRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskQueryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tasks> GetTaskById(int taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }

        public async Task<(IEnumerable<Tasks> Tasks, int TotalCount)> GetTaskList(string sortOrder, string filterStatus, int pageNumber, int pageSize, string userId, bool isDeleted)
        {
            var query = _context.Tasks
                .Where(t => isDeleted ? t.IsDeleted == 1 : (t.IsDeleted == 0 || t.IsDeleted == null));

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(t => t.UserId == userId);

            if (!string.IsNullOrEmpty(filterStatus) && filterStatus != "all")
                query = query.Where(t => t.IsCompleted == (filterStatus == "completed"));

            if (sortOrder == "due_date_desc")
                query = query.OrderByDescending(t => t.DueDate ?? DateTime.MaxValue);
            else if (sortOrder == "due_date_asc")
                query = query.OrderBy(t => t.DueDate ?? DateTime.MaxValue);
            else
                query = query.OrderByDescending(t => t.Id);

            var totalCount = await query.CountAsync();
            var tasks = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (tasks, totalCount);
        }

        public async Task<int> GetTaskCount(string userId, bool isDeleted)
        {
            return await _context.Tasks
                .Where(t => isDeleted ? t.IsDeleted == 1 : (t.IsDeleted == 0 || t.IsDeleted == null))
                .Where(t => string.IsNullOrEmpty(userId) || t.UserId == userId)
                .CountAsync();
        }
    }
}