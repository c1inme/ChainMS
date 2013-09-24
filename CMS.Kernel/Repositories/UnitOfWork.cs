using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Kernel
{
    public class UnitOfWork<C> : IUnitOfWork, IDisposable where C : DbContext
    {
        private DbTransaction _transaction;
        private Dictionary<Type, object> _repositories;
        private C _ctx;
        private bool _isStartBySession;
        private DbContextRepository _dbContext;


        public UnitOfWork(bool isBeginTransaction = true, bool isStartBySession = false)
        {
            _ctx = Activator.CreateInstance<C>();
            //_ctx = ContextManager.Instance.GetContext<C>();
            _dbContext = new DbContextRepository(_ctx);
            _repositories = new Dictionary<Type, object>();
            BeginTransaction(isBeginTransaction);
            _isStartBySession = isStartBySession;
        }

        public DbContextRepository GetDbContextRepository()
        {
            return _dbContext;
        }



        public TService GetService<TService>() where TService : BaseService
        {
            if (_repositories.Keys.Contains(typeof(TService)))
                return _repositories[typeof(TService)] as TService;
            var service = Activator.CreateInstance(typeof(TService), _dbContext);
            //service.dbSet = _dbContext;
            _repositories.Add(typeof(TService), service);
            return service as TService;
        }

        /// <summary>
        /// Start Transaction
        /// </summary>
        /// <returns></returns>
        internal DbTransaction BeginTransaction(bool isBeginTransaction = true)
        {
            if (null == _transaction)
            {
                IObjectContextAdapter adapter = _ctx;

                var objectContext = adapter.ObjectContext;
                if ((objectContext.Connection.State & ConnectionState.Open) != ConnectionState.Open)
                {
                    objectContext.Connection.Open();
                }
                if (isBeginTransaction)
                    this._transaction = objectContext.Connection.BeginTransaction();
            }
            return _transaction;
        }


        /// <summary>
        /// Commit current transaction
        /// </summary>
        public void Comit()
        {
            if (_transaction == null)
                return;
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                if (!_isStartBySession)
                    _transaction.Rollback();
                //if _isStartBySession == true. Rollback will be call by session.
                throw ex;
            }
        }


        public void RollBack()
        {
            if (_transaction == null)
                return;
            try
            {
                _transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region IDisposable Members
        public void Dispose()
        {
            if (_isStartBySession)
                return;
            //Comit();
            if (null != _transaction)
            {
                _transaction.Dispose();
            }

            if (null != _ctx)
            {
                _ctx.Dispose();
            }
        }
        #endregion

    }
}
