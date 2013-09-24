using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using CMS.Entities.ServerObjects;
using CMS.Kernel;
using System.Linq.Dynamic;
using System.Linq;
using System.Web.Security;
using System.Linq.Expressions;
namespace CMS.BussinessLayer.Servers
{
    public class CMSMembershipProvider : MembershipProvider
    {

        #region "Properties"
        //private DbContextRepository instance;
        //public DbContextRepository db
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = new DbContextRepository(new DBServerContext());
        //        return instance;
        //    }
        //}
        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 5; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 0; }
        }

        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return string.Empty; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        #endregion

        #region "Functions"


        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            if (string.IsNullOrEmpty(username))
            {
                status = MembershipCreateStatus.InvalidUserName;
                return null;
            }
            if (string.IsNullOrEmpty(password))
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            //if (string.IsNullOrEmpty(email))
            //{
            //    status = MembershipCreateStatus.InvalidEmail;
            //    return null;
            //}

            string HashedPassword = Crypto.HashPassword(password);
            if (HashedPassword.Length > 128)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    if (db.RowCount<Users>(Usr => Usr.Alias == username) > 0)
                    {
                        status = MembershipCreateStatus.DuplicateUserName;
                        return null;
                    }

                    if (db.RowCount<Users>(Usr => Usr.Email == email) > 0)
                    {
                        status = MembershipCreateStatus.DuplicateEmail;
                        return null;
                    }

                    Users NewUser = new Users
                    {
                        GuidId = providerUserKey == null ?  Guid.NewGuid() : (Guid)providerUserKey,
                        Alias = username,
                        FirstName = username.Split('@')[0],
                        Password = HashedPassword,
                        IsApproved = isApproved,
                        Email = username,
                        CreateDate = DateTime.UtcNow,
                        LastPasswordChangedDate = DateTime.UtcNow,
                        PasswordFailuresSinceLastSuccess = 0,
                        LastLoginDate = DateTime.UtcNow,
                        LastActivityDate = DateTime.UtcNow,
                        LastLockoutDate = DateTime.UtcNow,
                        IsLockedOut = false,
                        LastPasswordFailureDate = DateTime.UtcNow
                    };
                    db.SaveEntity(NewUser);

                    status = MembershipCreateStatus.Success;
                    
                    var result = new MembershipUser(Membership.Provider.Name, NewUser.Alias, NewUser.GuidId, NewUser.Email, null, null, NewUser.IsApproved, NewUser.IsLockedOut, NewUser.CreateDate, NewUser.LastLoginDate,
                    NewUser.LastActivityDate, NewUser.LastPasswordChangedDate, NewUser.LastLockoutDate);
                    unitOfWork.Comit();
                    return result;
                }
            }
        }

        public override bool ValidateUser(string username, string password)
        {

            
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    
                    Users User = null;
                    User = db.FindFirst<Users>(Usr => Usr.Alias == username);
                    if (User == null)
                    {
                        return false;
                    }
                    if (!User.IsApproved)
                    {
                        return false;
                    }
                    if (User.IsLockedOut)
                    {
                        return false;
                    }
                    dynamic HashedPassword = User.Password;
                    bool VerificationSucceeded = (HashedPassword != null && Crypto.VerifyHashedPassword(HashedPassword, password));
                    if (VerificationSucceeded)
                    {
                        User.PasswordFailuresSinceLastSuccess = 0;
                        User.LastLoginDate = DateTime.UtcNow;
                        User.LastActivityDate = DateTime.UtcNow;
                    }
                    else
                    {
                        int Failures = User.PasswordFailuresSinceLastSuccess;
                        if (Failures < MaxInvalidPasswordAttempts)
                        {
                            User.PasswordFailuresSinceLastSuccess += 1;
                            User.LastPasswordFailureDate = DateTime.UtcNow;
                        }
                        else if (Failures >= MaxInvalidPasswordAttempts)
                        {
                            User.LastPasswordFailureDate = DateTime.UtcNow;
                            User.LastLockoutDate = DateTime.UtcNow;
                            User.IsLockedOut = true;
                        }
                    }
                    db.SaveEntity(User,false,true);
                    unitOfWork.Comit();
                    //  Context.SaveChanges();
                    if (VerificationSucceeded)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    } 
                    
                }
            }
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    Users User = null;
                    User = db.FindFirst<Users>(Usr => Usr.Alias == username);
                    if (User != null)
                    {
                        if (userIsOnline)
                        {
                            User.LastActivityDate = DateTime.UtcNow;
                            db.Update(User);
                        }
                        
                        var result = new MembershipUser(Membership.Provider.Name, User.Alias, User.GuidId, User.Email, null, null, User.IsApproved, User.IsLockedOut, User.CreateDate, User.LastLoginDate,
                        User.LastActivityDate, User.LastPasswordChangedDate, User.LastLockoutDate);
                        unitOfWork.Comit();
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            if (!(providerUserKey is Guid))
            {
                return null;
            }
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {

                    Users User = null;
                    User = db.FindByKey<Users>(providerUserKey);
                    if (User != null)
                    {
                        if (userIsOnline)
                        {
                            User.LastActivityDate = DateTime.UtcNow;
                            db.Update(User);
                        }
                        
                        var result = new MembershipUser(Membership.Provider.Name, User.Alias, User.GuidId, User.Email, null, null, User.IsApproved, User.IsLockedOut, User.CreateDate, User.LastLoginDate,
                        User.LastActivityDate, User.LastPasswordChangedDate, User.LastLockoutDate);
                        unitOfWork.Comit();
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            if (string.IsNullOrEmpty(oldPassword))
            {
                return false;
            }
            if (string.IsNullOrEmpty(newPassword))
            {
                return false;
            }
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                  
                    Users User = null;
                    User = db.FindFirst<Users>(Usr => Usr.Alias == username);
                    if (User == null)
                    {
                        return false;
                    }
                    dynamic HashedPassword = User.Password;
                    bool VerificationSucceeded = (HashedPassword != null && Crypto.VerifyHashedPassword(HashedPassword, oldPassword));
                    if (VerificationSucceeded)
                    {
                        User.PasswordFailuresSinceLastSuccess = 0;
                    }
                    else
                    {
                        int Failures = User.PasswordFailuresSinceLastSuccess;
                        if (Failures < MaxInvalidPasswordAttempts)
                        {
                            User.PasswordFailuresSinceLastSuccess += 1;
                            User.LastPasswordFailureDate = DateTime.UtcNow;
                        }
                        else if (Failures >= MaxInvalidPasswordAttempts)
                        {
                            User.LastPasswordFailureDate = DateTime.UtcNow;
                            User.LastLockoutDate = DateTime.UtcNow;
                            User.IsLockedOut = true;
                        }
                        db.SaveEntity(User);
                        unitOfWork.Comit();
                        return false;
                    }
                    dynamic NewHashedPassword = Crypto.HashPassword(newPassword);
                    if (NewHashedPassword.Length > 128)
                    {
                        return false;
                    }
                    User.Password = NewHashedPassword;
                    User.LastPasswordChangedDate = DateTime.UtcNow;
                    db.SaveEntity(User);
                    unitOfWork.Comit();
                    return true;
                }
            }
        }

        public override bool UnlockUser(string userName)
        {
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    Users User = null;
                    User = db.FindFirst<Users>(Usr => Usr.Alias == userName);
                    if (User != null)
                    {
                        User.IsLockedOut = false;
                        User.PasswordFailuresSinceLastSuccess = 0;
                        db.SaveEntity(User);
                        unitOfWork.Comit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public override int GetNumberOfUsersOnline()
        {
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    DateTime DateActive = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(Convert.ToDouble(Membership.UserIsOnlineTimeWindow)));
                    return db.RowCount<Users>(Usr => Usr.LastActivityDate > DateActive);
                }
            }
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    if (string.IsNullOrEmpty(username))
                    {
                        return false;
                    }

                    Users User = null;
                    User = db.FindFirst<Users>(Usr => Usr.Alias == username);
                    if (User != null)
                    {
                        db.Delete(User);
                        unitOfWork.Comit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    Users User = null;
                    User = db.FindFirst<Users>(Usr => Usr.Email == email);
                    if (User != null)
                    {
                        return User.Alias;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection MembershipUsers = new MembershipUserCollection();
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    totalRecords = db.RowCount<Users>(Usr => Usr.Email == emailToMatch);
                    dynamic users = db.Find<Users>(Usr => Usr.Alias == emailToMatch, StaticMethods.CreateOrderBy<Users>(f => f.Email, f => f.Alias), pageIndex * pageSize, pageSize).ToList();
                    foreach (Users User_loopVariable in users)
                    {
                        var User = User_loopVariable;
                        MembershipUsers.Add(new MembershipUser(Membership.Provider.Name, User.Alias, User.GuidId, User.Email, null, null, User.IsApproved, User.IsLockedOut, User.CreateDate, User.LastLoginDate,
                        User.LastActivityDate, User.LastPasswordChangedDate, User.LastLockoutDate));
                    }
                    return MembershipUsers;
                }
            }
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection MembershipUsers = new MembershipUserCollection();
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    totalRecords = db.RowCount<Users>(Usr => Usr.Alias == usernameToMatch);
                    dynamic Users = db.Find<Users>(Usr => Usr.Alias == usernameToMatch, StaticMethods.CreateOrderBy<Users>(f => f.Alias, f => f.Email), pageIndex * pageSize, pageSize);
                    foreach (Users User_loopVariable in Users)
                    {
                        var User = User_loopVariable;
                        MembershipUsers.Add(new MembershipUser(Membership.Provider.Name, User.Alias, User.GuidId, User.Email, null, null, User.IsApproved, User.IsLockedOut, User.CreateDate, User.LastLoginDate,
                          User.LastActivityDate, User.LastPasswordChangedDate, User.LastLockoutDate));
                    }

                    return MembershipUsers;
                }
            }
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection MembershipUsers = new MembershipUserCollection();
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    totalRecords = db.RowCount<Users>();
                    dynamic Users = db.Find<Users>(StaticMethods.CreateOrderBy<Users>(f => f.Alias, f => f.Email), pageIndex * pageSize, pageSize);
                    foreach (Users User_loopVariable in Users)
                    {
                        var User = User_loopVariable;
                        MembershipUsers.Add(new MembershipUser(Membership.Provider.Name, User.Alias, User.GuidId, User.Email, null, null, User.IsApproved, User.IsLockedOut, User.CreateDate, User.LastLoginDate,
                        User.LastActivityDate, User.LastPasswordChangedDate, User.LastLockoutDate));
                    }
                    return MembershipUsers;
                }
            }
        }

        #endregion

        #region "Not Supported"

        //CodeFirstMembershipProvider does not support password retrieval scenarios.
        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }
        public override string GetPassword(string username, string answer)
        {
            return "";
            //throw new NotSupportedException("Consider using methods from WebSecurity module.");
        }

        //CodeFirstMembershipProvider does not support password reset scenarios.
        public override bool EnablePasswordReset
        {
            get { return false; }
        }
        public override string ResetPassword(string username, string answer)
        {
            return "";
            //throw new NotSupportedException("Consider using methods from WebSecurity module.");
        }

        //CodeFirstMembershipProvider does not support question and answer scenarios.
        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return false;
            //throw new NotSupportedException("Consider using methods from WebSecurity module.");
        }

        //CodeFirstMembershipProvider does not support UpdateUser because this method is useless.
        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            return;
            //    throw new NotSupportedException();
        }

        #endregion

    }
}