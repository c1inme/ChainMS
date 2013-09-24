using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ClientObjects
{
    [Table("COCustomerSupplier")]
    public partial class COCustomerSupplier : Entity
    {
        private string m_CodeSC;
        private string m_Name;
        private Guid? m_GoupID;
        private string m_Address;
        private string m_TaxCode;
        private string m_PhoneNumber;
        private Guid? m_ContactID;
        private string m_PrimaryEmail;
        private decimal? m_LiabilitiesLimited;
        private int? m_Discount;
        private string m_Description;
        private bool? m_IsAvaiable;

        [Required(ErrorMessage = "General_Required")]
        public string CodeSC
        {
            get
            {
                return this.m_CodeSC;
            }
            set
            {
                this.m_CodeSC = value;
                RaisePropertyChanged("CodeSC");
            }
        }
        //------------------------ 
        [Required(ErrorMessage = "General_Required")]
        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
                RaisePropertyChanged("Name");
            }
        }
        //------------------------ 
        public Guid? GoupID
        {
            get
            {
                return this.m_GoupID;
            }
            set
            {
                this.m_GoupID = value;
                RaisePropertyChanged("GoupID");
            }
        }
        //------------------------ 
        public string Address
        {
            get
            {
                return this.m_Address;
            }
            set
            {
                this.m_Address = value;
                RaisePropertyChanged("Address");
            }
        }
        //------------------------ 
        public string TaxCode
        {
            get
            {
                return this.m_TaxCode;
            }
            set
            {
                this.m_TaxCode = value;
                RaisePropertyChanged("TaxCode");
            }
        }
        //------------------------ 
        public string PhoneNumber
        {
            get
            {
                return this.m_PhoneNumber;
            }
            set
            {
                this.m_PhoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }
        //------------------------ 
        public Guid? ContactID
        {
            get
            {
                return this.m_ContactID;
            }
            set
            {
                this.m_ContactID = value;
                RaisePropertyChanged("ContactID");
            }
        }
        //------------------------ 
        [Required(ErrorMessage = "General_Required")]
        public string PrimaryEmail
        {
            get
            {
                return this.m_PrimaryEmail;
            }
            set
            {
                this.m_PrimaryEmail = value;
                RaisePropertyChanged("PrimaryEmail");
            }
        }
        //------------------------ 
        public decimal? LiabilitiesLimited
        {
            get
            {
                return this.m_LiabilitiesLimited;
            }
            set
            {
                this.m_LiabilitiesLimited = value;
                RaisePropertyChanged("LiabilitiesLimited");
            }
        }
        //------------------------ 
        public int? Discount
        {
            get
            {
                return this.m_Discount;
            }
            set
            {
                this.m_Discount = value;
                RaisePropertyChanged("Discount");
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
        public bool? IsAvaiable
        {
            get
            {
                return this.m_IsAvaiable;
            }
            set
            {
                this.m_IsAvaiable = value;
                RaisePropertyChanged("IsAvaiable");
            }
        }
        //------------------------ 



        //Khởi tạo đối tượng rỗng 


        public COCustomerSupplier()
        {
            this.m_CodeSC = "";
            this.m_Name = "";
            this.m_GoupID = Guid.NewGuid();
            this.m_Address = "";
            this.m_TaxCode = "";
            this.m_PhoneNumber = "";
            this.m_ContactID = Guid.NewGuid();
            this.m_PrimaryEmail = "";
            this.m_LiabilitiesLimited = 0;
            this.m_Discount = 0;
            this.m_Description = "";
            this.m_IsAvaiable = false;

        }
        #region Properties relation
        #endregion

    }
}