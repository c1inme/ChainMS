using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using CMS.Entities.ClientObjects;

namespace CMS.WPFHeadOffice.Views
{
    /// <summary>
    /// Interaction logic for FormGroupCustomerSupplier.xaml
    /// </summary>
    public partial class FormGroupCustomerSupplier : UserControl
    {
        public FormGroupCustomerSupplier()
        {
            InitializeComponent();
            lstGroup.AddItem += lstGroup_AddItem;
            lstGroup.UpdateItem += lstGroup_UpdateItem;
            this.Group.CancelForm += Group_CancelForm;
            this.Group.SubmitSucceed += Group_SubmitSucceed;
        }

        public EnumGroupCustomerSupplier Discriminator
        {
            set
            {
                if (lstGroup != null)
                    lstGroup.Discriminator = value;
                if (Group != null)
                    Group.Discriminator = value;
            }
        }

        void Group_SubmitSucceed(object sender, ControlLibrary.SubmitEvent e)
        {
            //this.lstGroup.ItemSource.Insert(0, e.ObjectSubmit as COGroupCustomerSupplier);
            this.lstGroup.GetData(this.lstGroup.Discriminator.ToString());
        }

        void lstGroup_UpdateItem(object sender, ControlLibrary.SubmitEvent e)
        {
            this.Group.GroupCustomerInfo = e.ObjectSubmit as COGroupCustomerSupplier;
            this.Group.Mode = ModeForm.Edit;
            if (this.Group.Visibility != System.Windows.Visibility.Visible)
                this.Group.Visibility = System.Windows.Visibility.Visible;
        }

        void Group_CancelForm(object sender, EventArgs e)
        {
            if (this.Group.Visibility != System.Windows.Visibility.Collapsed)
                this.Group.Visibility = System.Windows.Visibility.Collapsed;
        }

        void lstGroup_AddItem(object sender, ControlLibrary.SubmitEvent e)
        {
            this.Group.GroupCustomerInfo = e.ObjectSubmit as COGroupCustomerSupplier;
            this.Group.Mode = ModeForm.Add;
            if (this.Group.Visibility != System.Windows.Visibility.Visible)
                this.Group.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
