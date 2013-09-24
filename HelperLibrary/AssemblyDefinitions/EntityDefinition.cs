using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.AssemblyDefinitions
{
    public class EntityDefinition
    {
        public string EntityName { get; set; }
        Type EntityType { get; set; }
        PropertyDefinitionCollection EntityProperties { get; set; }

        public EntityDefinition()
        {
        }

        public EntityDefinition(string entityName, PropertyDefinitionCollection entityProperties = null, Type entityType = null)
        {
            EntityName = entityName;
            EntityType = entityType;
            EntityProperties = entityProperties;
        }
    }
}
