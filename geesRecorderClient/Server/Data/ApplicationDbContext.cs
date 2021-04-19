using geesRecorderClient.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace geesRecorderClient.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<PersonEvent> PersonEvents { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<AttendanceProject> AttendanceProjects { get; set; }

        public DbSet<DataCollectionProject> DataCollectionProjects { get; set; }

        public DbSet<ServerState> ServerState { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .Property(x => x.FingerprintIds)
                .HasConversion(v => JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                v => JsonSerializer.Deserialize<List<int>>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));

            modelBuilder.Entity<Project>()
                .HasDiscriminator(x => x.Type)
                .HasValue<Project>(ProjectType.None)
                .HasValue<AttendanceProject>(ProjectType.Attendance)
                .HasValue<DataCollectionProject>(ProjectType.DataCollection);
        }
    }
}
