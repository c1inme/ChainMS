using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlLibrary;
using CMS.Entities.ClientObjects;
using System.Collections.ObjectModel;
using CMS.Entities;
using System.ComponentModel;

namespace CMS.WPFHeadOffice.Views
{
    /// <summary>
    /// Interaction logic for ListGroupCustomerSupplier.xaml
    /// </summary>
    public partial class ListGroupCustomerSupplier : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        WCFService.ServicesClient.ClientService ServiceClient;
        public ListGroupCustomerSupplier()
        {
            InitializeComponent();
            ServiceClient = new WCFService.ServicesClient.ClientService();
            mainTree.DataContext = this;
            this.Loaded += ListGroupCustomerSupplier_Loaded;
        }

        void ListGroupCustomerSupplier_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= ListGroupCustomerSupplier_Loaded;
            GetData(Discriminator.ToString());
        }

        public EnumGroupCustomerSupplier Discriminator { get; set; }
        private ObservableCollection<COGroupCustomerSupplier> itemSource;

        public ObservableCollection<COGroupCustomerSupplier> ItemSource
        {
            get
            {
                if (itemSource == null)
                    itemSource = new ObservableCollection<COGroupCustomerSupplier>();
                return itemSource;
            }
            set
            {
                itemSource = value;
                RaisePropertyChanged("ItemSource");
            }
        }




        public void GetData(string discriminator)
        {
            try
            {
                MainWindow.Instance.SetLoading(true);
                if (itemSource == null)
                    itemSource = new ObservableCollection<COGroupCustomerSupplier>();
                if (discriminator.ToLower().Equals("both"))
                    discriminator = string.Empty;
                var result = new ObservableCollection<COGroupCustomerSupplier>(ServiceClient.GetAllCOGroupCustomerSupplier(discriminator).OrderBy(f => f.NameGroup));
                ItemSource.Clear();
                ItemSource = result;
                
                MainWindow.Instance.SetLoading(false);
            }
            catch (Exception ex)
            {
                ModernDialog.Show(ex);
            }
        }

        public event EventHandler<SubmitEvent> AddItem;
        public event EventHandler<SubmitEvent> UpdateItem;
        public event EventHandler DeletedItem;
        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            if (AddItem != null)
                if (mainTree.SelectedItem is COGroupCustomerSupplier)
                    AddItem(this, new SubmitEvent(new COGroupCustomerSupplier() { Parent = (COGroupCustomerSupplier)mainTree.SelectedItem }));
                else
                    AddItem(this, new SubmitEvent(new COGroupCustomerSupplier()));
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            //var itemToSelect = ServiceClient.GetByIdCOGroupCustomerSupplier(this.Discriminator.ToString(), 5);
            //  mainTree.ItemToSelected = itemToSelect;
            //SetItemSelected(this.mainTree, itemToSelect);
            if (UpdateItem != null)
                UpdateItem(this, new SubmitEvent(mainTree.SelectedItem));
        }

        private void MenuDeleted_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(mainTree.SelectedItem is COGroupCustomerSupplier))
                    return;
                var result = ModernDialog.Show(Global.Instance.GetLangByKey("General_ConfirmMessage"), Global.Instance.GetLangByKey("General_TitleConfirm"), MessageBoxButton.YesNo);
                if (!result.HasValue || !result.Value)
                    return;


                //if (DG1.SelectedItems != null && DG1.SelectedItems.Count > 1)
                //{
                //    var itemsToDeleted = new ObservableCollection<COGroupCustomerSupplier>(DG1.SelectedItems.Cast<COGroupCustomerSupplier>());
                //    ServiceClient.DeleteGroupCustomerSuppliers(itemsToDeleted);
                //    foreach (var obj in itemsToDeleted)
                //        itemSource.Remove(obj);
                //}
                //else
                //{
                ServiceClient.DeleteGroupCustomerSupplier((mainTree.SelectedItem as Entity).GuidId);
                itemSource.Remove((mainTree.SelectedItem as COGroupCustomerSupplier));
                //}
                if (DeletedItem != null)
                    DeletedItem(this, EventArgs.Empty);

            }
            catch (Exception ex)
            {
                ModernDialog.Show(ex);
            }
        }

        private void MenuRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetData(Discriminator.ToString());
        }


    }
}
