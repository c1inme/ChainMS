using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
namespace CMS.Kernel
{
    public abstract class GenericEntityRepository<T>
        where T : class
    {
        #region Properties

        public BaseRepository Repository
        {
            get
            {
                return Activator.CreateInstance(BaseRepositoryType) as BaseRepository;
            }
        }

        public abstract Type BaseRepositoryType { get; }

        #endregion

        #region Methods

        public virtual void Delete(T entity)
        {
            ExecuteQuery(query =>
            {
                query.Delete<T>(entity);
                query.SaveChanges();
            });
        }

        internal virtual void ExecuteQuery(Action<IRepository> query)
        {
            if (query == null)
                throw new ArgumentException("The query argument can not be null.");

            using (var queryRepository = Repository)
            {
                query(queryRepository);
            }
        }

        public virtual void Insert(T entity)
        {
            ExecuteQuery(query =>
            {
                query.Insert<T>(entity);
                query.SaveChanges();
            });
        }

        public virtual IList<T> Select()
        {
            List<T> results = new List<T>();

            ExecuteQuery(query =>
            {
                results = query.Select<T>().ToList();
            });

            return results;
        }

        public virtual IList<T> Select(int rowNumber, int pageSize)
        {
            // There is a bug in the EF stack that does not allow you to call the Skip()
            // function without calling the OrderBy() function.
            string defaultSort = StaticMethods.CreateSortExpression<T>(string.Empty, string.Empty);

            List<T> results = null;

            ExecuteQuery(query =>
            {
                results = query.Select<T>().AsQueryable<T>().OrderBy(defaultSort).Skip(rowNumber).Take(pageSize).ToList();
            });

            return results;
        }

        public virtual IList<T> Select(string sortByColumnName, int rowNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(sortByColumnName))
                return Select(rowNumber, pageSize);

            List<T> results = null;

            ExecuteQuery(query =>
            {
                results = query.Select<T>().OrderBy(sortByColumnName, false).Skip(rowNumber).Take(pageSize).ToList();
            });

            return results;
        }

        public virtual IList<T> Select(Expression<Func<T, bool>> where)
        {
            if (where == null)
                return Select();

            List<T> results = new List<T>();

            ExecuteQuery(query =>
            {
                results = query.Select<T>().Where<T>(where).ToList();
            });

            return results;
        }

        public virtual IList<T> Select(Expression<Func<T, bool>> where, int rowNumber, int pageSize)
        {
            if (where == null)
                return Select(rowNumber, pageSize);

            // There is a bug in the EF stack that does not allow you to call the Skip()
            // function without calling the OrderBy() function.
            string defaultSort = StaticMethods.CreateSortExpression<T>(string.Empty, string.Empty);

            List<T> results = null;

            ExecuteQuery(query =>
            {
                results = query.Select<T>().Where<T>(where).AsQueryable<T>().OrderBy(defaultSort).Skip(rowNumber).Take(pageSize).ToList();
            });

            return results;
        }

        public virtual IList<T> Select(Expression<Func<T, bool>> where, string sortByColumnName, int rowNumber, int pageSize)
        {
            if (where == null)
                return Select(sortByColumnName, rowNumber, pageSize);

            if (string.IsNullOrEmpty(sortByColumnName))
                return Select(where, rowNumber, pageSize);

            List<T> results = null;

            ExecuteQuery(query =>
            {
                results = query.Select<T>().Where<T>(where).AsQueryable<T>().OrderBy(sortByColumnName, false).Skip(rowNumber).Take(pageSize).ToList();
            });

            return results;
        }

        //public virtual T Select(object key)
        //{
        //    if (key == null)
        //        throw new ArgumentNullException("key", "The key argument can not be null.");

        //    T entity = null;

        //    ExecuteQuery(query =>
        //    {
        //        entity = query.Select<T>(key);
        //    });

        //    return entity;
        //}

        public virtual int RowCount()
        {
            int rowCount = -1;

            ExecuteQuery(query =>
            {
                rowCount = query.Select<T>().Count();
            });

            return rowCount;
        }

        public virtual int RowCount(Expression<Func<T, bool>> where)
        {
            if (where == null)
                return RowCount();

            int rowCount = -1;

            ExecuteQuery(query =>
            {
                rowCount = query.Select<T>().Where<T>(where).Count();
            });

            return rowCount;
        }

        public virtual void Update(T entity)
        {
            ExecuteQuery(query =>
            {
                query.Update<T>(entity);
                query.SaveChanges();
            });
        }

        #endregion
    }
}
