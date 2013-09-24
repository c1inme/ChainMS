using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CMS.Entities.ServerObjects
{
    [Table("Manufacture")]
    public partial class Manufacture : Entity
    {

        #region Fields
        private string m_Name;
        private string m_Description;
        private string m_HomePage;
        private string m_IconImage;
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
        [StringLength(1000, MinimumLength = 0)]
        public string HomePage
        {
            get
            {
                return this.m_HomePage;
            }
            set
            {
                this.m_HomePage = value;
                RaisePropertyChanged("HomePage");
            }
        }
        //------------------------ 
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

        #endregion

        #region Constructor method
        public Manufacture()
        {
            this.m_Name = "";
            this.m_Description = "";
            this.m_HomePage = "";

        }
        #endregion

        #region Properties relation
        #endregion

        #region List Properties relation
        public virtual ICollection<Gallery> ListGallery { get; set; }
        public virtual ICollection<Product> ListProduct { get; set; }
        #endregion

    }
}