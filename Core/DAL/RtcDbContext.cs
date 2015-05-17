namespace RoundTheClock.Core.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RtcDbContext : DbContext, IRtcDbContext
    {
        public RtcDbContext()
            : base("name=RoundTheClock")
        {
        }

        public virtual DbSet<CustomerDAO> Customers { get; set; }
        public virtual DbSet<ProjectDAO> Projects { get; set; }
        public virtual DbSet<TaskDAO> Tasks { get; set; }
        public virtual DbSet<EntryDAO> Entries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDAO>()
                .HasMany(e => e.Entries)
                .WithRequired(e => e.Customers)
                .HasForeignKey(e => e.customer_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectDAO>()
                .HasMany(e => e.Entries)
                .WithRequired(e => e.Projects)
                .HasForeignKey(e => e.project_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectDAO>()
                .HasMany(e => e.Customers)
                .WithMany(e => e.Projects)
                .Map(m => m.ToTable("CustomerProjects").MapLeftKey("project_fk").MapRightKey("customer_fk"));

            modelBuilder.Entity<TaskDAO>()
                .HasMany(e => e.Entries)
                .WithRequired(e => e.Tasks)
                .HasForeignKey(e => e.task_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskDAO>()
                .HasMany(e => e.Projects)
                .WithMany(e => e.Tasks)
                .Map(m => m.ToTable("ProjectTasks").MapLeftKey("task_fk").MapRightKey("project_fk"));
        }
    }
}
