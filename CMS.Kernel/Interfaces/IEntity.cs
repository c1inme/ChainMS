using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Kernel
{
    public interface IEntity
    {
        long EntityId
        {
            get;
            set;
        }
        Nullable<System.Guid> CreateBy { get; set; }
         Nullable<System.Guid> ModifyBy { get; set; }
         System.DateTime ModifyDate { get; set; }
         System.DateTime CreateDate { get; set; }
         Nullable<decimal> VersionNumber { get; set; }
         System.Guid GuidId { get; set; }
         Nullable<bool> Deleted { get; set; }
    }
}
