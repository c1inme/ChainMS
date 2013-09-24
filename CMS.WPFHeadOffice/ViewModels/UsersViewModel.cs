using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlLibrary;
using CMS.WCFService;
using System.Threading;
using System.Windows.Input;
using CMS.WCFService.ServicesClient;
using CMS.Entities.ClientObjects;
namespace CMS.WPFHeadOffice.ViewModels
{
    public class UsersViewModel : BaseEntityViewModel
    {
        public UsersViewModel()
            : base()
        {

        }
        ClientService serviceClient;
        ICommand DeleteUser;
        ICommand UpdateUser;
        public COUsers UserInfo { get; set; }
        public UsersViewModel(COUsers us)
            : base(us)
        {
            UserInfo = us;
            LazyInitializer.EnsureInitialized<ClientService>(ref serviceClient);
            UpdateUser = new RelayCommand(o => { this.AddUser(); }, c => { return this.CanAddUser(); });
            DeleteUser = new RelayCommand((o) => { this.DeleteUserExecute(); }, (c) => { return CanDeletedUser(); });
        }

        #region Implement command
        private void AddUser()
        {
            serviceClient.SaveUser(UserInfo);
        }

        private bool CanAddUser()
        {
            return string.IsNullOrWhiteSpace(Alias) && string.IsNullOrWhiteSpace(Password);
        }


        private void DeleteUserExecute()
        {
            serviceClient.DeletedUser(UserInfo.GuidId);
        }


        private bool CanDeletedUser()
        {
            return this.Deleted.HasValue && !this.Deleted.Value && this.Id > 0;
        }
        #endregion

        public string Alias
        {
            get { return UserInfo.Alias + UserInfo.FullName; }
            set
            {
                UserInfo.Alias = value;
                this.OnPropertyChanged("Alias");
            }
        }
        public string Password
        {
            get { return UserInfo.Password; }
            set
            {
                UserInfo.Password = value;
                this.OnPropertyChanged("Password");
            }
        }
        public string ShortName
        {
            get { return UserInfo.ShortName; }
            set
            {
                UserInfo.ShortName = value;
                this.OnPropertyChanged("ShortName");
            }
        }
        public string FullName
        {
            get { return UserInfo.FullName; }
            set
            {
                UserInfo.FullName = value;
                this.OnPropertyChanged("FullName");
            }
        }

        [StringLength(100, ErrorMessage = "BetweenLength", MinimumLength = 6)]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage = "EmailInValid")]
        public string EmailUser
        {
            get { return UserInfo.EmailUser; }
            set
            {
                UserInfo.EmailUser = value;
                this.OnPropertyChanged("EmailUser");
            }
        }
        public string MobileUser
        {
            get { return UserInfo.MobileUser; }
            set
            {
                UserInfo.MobileUser = value;
                this.OnPropertyChanged("MobileUser");
            }
        }
        public bool? IsAdmin
        {
            get { return UserInfo.IsAdmin; }
            set
            {
                UserInfo.IsAdmin = value;
                this.OnPropertyChanged("IsAdmin");
            }
        }
        public string ImagePath
        {
            get { return UserInfo.ImagePath; }
            set
            {
                UserInfo.ImagePath = value;
                this.OnPropertyChanged("ImagePath");
            }
        }


    }
}
