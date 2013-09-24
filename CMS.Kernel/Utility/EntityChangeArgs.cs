using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Kernel
{
    public class EntityChangeArgs : EventArgs
    {
        public string NameOfEntity{get;set;}
        public DateTime DateChange { get; set; }
        public object ObjectChange { get; set; }
        public EntityChangeArgs(string nameEntity, DateTime dateChange, object objectChange)
        {
            NameOfEntity = nameEntity;
            DateChange = dateChange;
            ObjectChange = objectChange;
        }
    }
}
