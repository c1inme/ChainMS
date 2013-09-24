using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ControlLibrary
{
    /// <summary>
    /// Provides a base implementation for objects that are displayed in the UI.
    /// </summary>
    public abstract class Displayable
        : NotifyPropertyChanged
    {

        private bool isDeleted;

        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; OnPropertyChanged("IsDeleted"); }
        }

        private object displayName;

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public object DisplayName
        {
            get { return this.displayName; }
            set
            {
                if (this.displayName != value) {
                    this.displayName = value;
                    OnPropertyChanged("DisplayName");
                }
            }
        }

        FrameworkElement contentPresenter;
        public FrameworkElement ContentPresenter
        {
            get { return contentPresenter; }
            set { 
                contentPresenter = value;
                OnPropertyChanged("ContentPresenter");
            }
        }

        public string XName
        {
            get
            {
                if (this.contentPresenter == null)
                    return string.Empty;
                return this.contentPresenter.Name;
            }
        }
    }
}
