using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.AssemblyDefinitions
{
    public class PropertyDefinition
    {
        public PropertyDefinition()
        {
        }
        public PropertyDefinition(string nameProperty, Type typeProperty, PropertyInfo valueProperty = null)
        {
            NameProperty = nameProperty;
            TypeProperty = typeProperty;
            ValueProperty = valueProperty;
        }
        public string NameProperty{get;set;}
        public Type TypeProperty { get; set; }
        public PropertyInfo ValueProperty { get; set; }

    }
}
