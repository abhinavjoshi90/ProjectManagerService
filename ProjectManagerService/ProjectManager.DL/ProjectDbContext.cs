namespace ProjectManager.DL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProjectDbContext : DbContext
    {
        public ProjectDbContext()
            : base("name=Model1")
        {
        }

        public virtual DbSet<ParentTask> ParentTasks { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Usr> Usrs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParentTask>()
                .Property(e => e.Parent_Task)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.Project_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.Task_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Usr>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Usr>()
                .Property(e => e.LastName)
                .IsUnicode(false);
        }
    }
}
