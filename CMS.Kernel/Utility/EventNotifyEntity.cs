using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Kernel
{
    public class EventNotifyEntity : EventArgs 
    {
        public object Entity
        {
            get;
            set;
        }


        public IEnumerable<object> Entities
        {
            get;
            set;    
        }

        public bool IsCancel
        {
            get;
            set;
        }

        public EventNotifyEntity()
        {

        }

        public EventNotifyEntity(object _entity)
        {
            Entity = _entity;
        }
        //public EventNotifyEntity(List<object> _entities)
        //{
        //    Entities = _entities;
        //}
    }
}
