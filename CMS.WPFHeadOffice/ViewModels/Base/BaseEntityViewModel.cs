using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Kernel;

namespace CMS.WPFHeadOffice.ViewModels
{
    public  class BaseEntityViewModel : BaseViewModel , IEntity
    {
        public BaseEntityViewModel() : base()
        {
        }

        public IEntity BaseEntity { get; set; }
        public BaseEntityViewModel(IEntity entity) : base()
        {
            BaseEntity = entity;
        }

        public long Id
        {
            get { return BaseEntity.EntityId; }
            set
            {
                BaseEntity.EntityId = value;
                this.OnPropertyChanged("Id");
            }
        }
        public Nullable<System.Guid> CreateBy
        {
            get { return BaseEntity.CreateBy; }
            set
            {
                BaseEntity.CreateBy = value;
                this.OnPropertyChanged("CreateBy");
            }
        }
        public Nullable<System.Guid> ModifyBy
        {
            get { return BaseEntity.ModifyBy; }
            set
            {
                BaseEntity.ModifyBy = value;
                this.OnPropertyChanged("ModifyBy");
            }
        }
        public System.DateTime ModifyDate
        {
            get { return BaseEntity.ModifyDate; }
            set
            {
                BaseEntity.ModifyDate = value;
                this.OnPropertyChanged("ModifyDate");
            }
        }
        public System.DateTime CreateDate
        {
            get { return BaseEntity.CreateDate; }
            set
            {
                BaseEntity.CreateDate = value;
                this.OnPropertyChanged("CreateDate");
            }
        }
        public decimal VersionNumber
        {
            get { return BaseEntity.VersionNumber; }
            set
            {
                BaseEntity.VersionNumber = value;
                this.OnPropertyChanged("VersionNumber");
            }
        }
        public System.Guid GuidId
        {
            get { return BaseEntity.GuidId; }
            set
            {
                BaseEntity.GuidId = value;
                this.OnPropertyChanged("GuidId");
            }
        }
        public Nullable<bool> Deleted
        {
            get { return BaseEntity.Deleted; }
            set
            {
                BaseEntity.Deleted = value;
                this.OnPropertyChanged("Deleted");
            }
        }
    }
}
