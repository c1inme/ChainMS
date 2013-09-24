using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.TraceChange
{
    public class InfoChange
    {
        public string TableChange { get; set; }
        public string PropertyChange { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public int VersionChange { get; set; }
        public Guid GuiIdChange { get; set; }
        public InfoChange(string tableChange,string propertyChange,string oldValue,string newValue,int versionChange,Guid guidEntity )
        {
            TableChange = tableChange;
            PropertyChange = propertyChange;
            OldValue = oldValue;
            NewValue = newValue;
            VersionChange = versionChange;
            GuiIdChange = guidEntity;
        }
    }
}
