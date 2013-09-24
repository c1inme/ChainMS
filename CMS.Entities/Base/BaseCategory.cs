using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Entities
{
    public  class BaseCategory : Entity
    {
         #region Fields
        private Guid? m_ParentId;
        private string m_Name;
        private string m_Description;
        private int? m_Order;
        private string m_IconImage;
        private string m_Link;
        #endregion

        #region Properties
        public Guid? ParentId
        {
            get
            {
                return this.m_ParentId;
            }
            set
            {
                this.m_ParentId = value;
                RaisePropertyChanged("ParentId");
            }
        }
        //------------------------ 
        [Required]
        [StringLength(255, MinimumLength = 0)]
        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
                RaisePropertyChanged("Name");
            }
        }
        //------------------------ 
        [StringLength(1000, MinimumLength = 0)]
        [DataType(DataType.MultilineText)]
        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
                RaisePropertyChanged("Description");
            }
        }
        //------------------------ 
        //------------------------ 
        [StringLength(1000, MinimumLength = 0)]
        public string Link
        {
            get
            {
                return this.m_Link;
            }
            set
            {
                this.m_Link = value;
                RaisePropertyChanged("Link");
            }
        }
        //------------------------ 
        public int? Order
        {
            get
            {
                return this.m_Order;
            }
            set
            {
                this.m_Order = value;
                RaisePropertyChanged("Order");
            }
        }
        //------------------------ 
        [StringLength(1000, MinimumLength = 0)]
        public string IconImage
        {
            get
            {
                return this.m_IconImage;
            }
            set
            {
                this.m_IconImage = value;
                RaisePropertyChanged("IconImage");
            }
        }
        #endregion

        #region Constructor method
        public BaseCategory()
        {
            
            this.m_Name = "";
            this.m_Description = "";

        }
        #endregion
        [NotMapped]
        public virtual CategoryCollection ListBaseCategory { get; set; }
    }
}
