using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary.AssemblyDefinitions;
using HelperLibrary.TraceChange;

namespace HelperLibrary
{
    public class TraceChanges : EntityInfo
    {

        static Guid guiIDEmty = Guid.Empty;

        /// <summary>
        /// Detect changes of Entity
        /// </summary>
        /// <typeparam name="TOld">Old entity</typeparam>
        /// <typeparam name="TNew">New entity</typeparam>
        /// <param name="OldEntity"></param>
        /// <param name="NewEntity"></param>
        /// <returns>Return all changes of new entity</returns>
        public static InfoChangeCollection GetChange<TOld, TNew>(TOld OldEntity, TNew NewEntity, Guid guidID)
        {
            InfoChangeCollection changes = new InfoChangeCollection();
            PropertyDefinitionCollection oldProperties = GetPropertiesInType(OldEntity.GetType());
            PropertyDefinitionCollection newProperties = GetPropertiesInType(NewEntity.GetType());

            int version = GetVersion(OldEntity);

            foreach (PropertyDefinition oldItem in oldProperties)
            {
                if (oldItem.NameProperty == "VersionNumber" || newProperties.FirstOrDefault(f => f.NameProperty == oldItem.NameProperty) == null)
                    continue;//For case name property not match
                if (IsCollection(oldItem.ValueProperty))
                {
                    continue;
                }
                if (!IsFundamental(oldItem.TypeProperty))
                {
                    continue;
                }
                foreach (PropertyDefinition newItem in newProperties)
                {
                    if (oldItem.NameProperty == newItem.NameProperty)
                    {
                        var oldValue = GetValueProperty(OldEntity, oldItem.NameProperty);
                        var newValue = GetValueProperty(NewEntity, oldItem.NameProperty);
                        if (oldValue == null && newValue == null)
                        {
                            newProperties.Remove(newItem);
                            break;
                        }
                        if ((oldValue == null && newValue != null) || (oldValue != null && newValue == null) || !oldValue.Equals(newValue))
                        {
                            var infoChange = new InfoChange(OldEntity.GetType().Name, oldItem.NameProperty, oldValue == null ? null : oldValue.ToString(), newValue == null ? null : newValue.ToString(), version, guidID);
                            changes.Add(infoChange);
                        }
                        newProperties.Remove(newItem);
                        break;
                    }
                }
            }
            return changes;
        }

        private static int GetVersion<Tentity>(Tentity entityToGet, PropertyDefinitionCollection propertyCollection = null)
        {
            if (propertyCollection == null)
                propertyCollection = GetPropertiesInType(entityToGet.GetType());
            foreach (PropertyDefinition it in propertyCollection)
            {
                if (it.NameProperty == "VersionNumber")
                {
                    var value = GetValueProperty(entityToGet, it.NameProperty);
                    return Convert.ToInt32(value);
                }
            }
            return -1;
        }
    }
}
