using CRM_Core.DomainLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.DataLayers.EntityConfigurations
{
    public class People_Config : IEntityTypeConfiguration<People>
    {
        public void Configure(EntityTypeBuilder<People> modelBuilder)
        {
            modelBuilder.Property(t => t.TBASPrefixId).IsRequired(false);
            modelBuilder.Property(t => t.TBASPotentialId).IsRequired(false);
            modelBuilder.Property(t => t.TBASIntroductionTypeId).IsRequired(false);
            //modelBuilder.Property(t => t.HomeNo).HasMaxLength(100);
            //modelBuilder.Property(t => t.MobileNo).HasMaxLength(100);
            //modelBuilder.Property(t => t.postalCode).HasMaxLength(100);
            //modelBuilder.Property(t => t.PostAddress).HasMaxLength(100);
            //modelBuilder.Property(t => t.State).HasMaxLength(100);
            //modelBuilder.Property(t => t.NameFamily).HasMaxLength(100);

            //Relations

            //modelBuilder.HasOne(t => t.Users)
            //.WithMany(r => r.Address_Users)
            //.HasForeignKey(r => new { r.UserId })
            //.OnDelete(DeleteBehavior.Cascade);
        }

    }
}
