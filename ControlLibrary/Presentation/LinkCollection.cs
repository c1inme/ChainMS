using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControlLibrary
{
    /// <summary>
    /// Represents an observable collection of links.
    /// </summary>
    public class LinkCollection
        : ObservableCollection<Link>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkCollection"/> class.
        /// </summary>
        public LinkCollection()
        {
        }

        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var link in e.NewItems)
                {
                    Link item = link as Link;
                    item.PropertyChanged += link_PropertyChanged;

                }
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var link in e.OldItems)
                {
                    Link item = link as Link;
                    item.PropertyChanged -= link_PropertyChanged;
                }
            }
        }
      
       
      
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkCollection"/> class that contains specified links.
        /// </summary>
        /// <param name="links">The links that are copied to this collection.</param>
        public LinkCollection(IEnumerable<Link> links)
        {
            if (links == null) {
                throw new ArgumentNullException("links");
            }
            foreach (var link in links) {
                
                Add(link);
            }
        }

        void link_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDeleted")
            {
                var _item = (Link)sender;
                if (_item.IsDeleted)
                {
                    var dispose = _item.ContentPresenter as IDisposable;
                    if (dispose != null)
                        dispose.Dispose();
                    else
                        _item.ContentPresenter = null;
                    this.Remove(_item);
                }
            }
        }

       
    }
}
