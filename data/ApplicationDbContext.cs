using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using weBelieveIT.models;

namespace weBelieveIT.data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Team> Team  { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Job> Job { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasKey(team => team.ID);
            modelBuilder.Entity<Member>().HasKey(member => member.Email);
            modelBuilder.Entity<Task>().HasKey(task => task.ID);
            modelBuilder.Entity<Job>().HasKey(job => job.JobNumber);

            modelBuilder.Entity<Member>()
            .HasOne<Team>(member => member.Team)
            .WithMany(team => team.Members);

            modelBuilder.Entity<Task>()
            .HasOne<Member>(task => task.Member)
            .WithMany(member => member.Tasks);

            modelBuilder.Entity<Task>()
            .HasOne<Job>(task => task.Job)
            .WithMany(job => job.Tasks);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
        {
            
        }


    }
}