using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.AssemblyDefinitions
{
    public class EntityDefinitionCollection : ObservableCollection<EntityDefinition>
    {

        public string NameSpace
        {
            get;
            set;
        }
        public EntityDefinitionCollection(string nameSpace = null)
        {
            this.NameSpace = nameSpace;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDefinitionCollection"/> class that contains specified links.
        /// </summary>
        /// <param name="entityDefinition">The links that are copied to this collection.</param>
        public EntityDefinitionCollection(IEnumerable<EntityDefinition> entityDefinition,string nameSpace)
        {
            if (entityDefinition == null)
            {
                throw new ArgumentNullException("entityDefinition");
            }
            foreach (var it in entityDefinition)
            {
                Add(it);
            }
            this.NameSpace = nameSpace;
        }
    }
}
