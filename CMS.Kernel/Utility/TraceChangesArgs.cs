using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary.TraceChange;

namespace CMS.Kernel
{
    public class TraceChangesArgs : EventArgs
    {
        public InfoChangeCollection ChangeCollection { get; set; }
        public TraceChangesArgs()
        {
        }
        public TraceChangesArgs(InfoChangeCollection collection)
        {
            this.ChangeCollection = collection;
        }
    }
}
