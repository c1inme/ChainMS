using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CMS.Kernel
{
    public interface IRepository : IDisposable
    {


        E SaveEntity<E>(E entity, bool isTraceChange = false, bool isSkipVersion = false, bool isSystemChange = false) where E : class;

        E Insert<E>(E entity) where E : class;

        E Update<E>(E entity) where E : class;


        IEnumerable<E> SaveEntities<E>(IEnumerable<E> entities, bool isTraceChange = false, bool isSkipVersion = false, bool isSystemChange = false) where E : class;

        void Delete<E>(E entity, bool isSoftDeleted = true) where E : class;
        void DeleteEntities<E>(IEnumerable<E> entities, bool isSoftDeleted = true) where E : class;
        void DeleteByKey<E>(object entityKey, bool isSoftDeleted = true) where E : class;

        IQueryable<E> Select<E>(Expression<Func<E, object>>[] includes, bool isDelete = false) where E : class;


        int SaveChanges();

        int SaveChanges(bool validateEntities);
    }
}
