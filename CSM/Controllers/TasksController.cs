using CSM.Core.Models;
using CSM.Core.UseCases.Commands;
using CSM.Core.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSM.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        private const int PageSize = 10;

        public TasksController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<IActionResult> List(string sortOrder, string filterStatus, int pageNumber = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = filterStatus;
            ViewBag.DueDateSortParm = sortOrder == "due_date_asc" ? "due_date_desc" : "due_date_asc";

            var userId = _userManager.GetUserId(User);
            var allTasksQuery = new GetTaskListQuery { UserId = userId, PageNumber = 1, PageSize = int.MaxValue, IsDeleted = false };
            var allTasks = await _mediator.Send(allTasksQuery);
            var taskCounts = new Dictionary<string, int>
            {
                { "Completed", allTasks.Tasks.Count(t => t.IsCompleted) },
                { "Pending", allTasks.Tasks.Count(t => !t.IsCompleted) }
            };
            ViewBag.TicketCounts = taskCounts;

            var query = new GetTaskListQuery
            {
                SortOrder = sortOrder,
                FilterStatus = filterStatus,
                PageNumber = pageNumber,
                PageSize = PageSize,
                UserId = userId,
                IsDeleted = false
            };
            var (tasks, totalCount) = await _mediator.Send(query);

            var viewModel = new TaskListViewModel
            {
                Tasks = tasks,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize),
                SortOrder = sortOrder,
                FilterStatus = filterStatus,
                Status = "Active"
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Bin(string sortOrder, string filterStatus, int pageNumber = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = filterStatus;
            ViewBag.DueDateSortParm = sortOrder == "due_date_asc" ? "due_date_desc" : "due_date_asc";

            var userId = _userManager.GetUserId(User);
            var allTasksQuery = new GetTaskListQuery { UserId = userId, PageNumber = 1, PageSize = int.MaxValue, IsDeleted = true };
            var allTasks = await _mediator.Send(allTasksQuery);
            var taskCounts = new Dictionary<string, int>
            {
                { "Completed", allTasks.Tasks.Count(t => t.IsCompleted) },
                { "Pending", allTasks.Tasks.Count(t => !t.IsCompleted) }
            };
            ViewBag.TicketCounts = taskCounts;

            var query = new GetTaskListQuery
            {
                SortOrder = sortOrder,
                FilterStatus = filterStatus,
                PageNumber = pageNumber,
                PageSize = PageSize,
                UserId = userId,
                IsDeleted = true
            };
            var (tasks, totalCount) = await _mediator.Send(query);

            var viewModel = new TaskListViewModel
            {
                Tasks = tasks,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize),
                SortOrder = sortOrder,
                FilterStatus = filterStatus,
                Status = "Bin"
            };

            return View("List", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDeleteTaskIndex(int task_id, string sortOrder, string filterStatus, int pageNumber)
        {
            var command = new SoftDeleteTaskCommand { TaskId = task_id };
            var result = await _mediator.Send(command);
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Task moved to Bin successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to move task to Bin.";
            }
            return RedirectToAction("List", new { sortOrder, filterStatus, pageNumber });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreTask(int task_id, string sortOrder, string filterStatus, int pageNumber)
        {
            var command = new RestoreTaskCommand { TaskId = task_id };
            var result = await _mediator.Send(command);
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Task restored successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to restore task.";
            }
            return RedirectToAction("Bin", new { sortOrder, filterStatus, pageNumber });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PermanentDeleteTask(int task_id, string sortOrder, string filterStatus, int pageNumber)
        {
            var command = new PermanentDeleteTaskCommand { TaskId = task_id };
            var result = await _mediator.Send(command);
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Task permanently deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to permanently delete task.";
            }
            return RedirectToAction("Bin", new { sortOrder, filterStatus, pageNumber });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTaskStatus(int id, bool isCompleted, string sortOrder, string filterStatus, int pageNumber)
        {
            var command = new UpdateTaskStatusCommand { Id = id, IsCompleted = isCompleted };
            var result = await _mediator.Send(command);
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Task status updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update task status.";
            }
            return RedirectToAction(ViewBag.Status == "Bin" ? "Bin" : "List", new { sortOrder, filterStatus, pageNumber });
        }

        public async Task<IActionResult> Details(int id)
        {
            var query = new GetTaskByIdQuery { TaskId = id };
            var task = await _mediator.Send(query);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        public async Task<IActionResult> Create()
        {
            var userId = _userManager.GetUserId(User);
            ViewBag.UserId = userId;
            ViewBag.Users = await _userManager.Users.Select(u => new { u.Id, u.UserName }).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Core.Entities.Tasks task)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(task.UserId))
                {
                    task.UserId = null;
                }
                var command = new CreateTaskCommand { Task = task };
                var result = await _mediator.Send(command);
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Task created successfully.";
                    return RedirectToAction(nameof(List));
                }
                ModelState.AddModelError("", "Failed to create task.");
            }
            ViewBag.Users = await _userManager.Users.Select(u => new { u.Id, u.UserName }).ToListAsync();
            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var query = new GetTaskByIdQuery { TaskId = id };
            var task = await _mediator.Send(query);
            if (task == null)
            {
                return NotFound();
            }
            if (task.UserId != userId && task.UserId != null)
            {
                return Forbid();
            }
            ViewBag.Users = await _userManager.Users.Select(u => new { u.Id, u.UserName }).ToListAsync();
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Core.Entities.Tasks task)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var existingTaskQuery = new GetTaskByIdQuery { TaskId = task.Id };
                var existingTask = await _mediator.Send(existingTaskQuery);
                if (existingTask == null)
                {
                    return NotFound();
                }
                if (existingTask.UserId != userId && existingTask.UserId != null)
                {
                    return Forbid();
                }
                if (string.IsNullOrEmpty(task.UserId))
                {
                    task.UserId = null;
                }
                var command = new UpdateTaskCommand { Task = task };
                var result = await _mediator.Send(command);
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Task updated successfully.";
                    return RedirectToAction(nameof(List));
                }
                ModelState.AddModelError("", "Failed to update task.");
            }
            ViewBag.Users = await _userManager.Users.Select(u => new { u.Id, u.UserName }).ToListAsync();
            return View(task);
        }
    }
}
