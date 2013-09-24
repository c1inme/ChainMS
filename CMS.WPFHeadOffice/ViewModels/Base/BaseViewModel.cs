using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Kernel;
using ControlLibrary;

namespace CMS.WPFHeadOffice.ViewModels
{
    public class BaseViewModel :NotifyPropertyChanged, IDisposable, IDataErrorInfo
    {
        [NonSerialized]
        protected readonly DataErrorInfoSupport dataErrorInfoSupport;

        public BaseViewModel()
        {
            dataErrorInfoSupport = new DataErrorInfoSupport(this);
            
        }


        string IDataErrorInfo.Error { get { return dataErrorInfoSupport.Error; } }

        string IDataErrorInfo.this[string memberName] { get { return dataErrorInfoSupport[memberName]; } }

        public virtual void Dispose()
        {

        }
    }
}
