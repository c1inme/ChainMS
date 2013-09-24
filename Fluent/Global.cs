using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluent
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
        public Dictionary<string, string> LanguageDictionary
        {
            get;
            set;
        }
    }
}
