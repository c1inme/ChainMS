using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ClientObjects
{
    [Table("COGrantPermission")]
    public partial class COGrantPermission : Entity
    {
        private Guid? m_IDGranted;
        private Guid? m_IDDefinitionPermission;
        private string m_Discriminator;
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


        public COGrantPermission()
        {
            this.m_IDGranted = Guid.NewGuid();
            this.m_IDDefinitionPermission = Guid.NewGuid();
            this.m_Discriminator = "";

        }
        #region Properties relation

        private COPermissionDefinition m_COPermissionDefinition;

        [ForeignKey("IDDefinitionPermission")]
        public COPermissionDefinition COPermissionDefinition
        {
            get { return m_COPermissionDefinition; }
            set
            {
                m_COPermissionDefinition = value;
                RaisePropertyChanged("COPermissionDefinition");
                if (value != null)
                    IDDefinitionPermission = value.GuidId;
            }
        }
        #endregion

    }
}