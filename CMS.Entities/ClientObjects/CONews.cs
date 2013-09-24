using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ClientObjects
{
    [Table("CONews")]
    public partial class CONews : Entity
    {
        private string m_Title;
        private Guid? m_MenuId;
        private string m_ImagePath;
        private string m_Description;
        private string m_Content;
        private string m_Tags;
        private bool? m_IsActive;
        private bool? m_IsHot;
        private int? m_ViewNumber;
        private DateTime? m_PublishDate;
        private DateTime? m_DateExpired;
        private string m_Link;
        private string m_Discriminator;
        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
                RaisePropertyChanged("Title");
            }
        }
        //------------------------ 
        public Guid? MenuId
        {
            get
            {
                return this.m_MenuId;
            }
            set
            {
                this.m_MenuId = value;
                RaisePropertyChanged("MenuId");
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
        //------------------------ 
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
        public string Tags
        {
            get
            {
                return this.m_Tags;
            }
            set
            {
                this.m_Tags = value;
                RaisePropertyChanged("Tags");
            }
        }
        //------------------------ 
        public bool? IsActive
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
        public bool? IsHot
        {
            get
            {
                return this.m_IsHot;
            }
            set
            {
                this.m_IsHot = value;
                RaisePropertyChanged("IsHot");
            }
        }
        //------------------------ 
        public int? ViewNumber
        {
            get
            {
                return this.m_ViewNumber;
            }
            set
            {
                this.m_ViewNumber = value;
                RaisePropertyChanged("ViewNumber");
            }
        }
        //------------------------ 
        public DateTime? PublishDate
        {
            get
            {
                return this.m_PublishDate;
            }
            set
            {
                this.m_PublishDate = value;
                RaisePropertyChanged("PublishDate");
            }
        }
        //------------------------ 
        public DateTime? DateExpired
        {
            get
            {
                return this.m_DateExpired;
            }
            set
            {
                this.m_DateExpired = value;
                RaisePropertyChanged("DateExpired");
            }
        }
        //------------------------ 
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
        //------------------------ 



        //Khởi tạo đối tượng rỗng 


        public CONews()
        {
            this.m_Title = "";
            this.m_MenuId = Guid.NewGuid();
            this.m_ImagePath = "";
            this.m_Description = "";
            this.m_Content = "";
            this.m_Tags = "";
            this.m_IsActive = false;
            this.m_IsHot = false;
            this.m_ViewNumber = 0;
            this.m_PublishDate = DateTime.Now;
            this.m_DateExpired = DateTime.Now;
            this.m_Link = "";
            this.m_Discriminator = "";

        }
        #region Properties relation
        #endregion

    }
}