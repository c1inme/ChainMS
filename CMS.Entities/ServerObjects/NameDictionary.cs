using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ServerObjects
{
    [Table("NameDictionary")]
    public partial class NameDictionary : Entity
    {

        #region Fields
        private string m_InternalName;
        private string m_DisplayName;
        private string m_TableName;
        private bool? m_IsLookup;
        private string m_TableLookup;
        private string m_PropertyLookupDisplay;

        #endregion

        #region Properties
        public string InternalName
        {
            get
            {
                return this.m_InternalName;
            }
            set
            {
                this.m_InternalName = value;
                RaisePropertyChanged("InternalName");
            }
        }
        //------------------------ 
        public string DisplayName
        {
            get
            {
                return this.m_DisplayName;
            }
            set
            {
                this.m_DisplayName = value;
                RaisePropertyChanged("DisplayName");
            }
        }
        //------------------------ 
        public string TableName
        {
            get
            {
                return this.m_TableName;
            }
            set
            {
                this.m_TableName = value;
                RaisePropertyChanged("TableName");
            }
        }
        //------------------------ 
        public bool? IsLookup
        {
            get
            {
                return this.m_IsLookup;
            }
            set
            {
                this.m_IsLookup = value;
                RaisePropertyChanged("IsLookup");
            }
        }
        //------------------------ 
        public string TableLookup
        {
            get
            {
                return this.m_TableLookup;
            }
            set
            {
                this.m_TableLookup = value;
                RaisePropertyChanged("TableLookup");
            }
        }
        //------------------------ 
        public string PropertyLookupDisplay
        {
            get
            {
                return this.m_PropertyLookupDisplay;
            }
            set
            {
                this.m_PropertyLookupDisplay = value;
                RaisePropertyChanged("PropertyLookupDisplay");
            }
        }
        //------------------------ 

        #endregion

        #region Constructor method
        public NameDictionary()
        {
            this.m_InternalName = "";
            this.m_DisplayName = "";
            this.m_TableName = "";
            this.m_IsLookup = false;
            this.m_TableLookup = "";
            this.m_PropertyLookupDisplay = "";

        }
        #endregion

        #region Properties relation
        #endregion

        #region List Properties relation
        #endregion

    }
}