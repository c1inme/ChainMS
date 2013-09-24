using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HelperLibrary.AssemblyDefinitions
{
    public class PropertyDefinitionCollection : ObservableCollection<PropertyDefinition>
    {
        public string EnityName
        {
            get;
            set;
        }
        public PropertyDefinitionCollection(string entityName = null)
        {
            this.EnityName = entityName;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDefinitionCollection"/> class that contains specified links.
        /// </summary>
        /// <param name="PropertyDefinitions">The links that are copied to this collection.</param>
        public PropertyDefinitionCollection(IEnumerable<PropertyDefinition> PropertyDefinitions, string entityName = null)
        {
            if (PropertyDefinitions == null)
            {
                throw new ArgumentNullException("PropertyDefinitions");
            }
            foreach (var it in PropertyDefinitions)
            {
                Add(it);
            }

            this.EnityName = entityName;
        }
    }
}
