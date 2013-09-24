using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ClientObjects
{
    [Table("COUsers")]
    public partial class COUsers : Entity
    {
        private string m_Alias;
        private string m_Password;
        private string m_ShortName;
        private string m_FullName;
        private string m_EmailUser;
        private string m_MobileUser;
        private bool? m_IsAdmin;
        private string m_ImagePath;
        public string Alias
        {
            get
            {
                return this.m_Alias;
            }
            set
            {
                this.m_Alias = value;
                RaisePropertyChanged("Alias");
            }
        }
        //------------------------ 
        public string Password
        {
            get
            {
                return this.m_Password;
            }
            set
            {
                this.m_Password = value;
                RaisePropertyChanged("Password");
            }
        }
        //------------------------ 
        public string ShortName
        {
            get
            {
                return this.m_ShortName;
            }
            set
            {
                this.m_ShortName = value;
                RaisePropertyChanged("ShortName");
            }
        }
        //------------------------ 
        public string FullName
        {
            get
            {
                return this.m_FullName;
            }
            set
            {
                this.m_FullName = value;
                RaisePropertyChanged("FullName");
            }
        }
        //------------------------ 
        public string EmailUser
        {
            get
            {
                return this.m_EmailUser;
            }
            set
            {
                this.m_EmailUser = value;
                RaisePropertyChanged("EmailUser");
            }
        }
        //------------------------ 
        public string MobileUser
        {
            get
            {
                return this.m_MobileUser;
            }
            set
            {
                this.m_MobileUser = value;
                RaisePropertyChanged("MobileUser");
            }
        }
        //------------------------ 
        public bool? IsAdmin
        {
            get
            {
                return this.m_IsAdmin;
            }
            set
            {
                this.m_IsAdmin = value;
                RaisePropertyChanged("IsAdmin");
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



        //Khởi tạo đối tượng rỗng 


        public COUsers()
        {
            this.m_Alias = "";
            this.m_Password = "";
            this.m_ShortName = "";
            this.m_FullName = "";
            this.m_EmailUser = "";
            this.m_MobileUser = "";
            this.m_IsAdmin = false;
            this.m_ImagePath = "";

        }
        #region Properties relation
        #endregion
        public virtual ICollection<COGroupMemberPermission> ListCOGroupMemberPermission { get; set; }

    }
}