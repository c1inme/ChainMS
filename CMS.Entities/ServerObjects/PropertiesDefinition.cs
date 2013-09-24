using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Entities.ServerObjects
{
    [Table("PropertiesDefinition")]
    public class PropertiesDefinition : Entity
    {
        private string _NameProperty;
        public string NameProperty
        {
            get { return _NameProperty; }
            set { _NameProperty = value; }
        }

        private string _Description;
        [DataType(DataType.MultilineText)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

    }
}
