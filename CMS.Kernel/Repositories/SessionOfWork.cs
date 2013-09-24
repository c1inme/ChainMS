using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Kernel
{
    public class SessionOfWork : IDisposable
    {
        private Dictionary<Type, IUnitOfWork> _unitOfWorks;
        public SessionOfWork()
        {
            _unitOfWorks = new Dictionary<Type, IUnitOfWork>();
        }

        public UnitOfWork<C> GetUnitOfWork<C>(bool isStartransaction = true)
            where C : DbContext
        {
            if (_unitOfWorks.Keys.Contains(typeof(UnitOfWork<C>)))
                return _unitOfWorks[typeof(UnitOfWork<C>)] as UnitOfWork<C>;
            var unitOfWork = new UnitOfWork<C>(isStartransaction, true);
            _unitOfWorks.Add(typeof(UnitOfWork<C>), unitOfWork);
            return unitOfWork;
        }

        public void Commit()
        {
            if (_unitOfWorks == null || _unitOfWorks.Count < 1)
                return;
            try
            {
                foreach (var unitWork in _unitOfWorks)
                {
                    unitWork.Value.Comit();
                }
            }
            catch (Exception ex)
            {
                RollBack();
                throw ex;
            }
        }

        public void RollBack()
        {
            if (_unitOfWorks == null || _unitOfWorks.Count < 1)
                return;
            try
            {
                foreach (var unitWork in _unitOfWorks)
                {
                    unitWork.Value.RollBack();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (_unitOfWorks == null || _unitOfWorks.Count == 0)
                return;
            // Commit();
            _unitOfWorks.Clear();
            _unitOfWorks = null;
        }
    }
}
