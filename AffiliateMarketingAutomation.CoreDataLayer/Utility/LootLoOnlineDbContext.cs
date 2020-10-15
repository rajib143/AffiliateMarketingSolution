
using AffiliateMarketingAutomation.CoreDataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AffiliateMarketingAutomation.CoreDataLayer
{
    public class LootLoOnlineDatabaseEntities : DbContext
    {
        //public IMemoryCache MemoryCache { get; }
        //private IConfiguration configuration;

        //public LootLoOnlineDbContext(IConfiguration config, IMemoryCache memoryCache)
        //{
        //    configuration = config;
        //    MemoryCache = memoryCache;
        //}

        public LootLoOnlineDatabaseEntities()
        {

        }

        public LootLoOnlineDatabaseEntities(DbContextOptions<LootLoOnlineDatabaseEntities> options)
       : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-K9JI0JL; Initial Catalog=LootLoOnlineDatabase; User id=sa;Password=rajib;");
                //optionsBuilder.UseSqlServer(@"Server=tcp:lootloonlinedb.database.windows.net,1433;Initial Catalog=LootLoOnlineDB;Persist Security Info=False;User ID=llo_admin;Password=lootloonline@2020;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        public DbSet<OfferProduct> OfferProducts { get; set; }
        public DbSet<DealsOfTheDay> DealsOfTheDays { get; set; }
        public DbSet<AllOffer> AllOffers { get; set; }
        public DbSet<VisitedUser> VisitedUsers { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<OfferBrand> OfferBrands { get; set; }
        public DbSet<Category> Categories { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OfferProduct>(entity =>
        //    {
        //        entity.Property(e => e.productId)
        //            .IsRequired()
        //            .HasMaxLength(50);

        //        //entity.Property(e => e.LastName)
        //        //    .IsRequired()
        //        //    .HasMaxLength(50);
        //    });

        //    modelBuilder.Entity<Log>(entity =>
        //    {
        //        entity.Property(e => e.Id)
        //            .IsRequired()
        //            .HasMaxLength(50);

        //        entity.Property(e => e.Thread)
        //            .IsRequired()
        //            .HasMaxLength(50);

        //       // entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
