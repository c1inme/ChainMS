using CMS.Entities.ClientObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CMS.WPFHeadOffice.Views
{
    /// <summary>
    /// Interaction logic for GroupCustomerSupplierForm.xaml
    /// </summary>
    public partial class GroupCustomerSupplierForm : UserControl
    {
        public GroupCustomerSupplierForm()
        {
            InitializeComponent();
            Groups = new GroupCustomerSupplierListViewModel();
            GroupCustomerSupplierViewModel viewModel = new GroupCustomerSupplierViewModel();
            viewModel.CodeGroup = "Code 001";
            viewModel.NameGroup = "KH Lẻ";
            
            viewModel.Parent = new GroupCustomerSupplier() { CodeGroup = "01", NameGroup = "KH Tạp hóa" };
            Groups.Add(new GroupCustomerSupplier() { CodeGroup = "01", NameGroup = "KH Tạp hóa" });

            viewModel.CodeGroup = "Code 002";
            viewModel.NameGroup = "KH sỉ";

            viewModel.Parent = new GroupCustomerSupplier() { CodeGroup = "01", NameGroup = "KH Tạp hóa" };

        }
        public GroupCustomerSupplierListViewModel Groups
        {
            get;
            set;
        }

        //ObservableCollection<GroupCustomerSupplier> Groups
    }
}
