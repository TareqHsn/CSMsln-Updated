using CSM.Core.Entities;

namespace CSM.Core.Models.Task_VM
{
    public class TaskListViewModel
    {
        public IEnumerable<Tasks> Tasks { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SortOrder { get; set; }
        public string FilterStatus { get; set; }
        public string Status { get; set; }
    }
}
