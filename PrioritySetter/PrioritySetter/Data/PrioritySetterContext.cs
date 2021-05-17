using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace PrioritySetter.Data
{
    public class PrioritySetterContext : DbContext
    {
        public PrioritySetterContext(DbContextOptions<PrioritySetterContext> options)
            : base(options)
        { }

        public DbSet<ErrorPriority> ErrorPriority { get; set; }
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

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("DicPriorityLevels");
                entity.HasKey(r => r.PriorityLevel);
                entity.Property(r => r.PriorityLevel).HasConversion<int>().HasColumnName("PriorityLevelId");
            });

            modelBuilder.Entity<Priority>().HasData(GetDefaultPriorities());

            base.OnModelCreating(modelBuilder);
        }


        private Priority[] GetDefaultPriorities()
        {
            return new Priority[]
            {
                new Priority
                {
                    PriorityLevel = EnumPriorityLevel.Normal,
                    Name = EnumPriorityLevel.Normal.ToString(),
                    Description = "Do it",
                },
                new Priority
                {
                    PriorityLevel = EnumPriorityLevel.High,
                    Name = EnumPriorityLevel.High.ToString(),
                    Description = "Do it now!",
                },
                new Priority
                {
                    PriorityLevel = EnumPriorityLevel.Low,
                    Name = EnumPriorityLevel.Low.ToString(),
                    Description = "Maybe later",
                },
            };
        }

    }
}
