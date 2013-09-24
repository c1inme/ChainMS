using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CMS.Entities.ServerObjects
{
    [Table("Product")]
    public partial class Product : Entity
    {

        #region Fields
        private string m_Code;
        private string m_Name;
        private string m_Description;
        private Guid? m_ProductCategoryId;
        private decimal? m_SellPrice;
        private decimal? m_BuyPrice;
        private Guid? m_ManufactureId;
        private decimal? m_CurrentStock;
        private decimal? m_Stock;
        private bool? m_IsAvailable;
        private float m_Rating;
        private string m_ImagePath;

        #endregion

        #region Properties
        [Required]
        [StringLength(100, MinimumLength = 0)]
        [UIHint("ReadOnly")]
        public string Code
        {
            get
            {
                return this.m_Code;
            }
            set
            {
                this.m_Code = value;
                RaisePropertyChanged("Code");
            }
        }
        //------------------------ 
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
        public Guid? ProductCategoryId
        {
            get
            {
                return this.m_ProductCategoryId;
            }
            set
            {
                this.m_ProductCategoryId = value;
                RaisePropertyChanged("ProductCategoryId");
            }
        }
        //------------------------ 
        //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        //[DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? SellPrice
        {
            get
            {
                return this.m_SellPrice;
            }
            set
            {
                
                this.m_SellPrice = Convert.ToDecimal(value);
                RaisePropertyChanged("SellPrice");
            }
        }
        //------------------------ 
        //[DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal? BuyPrice
        {
            get
            {
                return this.m_BuyPrice;
            }
            set
            {
                this.m_BuyPrice = value;
                RaisePropertyChanged("BuyPrice");
            }
        }
        //------------------------ 
        public Guid? ManufactureId
        {
            get
            {
                return this.m_ManufactureId;
            }
            set
            {
                this.m_ManufactureId = value;
                RaisePropertyChanged("ManufactureId");
            }
        }
        //------------------------ 
        public decimal? CurrentStock
        {
            get
            {
                return this.m_CurrentStock;
            }
            set
            {
                this.m_CurrentStock = value;
                RaisePropertyChanged("CurrentStock");
            }
        }
        //------------------------ 
        public decimal? Stock
        {
            get
            {
                return this.m_Stock;
            }
            set
            {
                this.m_Stock = value;
                RaisePropertyChanged("Stock");
            }
        }
        //------------------------ 
        public bool? IsAvailable
        {
            get
            {
                return this.m_IsAvailable;
            }
            set
            {
                this.m_IsAvailable = value;
                RaisePropertyChanged("IsAvailable");
            }
        }
        //------------------------ 
        [Required]
        public float Rating
        {
            get
            {
                return this.m_Rating;
            }
            set
            {
                this.m_Rating = value;
                RaisePropertyChanged("Rating");
            }
        }
        //------------------------ 
        public string ImagePath
        {
            get
            {
                return this.m_ImagePath;
            }
            set
            {
                this.m_ImagePath = value;
                RaisePropertyChanged("ImagePath");
            }
        }


        [NotMapped]
        public HttpPostedFileBase Picture
        {
            get;
            set;
        }
        //------------------------ 

        #endregion

        #region Constructor method
        public Product()
        {
            this.m_Code = "";
            this.m_Name = "";
            this.m_Description = "";
            this.m_SellPrice = 0;
            this.m_BuyPrice = 0;
            this.m_CurrentStock = 0;
            this.m_Stock = 0;
            this.m_IsAvailable = false;
            this.m_Rating = 0;
            this.m_ImagePath = "";

        }
        #endregion

        #region Properties relation

        private Manufacture m_Manufacture;

        [ForeignKey("ManufactureId")]
        public Manufacture Manufacture
        {
            get { return m_Manufacture; }
            set
            {
                m_Manufacture = value;
                RaisePropertyChanged("Manufacture");
                if (value != null)
                    ManufactureId = value.GuidId;
            }
        }

        private ProductCategory m_ProductCategory;

        [ForeignKey("ProductCategoryId")]
        public ProductCategory ProductCategory
        {
            get { return m_ProductCategory; }
            set
            {
                m_ProductCategory = value;
                RaisePropertyChanged("ProductCategory");
                if (value != null)
                    ProductCategoryId = value.GuidId;
            }
        }
        #endregion

        #region List Properties relation
        [NotMapped]
        public virtual ICollection<Comment> ListComment { get; set; }
        [NotMapped]
        public virtual ICollection<Gallery> ListGallery { get; set; }
        [NotMapped]
        public virtual ICollection<Image> ListImage { get; set; }
        [NotMapped]
        public virtual ICollection<Rating> ListRating { get; set; }

        [NotMapped]
        public virtual ICollection<News> ListNews { get; set; }


        //[NotMapped]
        [ForeignKey("IdBelong")]
        public virtual ICollection<RelationOfProperties> ListRelationOfProperties { get; set; }

        [NotMapped]
        public virtual ICollection<PropertiesDefinition> ListProperty { get; set; }
        #endregion

    }
}