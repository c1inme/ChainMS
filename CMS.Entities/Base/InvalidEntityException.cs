using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Entities
{
    public class InvalidEntityException : Exception
    {
        public string InvalidEntity { get; set; }
        public InvalidEntityException(string messeage)
            : base(messeage)
        {
            InvalidEntity = messeage;
        }
    }
}
