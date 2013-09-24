using System.Windows;
using HelperLibrary;
using HelperLibrary.TraceChange;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using CMS.Entities.ClientObjects;
namespace CMS.WPFHeadOffice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            LanguageXml.Instance.LoadLanguage("vn");
            //var assembly = Assembly.LoadFrom("CMS.Entities.dll");
            //var nameSpaceInfoClient = Dll.GetEntitiesInfo(assembly, "CMS.Entities.Entities");
            #region Test function convert from server to client
            COPermissionDefinition permision = new COPermissionDefinition();
            permision.ActionType = "System";
            permision.CodePermision = "Per1";
            permision.NamePermission = "Allow create user";
            permision.ListCOGrantPermission = new List<COGrantPermission>();
            permision.ListCOGrantPermission.Add(new COGrantPermission() { GuidId = Guid.NewGuid() });
            permision.ListCOGrantPermission.Add(new COGrantPermission() { GuidId = Guid.NewGuid(), IDGranted = Guid.NewGuid(), Discriminator = "User" });
            COPermissionDefinition permision2 = new COPermissionDefinition();
            permision2.ListCOGrantPermission = new Collection<COGrantPermission>();
            ConvertHelper.ConvertEntity(permision, permision2);
            permision2.ListCOGrantPermission.Add(new COGrantPermission() { GuidId = Guid.NewGuid() });
            permision2.ActionType = "Modified";
            permision2.CreateBy = Guid.NewGuid();
            var changeCollection = TraceChanges.GetChange(permision, permision2,permision.GuidId);

            #endregion
        }
    }
}
