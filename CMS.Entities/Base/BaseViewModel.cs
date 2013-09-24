using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Kernel;

namespace CMS.Entities
{
    public class BaseViewModel : IDisposable, IDataErrorInfo
    {
        [NonSerialized]
        protected readonly DataErrorSupport dataErrorInfoSupport;

        public BaseViewModel()
        {
            dataErrorInfoSupport = new DataErrorSupport(this);
            
        }


        string IDataErrorInfo.Error { get { return dataErrorInfoSupport.Error; } }

        string IDataErrorInfo.this[string memberName] { get { return dataErrorInfoSupport[memberName]; } }

        public virtual void Dispose()
        {

        }
    }
}
