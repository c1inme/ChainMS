using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using HelperLibrary.AssemblyDefinitions;

namespace CMS.WPFHeadOffice
{
    public class Global
    {
        static Global instance = null;
        static readonly object padlock = new object();
        Global()
        {
        }
        public static Global Instance
        {
            get
            {
                //if (!System.ComponentModel.DesignerProperties.IsInDesignModeProperty.)
                //{
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Global();
                    }
                    return instance;
                }
                //}
            }
        }

        public string GetLangByKey(string key)
        {
            if (this.LanguageDictionary != null && this.LanguageDictionary.ContainsKey(key))
                return this.LanguageDictionary[key];
            return key;
        }

        private Dictionary<string, string> languageDictionary;
        public Dictionary<string, string> LanguageDictionary
        {
            get { return languageDictionary; }
            set
            {
                languageDictionary = value;
                CMS.Entities.Global.Instance.LanguageDictionary = value;
            }
        }

        public Dictionary<string, EntityDefinitionCollection> NameSpaceInfo
        {
            get;
            set;
        }

        //public Users CurrentUser
        //{
        //    get;
        //    set;
        //}
    }
}