using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ServerObjects
{
    [Table("Rating")]
    public partial class Rating : Entity
    {

        #region Fields
        private Guid? m_IdBelong;
        private int? m_Discriminator;
        private int? m_SumRating;
        private int? m_CountCurrent;

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
        public int? SumRating
        {
            get
            {
                return this.m_SumRating;
            }
            set
            {
                this.m_SumRating = value;
                RaisePropertyChanged("SumRating");
            }
        }
        //------------------------ 
        public int? CountCurrent
        {
            get
            {
                return this.m_CountCurrent;
            }
            set
            {
                this.m_CountCurrent = value;
                RaisePropertyChanged("CountCurrent");
            }
        }
        //------------------------ 

        #endregion

        #region Constructor method
        public Rating()
        {
            this.m_IdBelong = Guid.NewGuid();
            this.m_Discriminator = 0;
            this.m_SumRating = 0;
            this.m_CountCurrent = 0;

        }
        #endregion

        #region Properties relation

        private Comment m_Comment;

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
        #endregion

    }
}