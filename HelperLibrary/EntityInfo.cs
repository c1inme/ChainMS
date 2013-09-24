using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary.AssemblyDefinitions;

namespace HelperLibrary
{
    public class EntityInfo
    { /// <summary>
        /// Get all class information in a namespace
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="nameSpace"></param>
        /// <returns>EntityDefinitionCollection Collection</returns>
        protected static EntityDefinitionCollection GetEntitiesInfo(Assembly assembly, string nameSpace)
        {
            EntityDefinitionCollection result = new EntityDefinitionCollection(nameSpace);
            var enities = GetTypesInNamespace(assembly, nameSpace);
            if (enities == null || enities.Count() == 0)
                throw new Exception("Can not found any class in nameSpace " + nameSpace);
            foreach (Type it in enities)
            {
                var entityProperties = GetPropertiesInType(it);
                result.Add(new EntityDefinition(it.Name, entityProperties, it));
            }
            return result;
        }


        /// <summary>
        /// Get value specify property by name in class
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="Entity"></param>
        /// <param name="nameProperty"></param>
        /// <returns>value of property</returns>
        protected static object GetValueProperty<E>(E Entity, string nameProperty)
        {
            object result = new object();
            result = Entity.GetType().GetProperty(nameProperty).GetValue(Entity, null);
            return result;
        }


        /// <summary>
        /// Set value for specify property
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="Entity"></param>
        /// <param name="nameProperty"></param>
        /// <param name="value"></param>
        protected static void SetValueProperty<E>(E Entity, string nameProperty, object value)
        {
            Entity.GetType().GetProperty(nameProperty).SetValue(Entity, value, null);
        }


        /// <summary>
        /// Get all class in name space (dll)
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        protected static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }


        /// <summary>
        /// Get all properties in class
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected static PropertyDefinitionCollection GetPropertiesInType(Type entity)
        {
            if (entity == null)
                throw new Exception("Cannot get properties for entity Null");
            PropertyDefinitionCollection result = new PropertyDefinitionCollection(entity.Name);
            PropertyInfo[] properties = entity.GetProperties();
            if (properties == null)
                throw new Exception("Cannot get properties of Entity " + entity.Name);
            foreach (PropertyInfo prop in properties)
            {
                result.Add(new PropertyDefinition(prop.Name, prop.PropertyType, prop));
            }
            return result;
        }


        protected static void ClearCollection(Type typeCollection, object EntityValue)
        {
            MethodInfo methodInfo = typeCollection.GetMethod("Clear");
            methodInfo.Invoke(EntityValue, null);
        }

        protected static void AddHashSet<Tsource>(Tsource sourceItem, Type typeCollection, object EntityValue)
        {
            MethodInfo methodInfo = typeCollection.GetMethod("Add");
            object[] parametersArray = new object[] { sourceItem };
            methodInfo.Invoke(EntityValue, parametersArray);
        }

        protected static void CreateCollection(object T)
        {
            T = Activator.CreateInstance(T.GetType());
        }


        /// <summary>
        /// Check current type is collection or not ?
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected static bool IsCollection(PropertyInfo p)
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
        /// <summary>
        /// Detect type is fundamental type or not.
        /// </summary>
        /// <param name="type">Type need detect</param>
        /// <returns>result after detected</returns>
        protected static bool IsFundamental(Type type)
        {
            return type.IsPrimitive || type.Equals(typeof(string)) || type.Equals(typeof(DateTime)) || type.FullName.StartsWith("System");
        }

    }
}
