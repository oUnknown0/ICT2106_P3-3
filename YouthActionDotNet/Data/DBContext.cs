using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.Data{
    public class DBContext : DbContext{
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DBContext(){
            DbContextOptions<DBContext> options = new DbContextOptions<DBContext>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Employee>().ToTable("Employee")
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId);
        modelBuilder.Entity<Volunteer>().ToTable("Volunteer")
            .HasOne(e => e.User)
            .WithMany().IsRequired(false)
            .HasForeignKey(e => e.UserId);
        modelBuilder.Entity<Volunteer>()
            .HasOne(e=> e.User)
            .WithMany()
            .HasForeignKey(e => e.ApprovedBy);
        modelBuilder.Entity<Donor>().ToTable("Donor")
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId);
        modelBuilder.Entity<ServiceCenter>().ToTable("ServiceCenter")
            .HasOne(e => e.RegionalDirector)
            .WithMany()
            .HasForeignKey(e => e.RegionalDirectorId);
        modelBuilder.Entity<VolunteerWork>().ToTable("VolunteerWork")
            .HasOne(e => e.employee)
            .WithMany()
            .HasForeignKey(e => e.SupervisingEmployee);
        modelBuilder.Entity<VolunteerWork>().ToTable("VolunteerWork")
            .HasOne(e => e.volunteer)
            .WithMany()
            .HasForeignKey(e => e.VolunteerId);
        modelBuilder.Entity<VolunteerWork>().ToTable("VolunteerWork")
            .HasOne(e => e.project)
            .WithMany()
            .HasForeignKey(e => e.projectId);
        modelBuilder.Entity<Project>().ToTable("Project")
            .HasOne(e => e.ServiceCenter)
            .WithMany()
            .HasForeignKey(e => e.ServiceCenterId);
        modelBuilder.Entity<Expense>().ToTable("Expense")
            .HasOne(e => e.project)
            .WithMany()
            .HasForeignKey(e => e.ProjectId);
        modelBuilder.Entity<Expense>().ToTable("Expense")
            .HasOne(e => e.user)
            .WithMany()
            .HasForeignKey(e => e.ApprovalId);
        modelBuilder.Entity<Expense>().ToTable("Expense")
            .HasOne(e => e.ExpenseReceiptFile)
            .WithMany()
            .HasForeignKey(e => e.ExpenseReceipt);
        modelBuilder.Entity<File>().ToTable("File");
        modelBuilder.Entity<Permissions>().ToTable("Permissions");
        modelBuilder.Entity<Donations>().ToTable("Donations")
            .HasOne(e => e.donor)
            .WithMany()
            .HasForeignKey(e => e.DonorId);
        modelBuilder.Entity<Donations>().ToTable("Donations")
            .HasOne(e => e.project)
            .WithMany()
            .HasForeignKey(e => e.ProjectId);
        // modelBuilder.Entity<Logs>().ToTable("Logs")
        // .HasOne(e => e.project)
        // .WithMany()
        // .HasForeignKey(e => e.ProjectId);
        }
        
    }
}