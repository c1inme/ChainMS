using System.Data;
using System.Data.Common;

namespace CMS.Kernel
{
    public static class ExtensionMethods
    {
        public static bool IsDatabaseOnline(this DbConnection connection)
        {
            try
            {
                if ((connection.State & ConnectionState.Open) != ConnectionState.Open)
                    connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
