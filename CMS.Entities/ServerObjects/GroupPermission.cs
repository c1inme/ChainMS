using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ServerObjects
{
    [Table("GroupPermission")]
    public partial class GroupPermission : Entity
    {

        #region Fields
        private string m_CodeGroup;
        private string m_NameGroup;
        private string m_Description;

        #endregion

        #region Properties
        [Required]
        [StringLength(100, MinimumLength = 0)]
        [UIHint("ReadOnly")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        public string CodeGroup
        {
            get
            {
                return this.m_CodeGroup;
            }
            set
            {
                this.m_CodeGroup = value;
                RaisePropertyChanged("CodeGroup");
            }
        }
        //------------------------ 
        [Required]
        [StringLength(255, MinimumLength = 0)]
        public string NameGroup
        {
            get
            {
                return this.m_NameGroup;
            }
            set
            {
                this.m_NameGroup = value;
                RaisePropertyChanged("NameGroup");
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

        #endregion

        #region Constructor method
        public GroupPermission()
        {
            this.m_CodeGroup = "";
            this.m_NameGroup = "";
            this.m_Description = "";

        }
        #endregion

        #region Properties relation
        #endregion

        #region List Properties relation
        public virtual ICollection<GrantPermission> ListGrantPermission { get; set; }
        public virtual ICollection<GroupMemberPermission> ListGroupMemberPermission { get; set; }
        #endregion

    }
}