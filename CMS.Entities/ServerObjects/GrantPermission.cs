using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ServerObjects
{
    [Table("GrantPermission")]
    public partial class GrantPermission : Entity
    {

        #region Fields
        private Guid? m_IDGranted;
        private Guid? m_IDDefinitionPermission;
        private string m_Discriminator;

        #endregion

        #region Properties
        public Guid? IDGranted
        {
            get
            {
                return this.m_IDGranted;
            }
            set
            {
                this.m_IDGranted = value;
                RaisePropertyChanged("IDGranted");
            }
        }
        //------------------------ 
        public Guid? IDDefinitionPermission
        {
            get
            {
                return this.m_IDDefinitionPermission;
            }
            set
            {
                this.m_IDDefinitionPermission = value;
                RaisePropertyChanged("IDDefinitionPermission");
            }
        }
        //------------------------ 
        [StringLength(30, MinimumLength = 0)]
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

        #endregion

        #region Constructor method
        public GrantPermission()
        {
            this.m_IDGranted = Guid.NewGuid();
            this.m_IDDefinitionPermission = Guid.NewGuid();
            this.m_Discriminator = "";

        }
        #endregion

        #region Properties relation

        private GroupPermission m_GroupPermission;

        [ForeignKey("IDGranted")]
        public GroupPermission GroupPermission
        {
            get { return m_GroupPermission; }
            set
            {
                m_GroupPermission = value;
                RaisePropertyChanged("GroupPermission");
                if (value != null)
                    IDGranted = value.GuidId;
            }
        }

        private PermissionDefinition m_PermissionDefinition;

        [ForeignKey("IDDefinitionPermission")]
        public PermissionDefinition PermissionDefinition
        {
            get { return m_PermissionDefinition; }
            set
            {
                m_PermissionDefinition = value;
                RaisePropertyChanged("PermissionDefinition");
                if (value != null)
                    IDDefinitionPermission = value.GuidId;
            }
        }
        #endregion

        #region List Properties relation
        #endregion

    }
}