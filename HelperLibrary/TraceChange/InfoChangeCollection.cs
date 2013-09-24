using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.TraceChange
{
    public class InfoChangeCollection : ObservableCollection<InfoChange>
    {
        public string NameTable
        {
            get;
            set;
        }
        public InfoChangeCollection(string nameTable = null)
        {
            this.NameTable = nameTable;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="infoChanges"/> class that contains specified links.
        /// </summary>
        /// <param name="infoChanges">The links that are copied to this collection.</param>
        public InfoChangeCollection(IEnumerable<InfoChange> infoChanges, string nameTable)
        {
            if (infoChanges == null)
            {
                throw new ArgumentNullException("InfoChange");
            }
            foreach (var it in infoChanges)
            {
                Add(it);
            }
            this.NameTable = nameTable;
        }
    }
}
