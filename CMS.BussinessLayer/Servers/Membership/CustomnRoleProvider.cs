using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using CMS.Entities.ServerObjects;
using CMS.Kernel;

namespace CMS.BussinessLayer.Servers
{
    public class CustomnRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get;
            set;
        }

        #region OVerride Function
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    dynamic Users = db.Find<Users>(Usr => usernames.Contains(Usr.Alias)).ToList();
                    dynamic Roles = db.Find<PermissionDefinition>(Rl => roleNames.Contains(Rl.CodePermision)).ToList();
                    foreach (Users User in Users)
                    {
                        foreach (PermissionDefinition Role in Roles)
                        {
                            if (!User.ListPermissionDefinition.Contains(Role))
                            {
                                User.ListPermissionDefinition.Add(Role);
                            }
                        }
                    }
                    unitOfWork.Comit();
                }
            }
        }



        public override void CreateRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    using (var db = unitOfWork.GetDbContextRepository())
                    {
                        PermissionDefinition Role = null;
                        Role = db.FindFirst<PermissionDefinition>(Rl => Rl.CodePermision == roleName);
                        if (Role == null)
                        {
                            PermissionDefinition NewRole = new PermissionDefinition
                            {
                                GuidId = Guid.NewGuid(),
                                CodePermision = roleName,
                                NamePermission = roleName
                            };
                            db.SaveEntity(NewRole);
                            unitOfWork.Comit();
                        }
                    }
                }
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return false;
            }
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    PermissionDefinition Role = null;
                    Role = db.FindFirst<PermissionDefinition>(Rl => Rl.CodePermision == roleName);
                    if (Role == null)
                    {
                        return false;
                    }
                    if (throwOnPopulatedRole)
                    {
                        if (Role.ListUsers.Any())
                        {
                            return false;
                        }
                    }
                    else
                    {
                        Role.ListUsers.Clear();
                    }
                    db.Delete(Role, false);
                    unitOfWork.Comit();
                    return true;
                }
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return null;
            }
            if (string.IsNullOrEmpty(usernameToMatch))
            {
                return null;
            }
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    return (from Rl in db.Find<PermissionDefinition>() from Usr in Rl.ListUsers where Rl.CodePermision == roleName && Usr.Alias.Contains(usernameToMatch) select Usr.Alias).ToArray();
                }
            }
        }

        public override string[] GetAllRoles()
        {
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    return db.Select<PermissionDefinition>().Select(f => f.CodePermision).ToArray(); ;
                }
            }
        }

        public override string[] GetRolesForUser(string username)
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
                        return User.ListPermissionDefinition.Select(Rl => Rl.CodePermision).ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return null;
            }
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    PermissionDefinition pd = null;
                    pd = db.FindFirst<PermissionDefinition>(Rl => Rl.CodePermision == roleName);
                    if (pd != null)
                    {
                        return pd.ListUsers.Select(Usr => Usr.Alias).ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            if (string.IsNullOrEmpty(roleName))
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
                    dynamic Role = db.FindFirst<PermissionDefinition>(Rl => Rl.CodePermision == roleName);
                    if (Role == null)
                    {
                        return false;
                    }
                    return User.ListPermissionDefinition.Contains(Role);
                }
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    foreach (string Username in usernames)
                    {
                        var Us = Username;
                        var User = db.FindFirst<Users>(U => U.Alias == Us);
                        if (User != null)
                        {
                            foreach (string RoleName in roleNames)
                            {
                                dynamic Rl = RoleName;
                                dynamic Role = User.ListPermissionDefinition.FirstOrDefault(R => R.CodePermision == Rl);
                                if (Role != null)
                                {
                                    User.ListPermissionDefinition.Remove(Role);
                                }
                            }
                        }
                    }
                    db.SaveChanges();
                    unitOfWork.Comit();
                }
            }
        }

        public override bool RoleExists(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return false;
            }
            using (var unitOfWork = new UnitOfWork<DBServerContext>())
            {
                using (var db = unitOfWork.GetDbContextRepository())
                {
                    PermissionDefinition pd = null;
                    pd = db.FindFirst<PermissionDefinition>(Rl => Rl.CodePermision == roleName);
                    if (pd != null)
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
        #endregion
    }
}
