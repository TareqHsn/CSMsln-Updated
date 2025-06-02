using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
    }
}
