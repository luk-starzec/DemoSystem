using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DescriptionProvider.Data
{
    public class DescriptionDbContext : DbContext
    {
        public DescriptionDbContext(DbContextOptions<DescriptionDbContext> options)
            : base(options)
        { }

        public DbSet<TextSet> TextSets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TextSet>().ToTable("TextSets");
            modelBuilder.Entity<TextSet>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).ValueGeneratedOnAdd();
                entity.HasIndex(r => r.Name).IsUnique();
            });

            modelBuilder.Entity<TextSet>().HasData(GetDefaultTextSets());


            base.OnModelCreating(modelBuilder);
        }

        private TextSet[] GetDefaultTextSets()
        {
            return new TextSet[]
            {
                new TextSet
                {
                    Id = 1,
                    Name = "Lorem ipsum",
                    Text= InitialData.LoremIpsumText,
                },
                new TextSet
                {
                    Id = 2,
                    Name = "Li Europan lingues",
                    Text= InitialData.LiEuropanLinguesText,
                }
            };
        }

    }
}
