using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.Entities.ServerObjects
{
    [Table("Language")]
    public class Language : Entity
    {
        string codeLanguage;
        public string CodeLanguage
        {
            get { return codeLanguage; }
            set { codeLanguage = value; }
        }
        string nameLanguage;
        public string NameLanguage
        {
            get { return nameLanguage; }
            set { nameLanguage = value; }
        }


        string pathFlagLanguage;
        public string PathFlagLanguage
        {
            get { return pathFlagLanguage; }
            set { pathFlagLanguage = value; }
        }

        int? orderNumber;
        public int? OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }

        [NotMapped]
        public HttpPostedFileBase Picture
        {
            get;
            set;
        }
    }
}
