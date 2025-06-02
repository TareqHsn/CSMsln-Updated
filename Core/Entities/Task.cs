using System.ComponentModel.DataAnnotations;


namespace CSM.Core.Entities
{
    public class Tasks
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? DueDate { get; set; }

        public string? UserId { get; set; }
        public int IsDeleted { get; set; }
    }
}
