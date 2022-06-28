using System;

namespace OMS.DataAccess.Shared.Models
{
    public class AuditLog
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
