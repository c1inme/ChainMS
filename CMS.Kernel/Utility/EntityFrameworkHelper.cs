using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Kernel.Utility
{
    #region - DataObjectExtension -

    /// <summary>
    /// Extension methods to handle extra functionality on data object and entities.
    /// </summary>
    public static class DataObjectExtension
    {
        #region - Constants & static fields -

        private const string DEFAULT_INCLUDE_SEPERATOR = ".";

        #endregion

        #region - Nested class : EntityFrameworkHelper -

        internal static class EntityFrameworkHelper
        {
            internal static void CollectRelationalMembers(Expression exp, IList<PropertyInfo> members)
            {
                if (exp.NodeType == ExpressionType.Lambda)
                {
                    // At root, explore body:  
                    CollectRelationalMembers(((LambdaExpression)exp).Body, members);
                }
                else if (exp.NodeType == ExpressionType.MemberAccess)
                {
                    MemberExpression mexp = (MemberExpression)exp;
                    CollectRelationalMembers(mexp.Expression, members);
                    members.Add((PropertyInfo)mexp.Member);
                }
                else if (exp.NodeType == ExpressionType.Call)
                {
                    MethodCallExpression cexp = (MethodCallExpression)exp;

                    if (cexp.Method.IsStatic == false)
                        throw new InvalidOperationException("Invalid type of expression.");

                    foreach (var arg in cexp.Arguments)
                        CollectRelationalMembers(arg, members);
                }
                else if (exp.NodeType == ExpressionType.Parameter)
                {
                    // Reached the toplevel:  
                    return;
                }
                else
                {
                    throw new InvalidOperationException("Invalid type of expression.");
                }
            }

            /// <summary>
            /// Check current type is collection or not ?
            /// </summary>
            /// <param name="p"></param>
            /// <returns></returns>
            internal static bool IsCollection(PropertyInfo p)
            {
                try
                {
                    var t = p.PropertyType.GetGenericTypeDefinition();
                    return typeof(IEnumerable).IsAssignableFrom(t);
                }
                catch
                {
                    return false;
                }
            }
        }



        #endregion

        #region - Public static methods -

      
        /// <summary>
        /// Get the entity for a Entity Framework reference.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="reference">The reference.</param>
        /// <returns>The entity.</returns>
        public static T GetEntity<T>(this EntityReference<T> reference)
            where T : EntityObject
        {
            if (!reference.IsLoaded)
            {
                reference.Load();
            }
            return reference.Value;
        }

        /// <summary>
        /// Loads the set of entities via the Entity Framework.
        /// </summary>
        /// <typeparam name="T">The type of the entities.</typeparam>
        /// <param name="collection">The collection to load.</param>
        /// <returns>A collection loaded with the entities.</returns>
        public static EntityCollection<T> LoadEntities<T>(this EntityCollection<T> collection)
            where T : EntityObject
        {
            if (!collection.IsLoaded)
            {
                collection.Load();
            }
            return collection;
        }

        /// <summary>  
        /// Specifies the related objects to include in the query results using a lambda expression mentioning the path members.  
        /// </summary>  
        /// <returns>A new System.Data.Objects.ObjectQuery{T} with the defined query path.</returns>  
        public static ObjectQuery<T> IncludeEntities<T>(this ObjectQuery<T> query, Expression<Func<T, object>> path)
        {
            // Retrieve member path.
            List<PropertyInfo> members = new List<PropertyInfo>();
            EntityFrameworkHelper.CollectRelationalMembers(path, members);

            // Build string path.
            StringBuilder sb = new StringBuilder();
            string separator = string.Empty;

            members.ForEach(member =>
            {
                sb.Append(separator);
                sb.Append(member.Name);
                separator = DataObjectExtension.DEFAULT_INCLUDE_SEPERATOR;
            });

            // Apply INCLUDE.
            return query.Include(sb.ToString());
        }

        #endregion
    }

    #endregion
}
