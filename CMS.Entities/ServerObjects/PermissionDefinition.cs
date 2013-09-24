using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ServerObjects
{
    [Table("PermissionDefinition")]
    public partial class PermissionDefinition : Entity
    {

        #region Fields
        private string m_CodePermision;
        private string m_NamePermission;
        private string m_Description;
        private string m_ActionType;
        private int? m_SortNumber;

        #endregion

        #region Properties
        [Required]
        [StringLength(100, MinimumLength = 0)]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        [UIHint("ReadOnly")]
        public string CodePermision
        {
            get
            {
                return this.m_CodePermision;
            }
            set
            {
                this.m_CodePermision = value;
                RaisePropertyChanged("CodePermision");
            }
        }
        //------------------------ 
        [Required]
        [StringLength(255, MinimumLength = 0)]
        public string NamePermission
        {
            get
            {
                return this.m_NamePermission;
            }
            set
            {
                this.m_NamePermission = value;
                RaisePropertyChanged("NamePermission");
            }
        }
        //------------------------ 
        [StringLength(1000, MinimumLength = 0)]
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
        [StringLength(10, MinimumLength = 0)]
        public string ActionType
        {
            get
            {
                return this.m_ActionType;
            }
            set
            {
                this.m_ActionType = value;
                RaisePropertyChanged("ActionType");
            }
        }
        //------------------------ 
        public int? SortNumber
        {
            get
            {
                return this.m_SortNumber;
            }
            set
            {
                this.m_SortNumber = value;
                RaisePropertyChanged("SortNumber");
            }
        }
        //------------------------ 

        #endregion

        #region Constructor method
        public PermissionDefinition()
        {
            this.m_CodePermision = "";
            this.m_NamePermission = "";
            this.m_Description = "";
            this.m_ActionType = "";
            this.m_SortNumber = 0;

        }
        #endregion

        #region Properties relation
        #endregion

        #region List Properties relation
        public virtual ICollection<GrantPermission> ListGrantPermission { get; set; }
        public virtual ICollection<Users> ListUsers { get; set; }
        #endregion

    }
}