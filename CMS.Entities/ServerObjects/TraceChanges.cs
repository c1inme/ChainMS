using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ServerObjects
{
    [Table("TraceChanges")]
    public partial class TraceChanges : Entity
    {

        #region Fields
        private string m_TableChange;
        private string m_PropertyChange;
        private string m_OldValue;
        private string m_NewValue;
        private int? m_VersionChange;
        private Guid? m_GuiIdChange;

        #endregion

        #region Properties
        public string TableChange
        {
            get
            {
                return this.m_TableChange;
            }
            set
            {
                this.m_TableChange = value;
                RaisePropertyChanged("TableChange");
            }
        }
        //------------------------ 
        public string PropertyChange
        {
            get
            {
                return this.m_PropertyChange;
            }
            set
            {
                this.m_PropertyChange = value;
                RaisePropertyChanged("PropertyChange");
            }
        }
        //------------------------ 
        public string OldValue
        {
            get
            {
                return this.m_OldValue;
            }
            set
            {
                this.m_OldValue = value;
                RaisePropertyChanged("OldValue");
            }
        }
        //------------------------ 
        public string NewValue
        {
            get
            {
                return this.m_NewValue;
            }
            set
            {
                this.m_NewValue = value;
                RaisePropertyChanged("NewValue");
            }
        }
        //------------------------ 
        public int? VersionChange
        {
            get
            {
                return this.m_VersionChange;
            }
            set
            {
                this.m_VersionChange = value;
                RaisePropertyChanged("VersionChange");
            }
        }
        //------------------------ 
        public Guid? GuiIdChange
        {
            get
            {
                return this.m_GuiIdChange;
            }
            set
            {
                this.m_GuiIdChange = value;
                RaisePropertyChanged("GuiIdChange");
            }
        }
        //------------------------ 

        #endregion

        #region Constructor method
        public TraceChanges()
        {
            this.m_TableChange = "";
            this.m_PropertyChange = "";
            this.m_OldValue = "";
            this.m_NewValue = "";
            this.m_VersionChange = 0;
            this.m_GuiIdChange = Guid.NewGuid();

        }
        #endregion

        #region Properties relation
        #endregion

        #region List Properties relation
        #endregion

    }
}