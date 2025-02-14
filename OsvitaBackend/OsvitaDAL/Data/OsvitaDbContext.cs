using System;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AssignmentLink> AssignmentLinks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<ChapterProgressDetail> ChapterProgressDetails{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

