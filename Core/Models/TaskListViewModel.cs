using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.Models
{
    public class TaskListViewModel
    {
        public IEnumerable<Entities.Tasks> Tasks { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SortOrder { get; set; }
        public string FilterStatus { get; set; }
        public string Status { get; set; }
    }
}
