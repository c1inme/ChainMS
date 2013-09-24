using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Kernel
{
    public class ContextManager
    {
        Dictionary<Type, DbContext> ContextCollection;

        private static ContextManager _instance;
        public static ContextManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ContextManager();
                return _instance;
            }
        }

        public ContextManager()
        {
            ContextCollection = new Dictionary<Type, DbContext>();
        }

        public TContext GetContext<TContext>() where TContext : DbContext
        {
            if (ContextCollection.Count > 0 && ContextCollection[typeof(TContext)] != null)
                return ContextCollection[typeof(TContext)] as TContext;
            var ctx = Activator.CreateInstance<TContext>();
            ContextCollection.Add(typeof(TContext), ctx);
            return ctx;
        }
    }
}
