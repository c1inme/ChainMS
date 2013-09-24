using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ServerObjects
{
    [Table("GroupMemberPermission")]
    public partial class GroupMemberPermission : Entity
    {

        #region Fields
        private Guid? m_IDUser;
        private Guid? m_IDGroupPermission;

        #endregion

        #region Properties
        public Guid? IDUser
        {
            get
            {
                return this.m_IDUser;
            }
            set
            {
                this.m_IDUser = value;
                RaisePropertyChanged("IDUser");
            }
        }
        //------------------------ 
        public Guid? IDGroupPermission
        {
            get
            {
                return this.m_IDGroupPermission;
            }
            set
            {
                this.m_IDGroupPermission = value;
                RaisePropertyChanged("IDGroupPermission");
            }
        }
        //------------------------ 

        #endregion

        #region Constructor method
        public GroupMemberPermission()
        {
            this.m_IDUser = Guid.NewGuid();
            this.m_IDGroupPermission = Guid.NewGuid();

        }
        #endregion

        #region Properties relation

        private GroupPermission m_GroupPermission;

        [ForeignKey("IDGroupPermission")]
        public GroupPermission GroupPermission
        {
            get { return m_GroupPermission; }
            set
            {
                m_GroupPermission = value;
                RaisePropertyChanged("GroupPermission");
                if (value != null)
                    IDGroupPermission = value.GuidId;
            }
        }

        private Users m_Users;

        [ForeignKey("IDUser")]
        public Users Users
        {
            get { return m_Users; }
            set
            {
                m_Users = value;
                RaisePropertyChanged("Users");
                if (value != null)
                    IDUser = value.GuidId;
            }
        }
        #endregion

        #region List Properties relation
        #endregion

    }
}