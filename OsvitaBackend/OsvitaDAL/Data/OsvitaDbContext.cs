using System;
using Microsoft.EntityFrameworkCore;
using OsvitaDAL.Entities;

namespace OsvitaDAL.Data
{
    public class OsvitaDbContext : DbContext
    {
        //public OsvitaDbContext()
        //{
        //}

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
        public DbSet<AssignmentSet> AssignmentSets { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AssignmentLink> AssignmentLinks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EducationClass> EducationClasses { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<TopicProgressDetail> TopicProgressDetails{ get; set; }
        public DbSet<AssignmentProgressDetail> AssignmentProgressDetails { get; set; }
        public DbSet<AssignmentSetProgressDetail> AssignmentSetProgressDetails { get; set; }
        public DbSet<EducationClassInvitation> EducationClassInvitations { get; set; }
        public DbSet<EducationClassPlan> EducationClassPlans { get; set; }
        public DbSet<AssignmentSetPlanDetail> AssignmentSetPlanDetails { get; set; }
        public DbSet<EducationPlan> EducationPlans { get; set; }
        public DbSet<TopicPlanDetail> TopicPlanDetails { get; set; }
        public DbSet<RecomendationMessage> RecomendationMessages { get; set; }
        public DbSet<DailyAssignment> DailyAssignments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Server=localhost;Database=OsvitaDB;TrustServerCertificate=True;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.EducationClasses)
                .WithMany(e => e.Students);
            modelBuilder.Entity<EducationClass>()
                .HasOne(e => e.Teacher);
            modelBuilder.Entity<User>()
                .HasOne(x => x.EducationPlan);
            modelBuilder.Entity<EducationPlan>()
                .HasMany(x => x.TopicPlanDetails);
            modelBuilder.Entity<TopicPlanDetail>()
                .HasOne(x => x.EducationPlan);
        }
    }
}

