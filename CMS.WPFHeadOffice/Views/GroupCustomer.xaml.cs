using System;
using System.Windows;
using System.Windows.Controls;
using CMS.Entities.ClientObjects;
using ControlLibrary;
using CMS.Entities;

namespace CMS.WPFHeadOffice.Views
{
    /// <summary>
    /// Interaction logic for GroupCustomer.xaml
    /// </summary>
    public partial class GroupCustomer : UserControl
    {
        public GroupCustomer()
        {
            InitializeComponent();
            this.DataContext = GroupCustomerInfo;
        }

        public ModeForm Mode
        {
            get;
            set;
        }

        public EnumGroupCustomerSupplier Discriminator { get; set; }

        COGroupCustomerSupplier groupCustomer;
        public COGroupCustomerSupplier GroupCustomerInfo
        {
            get
            {
                if (groupCustomer == null)
                    groupCustomer = new COGroupCustomerSupplier();
                return groupCustomer;
            }
            set
            {
                groupCustomer = value;
                this.Form.DataContext = value;
            }
        }


        public event EventHandler<SubmitEvent> SubmitSucceed;
        private void Submitbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WCFService.ServicesClient.ClientService client = new WCFService.ServicesClient.ClientService();
                groupCustomer.Discriminator = Discriminator.ToString();
                groupCustomer = client.SaveGroupCustomerSupplier(groupCustomer);
                if (Mode == ModeForm.Add)
                {
                    
                    if (SubmitSucceed != null)
                        SubmitSucceed(this, new SubmitEvent(groupCustomer));
                    GroupCustomerInfo = new COGroupCustomerSupplier();
                }
            }
            catch (Exception ex)
            {
                if (ex is InvalidEntityException)
                    ModernDialog.Show(Global.Instance.GetLangByKey(ex.Message));
                else
                    ModernDialog.Show(ex);
            }
        }


        public event EventHandler CancelForm;
        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            if (CancelForm != null)
                CancelForm(this, EventArgs.Empty);
        }
    }
}
