using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ClientObjects
{
    [Table("COGroupCustomerSupplier")]
    public partial class COGroupCustomerSupplier : Entity
    {
        private string m_CodeGroup;
        private string m_NameGroup;
        private string m_Description;
        private Guid? m_IDBelong;
        private string m_Discriminator;

        [Required(ErrorMessage = "General_Required")]
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
        [Required(ErrorMessage = "General_Required")]
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
        public Guid? IDBelong
        {
            get
            {
                return this.m_IDBelong;
            }
            set
            {
                this.m_IDBelong = value;
                RaisePropertyChanged("IDBelong");
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
        public COGroupCustomerSupplier()
            : base()
        {
            this.m_CodeGroup = "";
            this.m_NameGroup = "";
            this.m_Description = "";
            this.m_IDBelong = null;
            this.m_Discriminator = "";
            this.ListCOGroupCustomerSupplier = new HashSet<COGroupCustomerSupplier>();

        }
        #region Properties relation
        COGroupCustomerSupplier m_Parent;

        [ForeignKey("IDBelong")]
        public COGroupCustomerSupplier Parent
        {
            get { return m_Parent; }
            set
            {
                m_Parent = value;

                RaisePropertyChanged("Parent");
                if (value != null)
                    IDBelong = value.GuidId;
            }
        }

        ICollection<COGroupCustomerSupplier> m_ListCOGroupCustomerSupplier;
        public virtual ICollection<COGroupCustomerSupplier> ListCOGroupCustomerSupplier
        {
            get { return m_ListCOGroupCustomerSupplier; }
            set { m_ListCOGroupCustomerSupplier = value; RaisePropertyChanged("ListCOGroupCustomerSupplier"); }
        }
        #endregion

    }
}