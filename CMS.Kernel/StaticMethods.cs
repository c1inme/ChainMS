using System;
using System.Data.Linq.Mapping;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace CMS.Kernel
{
    public static class StaticMethods
    {
        #region Methods

        /// <summary>   Gets a primary key value. </summary>
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="entity">   The entity. </param>
        /// <returns>   The primary key value. </returns>
        public static object GetPrimaryKeyValue<T>(T entity)
        {
            PropertyInfo keyInfo = StaticMethods.GetPrimaryKeyInfo<T>();

            if (keyInfo != null)
                return keyInfo.GetValue(entity, null);

            return null;
        }

        /// <summary> Retrieve the primary key for the passed <c>EntityObject</c> descendant </summary>
        /// <typeparam name="T">Strongly-typed <c>EntityObject</c></typeparam>
        /// <returns>The property info for the primary key of the <c>EntityObject</c></returns>
        public static string GetPrimaryKeyName<T>()
        {
            string keyName = string.Empty;

            PropertyInfo key = StaticMethods.GetPrimaryKeyInfo<T>();

            if (key != null)
                keyName = key.Name;

            return keyName;
        }

        /// <summary>   Gets the primary key information. </summary>
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <returns>   The primary key info. </returns>
        public static PropertyInfo GetPrimaryKeyInfo<T>()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo propertyInfo in properties)
            {
                object[] attributes = propertyInfo.GetCustomAttributes(true);

                foreach (object attribute in attributes)
                {
                    if (attribute is EdmScalarPropertyAttribute)
                    {
                        if ((attribute as EdmScalarPropertyAttribute).EntityKeyProperty == true)
                            return propertyInfo;
                    }
                    else if (attribute is ColumnAttribute)
                    {
                        if ((attribute as ColumnAttribute).IsPrimaryKey == true)
                            return propertyInfo;
                    }
                }
            }

            // If we cannot determine the primary key, we return the first property.
            if (properties.Length > 0)
                return properties[0];

            return null;
        }

        /// <summary> Create the sort expression for the entity </summary>
        /// <typeparam name="T">Entity type on which to sort</typeparam>
        /// <param name="sortExpression">Expression on which to sort. Empty or null defaults to entity key.</param>
        /// <param name="sortDirection">Either null, blank, ASC, or DESC</param>
        /// <param name="defaultKey">Optional value to use as the default sort key.
        /// Only used if sortExpression is empty or null, not required if the object type used
        /// is an entity with a primary key property.</param>
        /// <returns>The (hopefully) valid sort expression</returns>
        public static string CreateSortExpression<T>(string sortExpression, string sortDirection = null, string defaultKey = null)
        {
            if (string.IsNullOrEmpty(sortExpression))
            {
                if (string.IsNullOrEmpty(defaultKey))
                {
                    // Default the sort expression to the primary key
                    sortExpression = GetPrimaryKeyName<T>();
                }
                else
                    sortExpression = defaultKey;
            }

            return string.Format("it.{0} {1}", sortExpression, sortDirection);
        }



        #endregion


        public static Expression<Func<E, object>>[] CreateInclude<E>(params  Expression<Func<E, object>>[] includes)
        {
            return includes;
        }

        public static Expression<Func<E, object>>[] CreateOrderBy<E>(params  Expression<Func<E, object>>[] orderBy)
        {
            return orderBy;
        }


        public static Guid? GuidCurrentUser(bool isOnline = true)
        {
            try
            {

                var user = Membership.GetUser(true);
                if (user == null)
                {
                    return null;
                }
                return (Guid)user.ProviderUserKey;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public static Guid? GuidCurrentUser(string alias, bool isOnline = true)
        {
            MembershipUser user = Membership.GetUser(alias, isOnline);
            if (user == null)
            {
                return null;
            }
            return (Guid)user.ProviderUserKey;
        }

        public static bool IsUserOnline()
        {
            var user = Membership.GetUser(true);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public static bool IsUserOnline(string alias)
        {
            var user = Membership.GetUser(alias, true);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}