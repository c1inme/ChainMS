using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Entities.ServerObjects;
namespace CMS.WebMVC
{
    public class AddPropertyModels
    {

        private string _ValueProperty;

        public string ValueProperty
        {
            get { return _ValueProperty; }
            set { _ValueProperty = value; }
        }

        bool _IsCheck;
        public bool IsCheck
        {
            get { return _IsCheck; }
            set { _IsCheck = value; }
        }
        Guid _IdBelong;
        public Guid IdBelong
        {
            get { return _IdBelong; }
            set { _IdBelong = value; }
        }
        string _Dicriminator;
        public string Dicriminator
        {
            get { return _Dicriminator; }
            set { _Dicriminator = value; }
        }

        public string NameProperty
        {
            get
            {
                if (ProperDef == null)
                    return "";
                return ProperDef.NameProperty;
            }
        }
        public Guid _GuidProperty;
        public Guid GuidProperty
        {
            get
            {

                return _GuidProperty;
            }
            set
            {
                _GuidProperty = value;
            }
        }

        PropertiesDefinition _ProperDef;
        public PropertiesDefinition ProperDef
        {
            get { return _ProperDef; }
            set { _ProperDef = value; }
        }
    }
}