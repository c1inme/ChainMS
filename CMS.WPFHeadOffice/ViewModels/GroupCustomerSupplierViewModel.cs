using CMS.Entities.ClientObjects;
using CMS.WCFService.ServicesClient;
using CMS.WPFHeadOffice.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.WPFHeadOffice
{
    public class GroupCustomerSupplierViewModel : BaseEntityViewModel
    {
        public GroupCustomerSupplierViewModel()
            : base()
        {

        }

        public GroupCustomerSupplierViewModel(GroupCustomerSupplier us)
            : base(us)
        {
            GroupEntity = us;
            LazyInitializer.EnsureInitialized<ClientService>(ref serviceClient);
            //UpdateUser = new RelayCommand(o => { this.AddUser(); }, c => { return this.CanAddUser(); });
            //DeleteUser = new RelayCommand((o) => { this.DeleteUserExecute(); }, (c) => { return CanDeletedUser(); });
        }

        GroupCustomerSupplier GroupEntity;
        ClientService serviceClient;
        public string CodeGroup
        {
            get { return GroupEntity.CodeGroup; }
            set { GroupEntity.CodeGroup = value; this.OnPropertyChanged("CodeGroup"); }
        }
        public string NameGroup
        {
            get { return GroupEntity.NameGroup; }
            set { GroupEntity.NameGroup = value; this.OnPropertyChanged("NameGroup"); }
        }
        public string Description
        {
            get { return GroupEntity.Description; }
            set { GroupEntity.Description = value; this.OnPropertyChanged("Description"); }
        }
        public Nullable<System.Guid> IDBelong
        {
            get { return GroupEntity.IDBelong; }
            set { GroupEntity.IDBelong = value; this.OnPropertyChanged("IDBelong"); }
        }
        public string Discriminator
        {
            get { return GroupEntity.Discriminator; }
            set
            {
                GroupEntity.Discriminator = value;
                this.OnPropertyChanged("Discriminator");
            }
        }

        public GroupCustomerSupplier Parent
        {
            get;
            set;    
        }
      
      
    }
}
