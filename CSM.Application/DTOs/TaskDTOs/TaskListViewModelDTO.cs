
namespace CSM.Application.DTOs.TaskDTOs
{
    public class TaskListViewModelDTO
    {
        public IEnumerable<TaskDTO> Tasks { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SortOrder { get; set; }
        public string FilterStatus { get; set; }
        public string Status { get; set; }
    }
}
