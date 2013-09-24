using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using CMS.Entities.ServerObjects;
using CMS.Kernel;

namespace CMS.BussinessLayer.Servers
{

    public static class WebSecurity
    {
        private static  DbContextRepository instance;
        public static DbContextRepository db
        {
            get
            {
                if (instance == null)
                    instance = new DbContextRepository(new  DBServerContext());
                return instance;
            }
        }


        public static HttpContextBase Context
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }

        public static HttpRequestBase Request
        {
            get { return Context.Request; }
        }

        public static HttpResponseBase Response
        {
            get { return Context.Response; }
        }

        public static System.Security.Principal.IPrincipal User
        {
            get { return Context.User; }
        }

        public static bool IsAuthenticated
        {
            get { return User.Identity.IsAuthenticated; }
        }

        public static System.Web.Security.MembershipCreateStatus Register(string Username, string Password, string Email, bool IsApproved, string FirstName, string LastName)
        {
            MembershipCreateStatus CreateStatus = default(MembershipCreateStatus);
            Membership.CreateUser(Username, Password, Email, null, null, IsApproved, null,out CreateStatus);

            if (CreateStatus == MembershipCreateStatus.Success)
            {
               
                    dynamic User = db.Find<Users>(Usr => Usr.Alias== Username);
                    User.FirstName = FirstName;
                    User.LastName = LastName;
                    db.SaveChanges(User);

                if (IsApproved)
                {
                    FormsAuthentication.SetAuthCookie(Username, false);
                }
            }

            return CreateStatus;
        }

        public enum MembershipLoginStatus
        {
            Success,
            Failure
        }

        public static MembershipLoginStatus Login(string Username, string Password, bool RememberMe)
        {
            if (Membership.ValidateUser(Username, Password))
            {
                FormsAuthentication.SetAuthCookie(Username, RememberMe);
                return MembershipLoginStatus.Success;
            }
            else
            {
                return MembershipLoginStatus.Failure;
            }
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public static MembershipUser GetUser(string Username)
        {
            return Membership.GetUser(Username);
        }

        public static bool ChangePassword(string OldPassword, string NewPassword)
        {
            dynamic CurrentUser = Membership.GetUser(User.Identity.Name);
            return CurrentUser.ChangePassword(OldPassword, NewPassword);
        }

        public static bool DeleteUser(string Username)
        {
            return Membership.DeleteUser(Username);
        }

        public static List<MembershipUser> FindUsersByEmail(string Email, int PageIndex, int PageSize)
        {
            int row ;
            return Membership.FindUsersByEmail(Email, PageIndex, PageSize,out row).Cast<MembershipUser>().ToList();
        }

        public static List<MembershipUser> FindUsersByName(string Username, int PageIndex, int PageSize)
        {
            int row;
            return Membership.FindUsersByName(Username, PageIndex, PageSize, out row).Cast<MembershipUser>().ToList();
        }

        public static List<MembershipUser> GetAllUsers(int PageIndex, int PageSize)
        {
            int row;
            return Membership.GetAllUsers(PageIndex, PageSize, out row).Cast<MembershipUser>().ToList();
        }

    }
}
