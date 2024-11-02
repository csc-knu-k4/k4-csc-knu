using System;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.PortableExecutable;
using OsvitaDAL.Entities;

namespace OsvitaDAL.Data
{
    public class OsvitaDbContext : DbContext
    {
        public OsvitaDbContext(DbContextOptions<OsvitaDbContext> options)
        : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ContentBlock> ContentBlocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

