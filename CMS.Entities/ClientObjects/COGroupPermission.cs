using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ClientObjects
{
    [Table("COGroupPermission")]
    public partial class COGroupPermission : Entity
    {
        private string m_CodeGroup;
        private string m_NameGroup;
        private string m_Description;
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



        //Khởi tạo đối tượng rỗng 


        public COGroupPermission()
        {
            this.m_CodeGroup = "";
            this.m_NameGroup = "";
            this.m_Description = "";

        }
        #region Properties relation
      
        public virtual ICollection<COGroupMemberPermission> ListCOGroupMemberPermission { get; set; }
        #endregion
    }
}