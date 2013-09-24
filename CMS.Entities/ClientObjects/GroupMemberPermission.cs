//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMS.Entities.ClientObjects
{
    using CMS.Kernel;
    using System;
    using System.Collections.Generic;

    public partial class GroupMemberPermission : IEntity
    {
        public long Id { get; set; }
        public Nullable<System.Guid> CreateBy { get; set; }
        public Nullable<System.Guid> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<decimal> VersionNumber { get; set; }
        public System.Guid GuidId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.Guid> IDUser { get; set; }
        public Nullable<System.Guid> IDGroupPermission { get; set; }
    
        public virtual GroupPermission GroupPermission { get; set; }
        public virtual User User { get; set; }
    }
}
