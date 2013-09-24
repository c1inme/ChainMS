using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlLibrary
{
    public class SubmitEvent
    {
        public object ObjectSubmit { get; private set; }

        private SubmitEvent() { }

        public SubmitEvent(object objectSubmit)
        {
            ObjectSubmit = objectSubmit;
        }
    }
}
