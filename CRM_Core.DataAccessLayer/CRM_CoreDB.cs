using CRM_Core.DomainLayer;
using CRM_Core.Entities.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace CRM_Core.DataAccessLayer
{
    using CRM_Core.DataLayers.EntityConfigurations;
    using CRM_Core.Entities;

    //public class DBContextFactory : IDesignTimeDbContextFactory<CRM_CoreDB>
    //{
    //    public CRM_CoreDB CreateDbContext(string[] args)
    //    {
    //        DbContextOptionsBuilder<CRM_CoreDB> optionsBuilder = new DbContextOptionsBuilder<CRM_CoreDB>();
    //        optionsBuilder.UseSqlServer("Server=.; initial Catalog=MoshattehDB; integrated security=true;");
    //        //return new CRM_CoreDB(optionsBuilder.Options);
    //        return new CRM_CoreDB();
    //    }
    //}

    public class CRM_CoreDB : DbContext
    {
        public CRM_CoreDB(DbContextOptions<CRM_CoreDB> options)
            : base(options)
        { }
        // Introduce each Table To DataBase 
        public virtual DbSet<CRM_Core.DomainLayer.People> People { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<TBASGraduation> TBASGraduation { get; set; }
        public virtual DbSet<TBASCategory> TBASCategory { get; set; }
        public virtual DbSet<TBASPrefix> TBASPrefix { get; set; }
        public virtual DbSet<TBASPotential> TBASPotential { get; set; }
        public virtual DbSet<PeopleProperty> PeopleProperty { get; set; }
        public virtual DbSet<TBASPeopleTypeField> TBASPeopleTypeField { get; set; }
        public virtual DbSet<PeopleVirtual> PeopleVirtual { get; set; }
        public virtual DbSet<CRM_Core.DomainLayer.User> User { get; set; }
        public virtual DbSet<CRM_Core.Entities.Reservation.Reservation> Reservation { get; set; }
        public virtual DbSet<TBASPayType> TBASPayType { get; set; }
        public virtual DbSet<TBASServices> TBASServices { get; set; }
        public virtual DbSet<ClerkServices> ClerkServices { get; set; }
        public virtual DbSet<TBASIntroductionType> TBASIntroductionType { get; set; }
        public virtual DbSet<PeopleServices> PeopleServices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new People_Config());
            //builder.ApplyConfiguration(new AttributGrpConfig());
            //builder.ApplyConfiguration(new chartPostConfig());
            //builder.ApplyConfiguration(new CityConfig());
            //builder.ApplyConfiguration(new ComponentConfig());
            //builder.ApplyConfiguration(new ContactModuleConfig());
            //builder.ApplyConfiguration(new DetailItemConfig());
            //builder.ApplyConfiguration(new StoreInfoConfig());
            //builder.ApplyConfiguration(new ModuleConfig());
            //builder.ApplyConfiguration(new UserConfig());

            base.OnModelCreating(builder);
        }

        //public CRM_CoreDB(DbContextOptions<CRM_CoreDB> dbContextOptions) : base(dbContextOptions)
        //{

        //}
        //public CRM_CoreDB()
        //{

        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=.; initial Catalog=MoshattehDB; integrated security=true;");
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Query<CourseVw>().ToView("CourseVw");
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}

