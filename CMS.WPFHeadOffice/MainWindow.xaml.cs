using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ControlLibrary;
using Fluent;

namespace CMS.WPFHeadOffice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            ribbon.DataContext = this;
            DataContext = this;
            this.Title = Global.Instance.GetLangByKey("General_Title");
        }

        #region Singleton
        private static MainWindow instance;
        public static MainWindow Instance
        {
            get { return instance; }
        }
        #endregion

        public void SetLoading(bool value)
        {
            if (this.ContentContainer == null)
                return;
            this.ContentContainer.IsLoadingContent = value;
        }

        public void AddTabItem(FrameworkElement item, string xName, object displayName = null)
        {
            SetLoading(true);
            if (this.ContentContainer == null)
                return;
            this.ContentContainer.AddTabItem(item, xName, displayName);
            SetLoading(false);
        }


        private void SelectedTabByName(string xName)
        {
            if (this.ContentContainer == null)
                return;
            this.ContentContainer.SelectTabByName(xName);
        }

        private bool CheckTabByName(string xName)
        {
            if (this.ContentContainer == null)
                return false;
            return this.ContentContainer.CheckTabByName(xName);
        }

        #region Category tab
        private void CreateGroupCustomer_Click(object sender, RoutedEventArgs e)
        {
            string tabName = "CreateGroupCustomer";
            if (this.CheckTabByName(tabName))
            {
                this.SelectedTabByName(tabName);
            }
            Views.FormGroupCustomerSupplier group = new Views.FormGroupCustomerSupplier();
            group.Discriminator = EnumGroupCustomerSupplier.Customer;
            this.AddTabItem(group, tabName, new FLabel() { TextKey = "Ribbon_Category_GroupCustomer" });
        }

        private void CreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            string tabName = "CreateCustomer";
            if (this.CheckTabByName(tabName))
            {
                this.SelectedTabByName(tabName);
            }
            UsersForm f = new UsersForm();
            this.AddTabItem(f, tabName, new FLabel() { TextKey = "Ribbon_Category_Customer"});
        }

        private void GroupSupplier_Click(object sender, RoutedEventArgs e)
        {
            string tabName = "CreateGroupSupplier";
            if (this.CheckTabByName(tabName))
            {
                this.SelectedTabByName(tabName);
            }
            Views.FormGroupCustomerSupplier group = new Views.FormGroupCustomerSupplier();
            group.Discriminator = EnumGroupCustomerSupplier.Supplier;
            this.AddTabItem(group, tabName, new FLabel() { TextKey = "Ribbon_Category_GroupSupplier" });
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PhanCap_Click(object sender, RoutedEventArgs e)
        {

        }

    
      




    }
}
