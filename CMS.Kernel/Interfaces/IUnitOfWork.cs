using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Kernel
{
    public interface IUnitOfWork
    {
        TRepository GetService<TRepository>() where TRepository : BaseService;
        //DbTransaction BeginTransaction();
        void Comit();
        void RollBack();
        //int Save();
        void Dispose();

    }
}
