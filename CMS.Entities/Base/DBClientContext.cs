using System.Data.Entity;
using CMS.Entities.ClientObjects;
namespace CMS.Entities
{
    public partial class DBClientContext : DbContext
    {
        
            public DBClientContext()
                : base("name=ClientChainMS")
            { }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                Database.SetInitializer<DBClientContext>(null);
                base.OnModelCreating(modelBuilder);
                //throw new UnintentionalCodeFirstException();
            }
            public DbSet<COCurrencyUnit> COCurrencyUnits
            { get; set; }
            public DbSet<COCustomerSupplier> COCustomerSuppliers
            { get; set; }
            public DbSet<COEmployee> COEmployees
            { get; set; }
            public DbSet<COGallery> COGallerys
            { get; set; }
            public DbSet<COGrantPermission> COGrantPermissions
            { get; set; }
            public DbSet<COGroupCustomerSupplier> COGroupCustomerSuppliers
            { get; set; }
            public DbSet<COGroupMemberPermission> COGroupMemberPermissions
            { get; set; }
            public DbSet<COGroupPermission> COGroupPermissions
            { get; set; }
            public DbSet<COImage> COImages
            { get; set; }
            public DbSet<COIncomeExpenditure> COIncomeExpenditures
            { get; set; }
            public DbSet<COIncomeExpenditureException> COIncomeExpenditureExceptions
            { get; set; }
            public DbSet<COLiabilitiesPeriod> COLiabilitiesPeriods
            { get; set; }
            public DbSet<COLocationCategory> COLocationCategorys
            { get; set; }
            public DbSet<COManufacture> COManufactures
            { get; set; }
            public DbSet<COManufacturer> COManufacturers
            { get; set; }
            public DbSet<COMenuCategory> COMenuCategorys
            { get; set; }
            public DbSet<CONameDictionary> CONameDictionarys
            { get; set; }
            public DbSet<CONews> CONewss
            { get; set; }
            public DbSet<COPaymentMethod> COPaymentMethods
            { get; set; }
            public DbSet<COPeriodPayment> COPeriodPayments
            { get; set; }
            public DbSet<COPermissionDefinition> COPermissionDefinitions
            { get; set; }
            public DbSet<COProduct> COProducts
            { get; set; }
            public DbSet<COProductCategory> COProductCategorys
            { get; set; }
            public DbSet<CORating> CORatings
            { get; set; }
            public DbSet<COSEOContent> COSEOContents
            { get; set; }
            public DbSet<COTableLastModified> COTableLastModifieds
            { get; set; }
            public DbSet<COTraceChanges> COTraceChangess
            { get; set; }
            public DbSet<COUsers> COUserss
            { get; set; }
            public DbSet<COWareHouse> COWareHouses
            { get; set; }
            public DbSet<COAccountInformation> COAccountInformations
            { get; set; }
            public DbSet<COComment> COComments
            { get; set; }
    }
}

