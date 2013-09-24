using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ServerObjects
{
    [Table("Gallery")]
    public partial class Gallery : Entity
    {

        #region Fields
        private string m_Name;
        private Guid? m_IDBelong;
        private string m_Description;
        private string m_Link;
        private string m_SourceUrl;
        private int? m_SortOrder;
        private bool m_IsActive;
        private string m_TypeEnum;
        private bool m_IsSystem;
        #endregion

        #region Properties
        public bool IsSystem
        {
            get { return m_IsSystem; }
            set { m_IsSystem = value; }
        }

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
        public Guid? IDBelong
        {
            get
            {
                return this.m_IDBelong;
            }
            set
            {
                this.m_IDBelong = value;
                RaisePropertyChanged("IDBelong");
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
        [StringLength(1000, MinimumLength = 0)]
        public string SourceUrl
        {
            get
            {
                return this.m_SourceUrl;
            }
            set
            {
                this.m_SourceUrl = value;
                RaisePropertyChanged("SourceUrl");
            }
        }
        //------------------------ 
        public int? SortOrder
        {
            get
            {
                return this.m_SortOrder;
            }
            set
            {
                this.m_SortOrder = value;
                RaisePropertyChanged("SortOrder");
            }
        }
        //------------------------ 
        public bool IsActive
        {
            get
            {
                return this.m_IsActive;
            }
            set
            {
                this.m_IsActive = value;
                RaisePropertyChanged("IsActive");
            }
        }
        //------------------------ 
        public string TypeEnum
        {
            get
            {
                return this.m_TypeEnum;
            }
            set
            {
                this.m_TypeEnum = value;
                RaisePropertyChanged("TypeEnum");
            }
        }
        //------------------------ 

        #endregion

        #region Constructor method
        public Gallery()
        {
            this.m_Name = "";
            
            this.m_Description = "";
            this.m_Link = "";
            this.m_SourceUrl = "";
            this.m_SortOrder = 0;
            this.m_IsActive = false;

        }
        #endregion

        #region Properties relation

        private Manufacture m_Manufacture;

        public Manufacture Manufacture
        {
            get { return m_Manufacture; }
            set
            {
                m_Manufacture = value;
                RaisePropertyChanged("Manufacture");
                if (value != null)
                    IDBelong = value.GuidId;
            }
        }

        private MenuCategory m_MenuCategory;

 
        public MenuCategory MenuCategory
        {
            get { return m_MenuCategory; }
            set
            {
                m_MenuCategory = value;
                RaisePropertyChanged("MenuCategory");
                if (value != null)
                    IDBelong = value.GuidId;
            }
        }

        private News m_News;

   
        public News News
        {
            get { return m_News; }
            set
            {
                m_News = value;
                RaisePropertyChanged("News");
                if (value != null)
                    IDBelong = value.GuidId;
            }
        }

        private Product m_Product;

        public Product Product
        {
            get { return m_Product; }
            set
            {
                m_Product = value;
                RaisePropertyChanged("Product");
                if (value != null)
                    IDBelong = value.GuidId;
            }
        }

        private ProductCategory m_ProductCategory;

        public ProductCategory ProductCategory
        {
            get { return m_ProductCategory; }
            set
            {
                m_ProductCategory = value;
                RaisePropertyChanged("ProductCategory");
                if (value != null)
                    IDBelong = value.GuidId;
            }
        }
        #endregion

        #region List Properties relation
        public virtual ICollection<Comment> ListComment { get; set; }
        public virtual ICollection<Image> ListImage { get; set; }
        public virtual ICollection<Rating> ListRating { get; set; }
        #endregion

    }
}