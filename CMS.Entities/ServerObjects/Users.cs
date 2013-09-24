using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CMS.Entities.ServerObjects
{
    [Table("Users")]
    public partial class Users : Entity
    {
        private string m_Alias;
        private string m_Password;
        private string m_FirstName;
        private bool m_IsApproved;
        private DateTime m_LastPasswordChangedDate;
        private DateTime m_LastLoginDate;
        private DateTime m_LastActivityDate;
        private DateTime m_LastLockoutDate;
        private DateTime m_LastPasswordFailureDate;
        private bool m_IsLockedOut;
        private int m_PasswordFailuresSinceLastSuccess;
        private string m_FullName;
        private string m_Email;
        private string m_MobileUser;
        private bool? m_IsAdmin = false;
        private string m_ImagePath;
        private string m_LastName;
        private bool m_IsSystem;

        public bool IsSystem
        {
            get { return m_IsSystem; }
            set { m_IsSystem = value; }
        }
        public string LastName
        {
            get { return m_LastName; }
            set { m_LastName = value; }
        }
        public DateTime LastPasswordFailureDate
        {
            get { return m_LastPasswordFailureDate; }
            set { m_LastPasswordFailureDate = value; }
        }

        public bool IsLockedOut
        {
            get { return m_IsLockedOut; }
            set { m_IsLockedOut = value; }
        }

        public DateTime LastLockoutDate
        {
            get { return m_LastLockoutDate; }
            set { m_LastLockoutDate = value; }
        }

        public DateTime LastActivityDate
        {
            get { return m_LastActivityDate; }
            set { m_LastActivityDate = value; }
        }

        public DateTime LastLoginDate
        {
            get { return m_LastLoginDate; }
            set { m_LastLoginDate = value; }
        }
        public int PasswordFailuresSinceLastSuccess
        {
            get { return m_PasswordFailuresSinceLastSuccess; }
            set { m_PasswordFailuresSinceLastSuccess = value; }
        }
        public DateTime LastPasswordChangedDate
        {
            get { return m_LastPasswordChangedDate; }
            set { m_LastPasswordChangedDate = value; }
        }

        public bool IsApproved
        {
            get { return m_IsApproved; }
            set { m_IsApproved = value; }
        }

        public string FirstName
        {
            get { return m_FirstName; }
            set { m_FirstName = value; }
        }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
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
        [Required]
        [DataType(DataType.Password)]
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
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email
        {
            get
            {
                return this.m_Email;
            }
            set
            {
                this.m_Email = value;
                RaisePropertyChanged("Email");
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

        [NotMapped]
        public HttpPostedFileBase Avartar
        {
            get;
            set;
        }

        //Khởi tạo đối tượng rỗng 


        public Users()
        {
            this.m_Alias = "";
            this.m_Password = "";
            this.m_FullName = "";
            this.m_Email = "";
            this.m_MobileUser = "";
            this.m_IsAdmin = false;
            this.m_ImagePath = "";

        }
        #region Properties relation
        public virtual ICollection<GroupMemberPermission> ListGroupMemberPermission { get; set; }
        public virtual ICollection<PermissionDefinition> ListPermissionDefinition { get; set; }

        #endregion


    }
}