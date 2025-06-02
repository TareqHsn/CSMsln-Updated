
namespace CSM.Application.DTOs.TaskDTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public string UserId { get; set; }
        public int? IsDeleted { get; set; }
    }
}
