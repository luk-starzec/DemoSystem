using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using PrioritySetter.Data;

namespace PrioritySetter.Data
{
    public class PrioritySetterContext : DbContext
    {
        public PrioritySetterContext(DbContextOptions<PrioritySetterContext> options)
            : base(options)
        { }

        public DbSet<ErrorPriority> ErrorPriority { get; set; }
        public DbSet<AppPriority> AppPriority { get; set; }
        public DbSet<Priority> Priority { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErrorPriority>(entity =>
            {
                entity.ToTable("ErrorPriorities");
                entity.HasKey(r => r.Error);
                entity.Property(r => r.PriorityLevel).HasConversion<int>().HasColumnName("PriorityLevelId");
                entity.HasOne(r => r.PriorityRelation).WithMany().HasForeignKey(r => r.PriorityLevel);
            });

            modelBuilder.Entity<AppPriority>(entity =>
            {
                entity.ToTable("AppPriorities");
                entity.HasKey(r => r.App);
                entity.Property(r => r.PriorityLevel).HasConversion<int>().HasColumnName("PriorityLevelId");
                entity.HasOne(r => r.PriorityRelation).WithMany().HasForeignKey(r => r.PriorityLevel);
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("DicPriorityLevels");
                entity.HasKey(r => r.PriorityLevel);
                entity.Property(r => r.PriorityLevel).HasConversion<int>().HasColumnName("PriorityLevelId");
            });

            modelBuilder.Entity<Priority>().HasData(InitialData.GetPriorities());
            modelBuilder.Entity<ErrorPriority>().HasData(InitialData.GetErrorPriorities());
            modelBuilder.Entity<AppPriority>().HasData(InitialData.GetAppPriorities());

            base.OnModelCreating(modelBuilder);
        }


    }
}
