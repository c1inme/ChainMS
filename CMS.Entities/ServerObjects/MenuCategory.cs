using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CMS.Entities.ServerObjects
{
    [Table("MenuCategory")]
    public partial class MenuCategory : Entity
    {

        #region Fields
        private string m_Name;
        private Guid? m_ParentId;
        private int? m_Order;
        private string m_IconImage;
        private string m_Description;
        private bool m_IsShowHome;
        private bool m_IsActive;
        private string m_Link;
        private Guid? m_LanguageId;
        private Language m_Lang;

        
        #endregion

        #region Properties
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
                RaisePropertyChanged("MenuName");
            }
        }
        //------------------------ 
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

        [NotMapped]
        public HttpPostedFileBase Picture
        {
            get;
            set;
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
        public bool IsShowHome
        {
            get
            {
                return this.m_IsShowHome;
            }
            set
            {
                this.m_IsShowHome = value;
                RaisePropertyChanged("IsShowHome");
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
        public Guid? LanguageId
        {
            get { return m_LanguageId; }
            set { m_LanguageId = value; }
        }
        #endregion

        #region Constructor method
        public MenuCategory()
        {
            this.m_Name = "";
            this.m_Order = 0;
            this.m_IconImage = "";
            this.m_Description = "";
            this.m_IsShowHome = false;
            this.m_IsActive = false;
            this.m_Link = "";
            this.ListMenuCategory = null;
            this.ListGallery = null;
            this.ListNews = null;
        }
        #endregion



        #region Properties relation
        [ForeignKey("LanguageId")]
        public Language Lang
        {
            get { return m_Lang; }
            set { m_Lang = value; }
        }
     


        private MenuCategory m_ParentMenuCategory;

        [ForeignKey("ParentId")]
        public MenuCategory ParentMenuCategory
        {
            get { return m_ParentMenuCategory; }
            set
            {
                m_ParentMenuCategory = value;
                RaisePropertyChanged("ParentMenuCategory");
                if (value != null)
                    ParentId = value.GuidId;
            }
        }
        #endregion

        #region List Properties relation
        [NotMapped]
        public virtual ICollection<Gallery> ListGallery { get; set; }
        [NotMapped]
        public virtual List<MenuCategory> ListMenuCategory { get; set; }
        [NotMapped]
        public virtual ICollection<News> ListNews { get; set; }
        #endregion

    }
}