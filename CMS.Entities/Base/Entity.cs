using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CMS.Kernel;

namespace CMS.Entities
{
    public abstract class Entity : BaseViewModel, INotifyPropertyChanged, IEntity
    {

        #region Private field
        private long m_EntityId;
        private Guid? m_CreateBy;
        private Guid? m_ModifyBy;
        private DateTime m_ModifyDate;
        private DateTime m_CreateDate;
        private decimal? m_VersionNumber;
        private Guid m_GuidId;
        private bool? m_Deleted;
        //private  Users m_CreateByUser;
        //private  Users m_ModifyByUser;

        #endregion

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public Entity()
        {
            // Id = 0;
            CreateDate = DateTime.Now;
            ModifyDate = DateTime.Now;
            Deleted = false;
            VersionNumber = 0;
            GuidId = Guid.NewGuid();
        }

        #region Property
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EntityId
        {
            get
            {
                return this.m_EntityId;
            }
            set
            {
                this.m_EntityId = value;
                RaisePropertyChanged("Id");
            }
        }
        //------------------------ 

        [ScaffoldColumn(false)]
        public Guid? CreateBy
        {
            get
            {
                return this.m_CreateBy;
            }
            set
            {
                this.m_CreateBy = value;
                RaisePropertyChanged("CreateBy");
            }
        }
        //------------------------ 
        [ScaffoldColumn(false)]
        public Guid? ModifyBy
        {
            get
            {
                return this.m_ModifyBy;
            }
            set
            {
                this.m_ModifyBy = value;
                RaisePropertyChanged("ModifyBy");
            }
        }
        //------------------------ 
        [ScaffoldColumn(false)]
        public DateTime ModifyDate
        {
            get
            {
                return this.m_ModifyDate;
            }
            set
            {
                this.m_ModifyDate = value;
                RaisePropertyChanged("ModifyDate");
            }
        }
        //------------------------ 
        [ScaffoldColumn(false)]
        public DateTime CreateDate
        {
            get
            {
                return this.m_CreateDate;
            }
            set
            {
                this.m_CreateDate = value;
                RaisePropertyChanged("CreateDate");
            }
        }
        //------------------------ 
        [ScaffoldColumn(false)]
        public decimal? VersionNumber
        {
            get
            {
                return this.m_VersionNumber;
            }
            set
            {
                this.m_VersionNumber = value;
                RaisePropertyChanged("VersionNumber");
            }
        }
        //------------------------ 

        [Key]
        public Guid GuidId
        {
            get
            {
                return this.m_GuidId;
            }
            set
            {
                this.m_GuidId = value;
                RaisePropertyChanged("GuidId");
            }
        }
        //------------------------ 
        [ScaffoldColumn(false)]
        public bool? Deleted
        {
            get
            {
                return this.m_Deleted;
            }
            set
            {
                this.m_Deleted = value;
                RaisePropertyChanged("Deleted");
            }
        }

        //[ScaffoldColumn(false)]
        //[ForeignKey("ModifyBy")]
        //public Users ModifyByUser
        //{
        //    get { return m_ModifyByUser; }
        //    set { m_ModifyByUser = value; }
        //}

        //[ScaffoldColumn(false)]
        //[ForeignKey("CreateBy")]
        //public Users CreateByUser
        //{
        //    get { return m_CreateByUser; }
        //    set { m_CreateByUser = value; }
        //}
        #endregion

        public override bool Equals(object obj)
        {
            if (this != null && obj != null)
                return this.GuidId == ((Entity)obj).GuidId;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            // Which is preferred?
            if (this != null)
                return this.GuidId.GetHashCode();
            return base.GetHashCode();
            //return this.FooId.GetHashCode();
        }
    }
}
