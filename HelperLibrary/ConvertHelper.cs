using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using HelperLibrary.AssemblyDefinitions;

namespace HelperLibrary
{
    public class ConvertHelper : EntityInfo
    {
        


       

        /// <summary>
        /// Clone a entity
        /// </summary>
        /// <typeparam name="Tsource">entity source type</typeparam>
        /// <typeparam name="Ttarget">type expected</typeparam>
        /// <param name="entiySource">entity source</param>
        /// <returns>Entity after clone done</returns>
        public static Ttarget ConvertEntity<Tsource, Ttarget>(Tsource entiySource)
        {
            var result = Activator.CreateInstance<Ttarget>();
            SetValueStandar(entiySource, result);
            return result;
        }


        /// <summary>
        /// Convert entity from Tsource to Ttarget
        /// </summary>
        /// <typeparam name="Tsource">Type of source</typeparam>
        /// <typeparam name="Ttarget">Type of target</typeparam>
        /// <param name="entiySource">source entity</param>
        /// <param name="entityTarget">target entity expected</param>
        /// <param name="forceMatchNameType">check name before copy</param>
        public static void ConvertEntity<Tsource, Ttarget>(Tsource entiySource, Ttarget entityTarget, bool forceMatchNameType = false)
        {
            if (entiySource == null || entityTarget == null)
                throw new Exception("Both Entity can not null");
            if (forceMatchNameType)
                if (entiySource.GetType().Name != entityTarget.GetType().Name)
                    throw new Exception("Tsource and Ttarget not match name type");
            SetValueStandar(entiySource, entityTarget);
        }


       
        private static void SetValueStandar<Tsource, Ttarget>(Tsource entiySource, Ttarget entityTarget)
        {
            PropertyDefinitionCollection sourceProperty = GetPropertiesInType(entiySource.GetType());
            PropertyDefinitionCollection targetProperty = GetPropertiesInType(entityTarget.GetType());
            foreach (PropertyDefinition sourceItem in sourceProperty)
            {
                if (targetProperty.FirstOrDefault(f => f.NameProperty == sourceItem.NameProperty) == null)
                    continue;//For case name property not match
                if (IsCollection(sourceItem.ValueProperty))
                {
                    Type sourceType = sourceItem.TypeProperty.GetGenericArguments()[0];
                    if (IsFundamental(sourceType))
                        return;
                    var value = GetValueProperty(entiySource, sourceItem.NameProperty);
                    var SourceValueCollection = (IEnumerable)value;
                    Type targetTypeItem = targetProperty.FirstOrDefault(f => f.NameProperty == sourceItem.NameProperty).TypeProperty.GetGenericArguments()[0];
                    Type targetTypeColletion = targetProperty.FirstOrDefault(f => f.NameProperty == sourceItem.NameProperty).TypeProperty;
                    var valueTarget = GetValueProperty(entityTarget, sourceItem.NameProperty);
                    ClearCollection(targetTypeColletion, valueTarget);
                    CreateCollection(valueTarget);
                    foreach (object sourceitem in SourceValueCollection)
                    {
                        var targetItem = Activator.CreateInstance(targetTypeItem);
                        ConvertEntity(sourceitem, targetItem);
                        AddHashSet(targetItem, targetTypeColletion, valueTarget);
                    }
                    continue;
                }
                if (!IsFundamental(sourceItem.TypeProperty))
                {
                    var valueSource = GetValueProperty(entiySource, sourceItem.NameProperty);
                    var valueTarget = GetValueProperty(entityTarget, sourceItem.NameProperty);
                    if (valueSource == null || valueTarget == null)
                        continue;
                    ConvertEntity(valueSource, valueTarget);
                    continue;
                }
                foreach (PropertyDefinition targetItem in targetProperty)
                {
                    if (sourceItem.NameProperty == targetItem.NameProperty)
                    {
                        var value = GetValueProperty(entiySource, sourceItem.NameProperty);
                        SetValueProperty(entityTarget, targetItem.NameProperty, value);
                        targetProperty.Remove(targetItem);
                        break;
                    }
                }
            }
        }

       
    }
}
