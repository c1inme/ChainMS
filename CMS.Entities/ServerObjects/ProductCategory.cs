using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CMS.Entities.ServerObjects
{
    [Table("ProductCategory")]
    public partial class ProductCategory : Entity
    {

        #region Fields
        private Guid? m_ParentId;
        private Guid? m_LanguageId;
        private Language m_Lang;
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

        [NotMapped]
        public HttpPostedFileBase Picture
        {
            get;
            set;
        }


        [ForeignKey("LanguageId")]
        public Language Lang
        {
            get { return m_Lang; }
            set { m_Lang = value; }
        }
        public Guid? LanguageId
        {
            get { return m_LanguageId; }
            set { m_LanguageId = value; }
        }
        #endregion

        #region Constructor method
        public ProductCategory()
        {

            this.m_Name = "";
            this.m_Description = "";

        }
        #endregion

        #region Properties relation

        private ProductCategory m_ParentProductCategory;

        [ForeignKey("ParentId")]
        public ProductCategory ParentProductCategory
        {
            get { return m_ParentProductCategory; }
            set
            {
                m_ParentProductCategory = value;
                RaisePropertyChanged("ParentProductCategory");
                if (value != null)
                    ParentId = value.GuidId;
            }
        }
        #endregion

        #region List Properties relation
        [NotMapped]
        public virtual ICollection<Gallery> ListGallery { get; set; }
        [NotMapped]
        public virtual ICollection<Product> ListProduct { get; set; }
        [NotMapped]
        public virtual List<ProductCategory> ListProductCategory { get; set; }

        [NotMapped]
        //[InverseProperty("ProductCategory")]
        public virtual ICollection<RelationOfProperties> ListRelationOfProperties { get; set; }

        [NotMapped]
        public virtual ICollection<PropertiesDefinition> ListProperty { get; set; }
        #endregion

    }
}