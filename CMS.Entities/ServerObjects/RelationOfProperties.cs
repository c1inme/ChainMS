using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Entities.ServerObjects
{
    [Table("RelationOfProperties")]
    public class RelationOfProperties : Entity
    {
        private Guid _IdBelong;
        public Guid IdBelong
        {
            get { return _IdBelong; }
            set { _IdBelong = value; }
        }

        private Guid _IdProperty;
        public Guid IdProperty
        {
            get { return _IdProperty; }
            set { _IdProperty = value; }
        }

        private string _vaueProperty;
        public string VaueProperty
        {
            get { return _vaueProperty; }
            set { _vaueProperty = value; }
        }


        private string _Dicriminator;
        public string Dicriminator
        {
            get { return _Dicriminator; }
            set { _Dicriminator = value; }
        }
         [NotMapped]
        //[ForeignKey("IdBelong")]
        public Product Product
        {
            get;
            set;
        }
         [NotMapped]
        //[ForeignKey("IdBelong")]
        public ProductCategory ProductCategory
        {
            get;
            set;
        }


        [ForeignKey("IdProperty")]
        public PropertiesDefinition PropertyDef
        {
            get;
            set;
        }

        [NotMapped]
        public List<ProductCategory> Categories;


        [NotMapped]
        public List<Product> Products;
    }
}
