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

    public partial class CustomerSupplier : IEntity
    {
        public long Id { get; set; }
        public Nullable<System.Guid> CreateBy { get; set; }
        public Nullable<System.Guid> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<decimal> VersionNumber { get; set; }
        public System.Guid GuidId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CodeSC { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> GoupID { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<System.Guid> ContactID { get; set; }
        public string PrimaryEmail { get; set; }
        public Nullable<decimal> LiabilitiesLimited { get; set; }
        public Nullable<int> Discount { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsAvaiable { get; set; }
    }
}