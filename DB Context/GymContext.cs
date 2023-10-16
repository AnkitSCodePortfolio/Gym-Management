using Gym_Management.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Gym_Management
{
    public class GymContext:DbContext
    {
        public GymContext(DbContextOptions<GymContext> options ):base (options)
        {
            

        }
        public DbSet<Customer> customer { get; set; }
        public DbSet<Attendance> employeestatus { get; set; }
        public DbSet<Membership> members { get; set; }
        public DbSet<Payment> payment { get; set; }
        public DbSet<Receptionist> receptionist { get; set; }
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<Timings> timings { get; set; }
        public DbSet<EmpStatus> EmployeeStatus { get; set; }
        public DbSet<Genders > Gender { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<PayMethods> PayMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Trainer)
                .WithMany(t => t.Customers)
                .HasForeignKey(c => c.TrainerId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
