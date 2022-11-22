using System.Data.Entity;

namespace kongcore.dk.Core.DB
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=umbracoDbDSN")
        {
        }

        public virtual DbSet<MyUser> myuser { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
