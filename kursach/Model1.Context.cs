
namespace kursach
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class OptEntities : DbContext
    {
        private static OptEntities _instance;
        public OptEntities()
            : base("name=OptEntities")
        {
        }
        public static OptEntities GetContext()
        {
            if (_instance == null)
                _instance = new OptEntities();
            return _instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Movement_goods> Movement_goods { get; set; }
        public virtual DbSet<Product_stock> Product_stock { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Supplies> Supplies { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
