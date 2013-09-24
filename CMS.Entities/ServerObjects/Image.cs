using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CMS.Entities.ServerObjects
{
    [Table("Image")]
    public partial class Image : Entity
    {

        #region Fields
        private Guid? m_IdBelong;
        private string m_Discriminator;
        private string m_Name;

        private string m_SmallPath;
        private string m_Description;
        private string m_FullHdPath;
        private string m_ThumpnailPath;

        #endregion

        #region Properties
        [ScaffoldColumn(false)]
        public Guid? IdBelong
        {
            get
            {
                return this.m_IdBelong;
            }
            set
            {
                this.m_IdBelong = value;
                RaisePropertyChanged("IdBelong");
            }
        }
        //------------------------ 

        public string Discriminator
        {
            get
            {
                return this.m_Discriminator;
            }
            set
            {
                this.m_Discriminator = value;
                RaisePropertyChanged("Discriminator");
            }
        }

        [StringLength(255, MinimumLength = 5)]
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
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
        public string FullHdPath
        {
            get
            {
                return this.m_FullHdPath;
            }
            set
            {
                this.m_FullHdPath = value;
                RaisePropertyChanged("FullHdPath");
            }
        }

        //------------------------ 
        [StringLength(1000, MinimumLength = 0)]
        public string SmallPath
        {
            get
            {
                return this.m_SmallPath;
            }
            set
            {
                this.m_SmallPath = value;
                RaisePropertyChanged("SmallPath");
            }
        }
        //------------------------ 
        [StringLength(1000, MinimumLength = 0)]
        public string ThumpnailPath
        {
            get
            {
                return this.m_ThumpnailPath;
            }
            set
            {
                this.m_ThumpnailPath = value;
                RaisePropertyChanged("ThumpnailPath");
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
        public Image()
        {
            this.m_IdBelong = Guid.NewGuid();
            this.m_Discriminator = "";
            this.m_Description = "";
            this.m_FullHdPath = "";
            this.m_ThumpnailPath = "";

        }
        #endregion

        #region Properties relation

        private Comment m_Comment;

        [NotMapped]
        [ForeignKey("IdBelong")]
        public Comment Comment
        {
            get { return m_Comment; }
            set
            {
                m_Comment = value;
                RaisePropertyChanged("Comment");
                if (value != null)
                    IdBelong = value.GuidId;
            }
        }

        private Gallery m_Gallery;

        [NotMapped]
        [ForeignKey("IdBelong")]
        public Gallery Gallery
        {
            get { return m_Gallery; }
            set
            {
                m_Gallery = value;
                RaisePropertyChanged("Gallery");
                if (value != null)
                    IdBelong = value.GuidId;
            }
        }

        private News m_News;

        [NotMapped]
        [ForeignKey("IdBelong")]
        public News News
        {
            get { return m_News; }
            set
            {
                m_News = value;
                RaisePropertyChanged("News");
                if (value != null)
                    IdBelong = value.GuidId;
            }
        }

        private Product m_Product;

        [NotMapped]
        [ForeignKey("IdBelong")]
        public Product Product
        {
            get { return m_Product; }
            set
            {
                m_Product = value;
                RaisePropertyChanged("Product");
                if (value != null)
                    IdBelong = value.GuidId;
            }
        }
        #endregion

        #region List Properties relation
        public virtual ICollection<Comment> ListComment { get; set; }
        public virtual ICollection<Rating> ListRating { get; set; }
        #endregion

    }
}
