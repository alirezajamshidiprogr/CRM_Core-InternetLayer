using CRM_Core.DomainLayer;
using CRM_Core.Entities.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace CRM_Core.DataAccessLayer
{
    using CRM_Core.DataLayers.EntityConfigurations;
    using CRM_Core.Entities.Models;
    using CRM_Core.Entities.Models.General;
    using CRM_Core.Entities.Models.Salon;
    //using CRM_Core.DataLayers.EntityConfigurations;
    //using CRM_Core.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using static CRM_Core.Entities.Models.Regions.Region;

    //public class DBContextFactory : IDesignTimeDbContextFactory<CRM_CoreDB>
    //{
    //    public CRM_CoreDB CreateDbContext(string[] args)
    //    {
    //        DbContextOptionsBuilder<CRM_CoreDB> optionsBuilder = new DbContextOptionsBuilder<CRM_CoreDB>();
    //        optionsBuilder.UseSqlServer("Server=.; initial Catalog=MoshattehDB; integrated security=true;");
    //        return new CRM_CoreDB(optionsBuilder.Options);
    //        //return new CRM_CoreDB();
    //    }
    //}

    public class CRM_CoreDB : IdentityDbContext
    {
        public CRM_CoreDB(DbContextOptions<CRM_CoreDB> options)
            : base(options)
        { 
        }
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
        public virtual DbSet<CRM_Core.Entities.Reservation.Reservation> Reservation { get; set; }
        public virtual DbSet<TBASPayType> TBASPayType { get; set; }
        public virtual DbSet<TBASServices> TBASServices { get; set; }
        public virtual DbSet<ClerkServices> ClerkServices { get; set; }
        public virtual DbSet<TBASIntroductionType> TBASIntroductionType { get; set; }
        public virtual DbSet<PeopleServices> PeopleServices { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<UserRegion> UserRegion { get; set; }
        public virtual DbSet<ActivityNumber> ActivityNumber { get; set; }
        public virtual DbSet<TBASState> TBASState { get; set; }
        public virtual DbSet<TBASMenu> TBASMenu { get; set; }
        public virtual DbSet<UserMenu> UserMenu { get; set; }
        public virtual DbSet<SalonInfo> SalonInfo{ get; set; }
        public virtual DbSet<SalonCosts> SalonCosts { get; set; }
        public virtual DbSet<BillCosts> BillCosts { get; set; }
        public virtual DbSet<TransferCosts> TransferCosts { get; set; }
        public virtual DbSet<TBASSalonCosts> TBASSalonCosts { get; set; }
        public virtual DbSet<Reminder> Reminder { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<People>().HasQueryFilter(c => c.IsActive);
            builder.Entity<SalonCosts>().HasQueryFilter(c => c.IsActive);
            builder.Entity<Reminder>().HasQueryFilter(c => c.IsActive);
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}

