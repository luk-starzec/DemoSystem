using Microsoft.EntityFrameworkCore;

namespace DescriptionProvider.Data
{
    public class DescriptionDbContext : DbContext
    {
        public DescriptionDbContext(DbContextOptions<DescriptionDbContext> options)
            : base(options)
        { }

        public DbSet<TextSource> TextSources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TextSource>(entity =>
            {
                entity.ToTable("TextSources");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).ValueGeneratedOnAdd();
                entity.HasIndex(r => r.Name).IsUnique();
            });

            modelBuilder.Entity<TextSource>().HasData(GetDefaultTextSources());

            base.OnModelCreating(modelBuilder);
        }

        private TextSource[] GetDefaultTextSources()
        {
            return new TextSource[]
            {
                new TextSource
                {
                    Id = 1,
                    Name = "Lorem ipsum",
                    Text= InitialData.LoremIpsumText,
                },
                new TextSource
                {
                    Id = 2,
                    Name = "Li Europan lingues",
                    Text= InitialData.LiEuropanLinguesText,
                }
            };
        }
    }
}
