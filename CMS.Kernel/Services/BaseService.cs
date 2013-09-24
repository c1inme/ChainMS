using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Kernel;
using System.Data.Entity;

namespace CMS.Kernel
{
    public abstract class BaseService
    {

        public BaseService(DbContextRepository ct)
        {
            _db = ct;
        }

        DbContextRepository _db;
        protected DbContextRepository db
        {
             get
            {
                return _db;
            }
           
        }
     
    }
}
