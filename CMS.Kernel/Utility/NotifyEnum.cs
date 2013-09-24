using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Kernel
{
    public enum NotifyEnum
    {
        BeforeSaveMulti,
        BeforeSave,
        BeforeDeleted,
        BeforeDeletedMulti,
        AfterSave,
        AfterSaveMulti,
        AfterDeleted,
        AfterDeletedMulti
    }
}
