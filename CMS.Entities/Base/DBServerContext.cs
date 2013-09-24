using System.Data.Entity;
namespace CMS.Entities.ServerObjects
{
    public partial class DBServerContext : DbContext
    {
        public DBServerContext()
            : base("name=ServerModelChainMS")
        { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           //Database.SetInitializer<DBServerContext>(null);
            base.OnModelCreating(modelBuilder);
            //throw new UnintentionalCodeFirstException();
        }
        public DbSet<Gallery> Gallerys
        { get; set; }

        public DbSet<Comment> Comments
        { get; set; }

        public DbSet<Users> Userss
        { get; set; }

        public DbSet<TraceChanges> TraceChangess
        { get; set; }

        public DbSet<TableLastModified> TableLastModifieds
        { get; set; }

        public DbSet<SEOContent> SEOContents
        { get; set; }

        public DbSet<Rating> Ratings
        { get; set; }

        public DbSet<ProductCategory> ProductCategorys
        { get; set; }

        public DbSet<Product> Products
        { get; set; }

        public DbSet<PermissionDefinition> PermissionDefinitions
        { get; set; }

        public DbSet<News> Newss
        { get; set; }

        public DbSet<NameDictionary> NameDictionarys
        { get; set; }

        public DbSet<MenuCategory> MenuCategorys
        { get; set; }

        public DbSet<Manufacture> Manufactures
        { get; set; }

        public DbSet<Image> Images
        { get; set; }

        public DbSet<GroupPermission> GroupPermissions
        { get; set; }

        public DbSet<GroupMemberPermission> GroupMemberPermissions
        { get; set; }

        public DbSet<GrantPermission> GrantPermissions
        { get; set; }

        public DbSet<RelationOfProperties> RelationOfPropertiess
        { get; set; }

        public DbSet<PropertiesDefinition> PropertiesDefinitions
        { get; set; }

        public DbSet<Language> Languages { get; set; }

    }
}