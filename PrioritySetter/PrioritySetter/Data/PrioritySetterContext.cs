using Microsoft.EntityFrameworkCore;

namespace PrioritySetter.Data
{
    public class PrioritySetterContext : DbContext
    {
        public PrioritySetterContext(DbContextOptions<PrioritySetterContext> options)
            : base(options)
        { }

        public DbSet<TitlePriority> TitlePriority { get; set; }
        public DbSet<AppPriority> AppPriority { get; set; }
        public DbSet<Priority> Priority { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitlePriority>(entity =>
            {
                entity.ToTable("TitlePriorities");
                entity.HasKey(r => r.Title);
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
            modelBuilder.Entity<TitlePriority>().HasData(InitialData.GetTitlePriorities());
            modelBuilder.Entity<AppPriority>().HasData(InitialData.GetAppPriorities());

            base.OnModelCreating(modelBuilder);
        }
    }
}
