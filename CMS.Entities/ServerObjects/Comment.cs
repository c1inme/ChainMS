using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ServerObjects
{
    [Table("Comment")]
    public partial class Comment : Entity
    {

        #region Fields
        private Guid? m_IdFather;
        private Guid m_IdBelong;
        private int? m_Discriminator;
        private string m_Content;
        private Guid? m_UserId;

        #endregion

        #region Properties
        [ScaffoldColumn(false)]
        public Guid? IdFather
        {
            get
            {
                return this.m_IdFather;
            }
            set
            {
                this.m_IdFather = value;
                RaisePropertyChanged("IdFather");
            }
        }
        //------------------------ 
        [Required]
        [ScaffoldColumn(false)]
        public Guid IdBelong
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
        public int? Discriminator
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
        //------------------------ 
        [DataType(DataType.MultilineText)]
        public string Content
        {
            get
            {
                return this.m_Content;
            }
            set
            {
                this.m_Content = value;
                RaisePropertyChanged("Content");
            }
        }
        //------------------------ 
        public Guid? UserId
        {
            get
            {
                return this.m_UserId;
            }
            set
            {
                this.m_UserId = value;
                RaisePropertyChanged("UserId");
            }
        }
        //------------------------ 

        #endregion

        #region Constructor method
        public Comment()
        {
            this.m_IdFather = Guid.NewGuid();
            this.m_IdBelong = Guid.NewGuid();
            this.m_Discriminator = 0;
            this.m_Content = "";
            this.m_UserId = Guid.NewGuid();

        }
        #endregion

        #region Properties relation

        private Comment m_ParentComment;

        [ForeignKey("IdFather")]
        public Comment ParentComment
        {
            get { return m_ParentComment; }
            set
            {
                m_ParentComment = value;
                RaisePropertyChanged("ParentComment");
                if (value != null)
                    IdFather = value.GuidId;
            }
        }

        private Gallery m_Gallery;

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

        private Image m_Image;

        [ForeignKey("IdBelong")]
        public Image Image
        {
            get { return m_Image; }
            set
            {
                m_Image = value;
                RaisePropertyChanged("Image");
                if (value != null)
                    IdBelong = value.GuidId;
            }
        }

        private News m_News;

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
        public virtual ICollection<Image> ListImage { get; set; }
        public virtual ICollection<Rating> ListRating { get; set; }
        #endregion

    }
}