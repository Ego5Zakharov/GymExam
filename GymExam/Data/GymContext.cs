using Microsoft.EntityFrameworkCore;
using GymExam.Models;

namespace GymExam.Data
{
    public class GymContext : DbContext
    {
        public DbSet<Administration>? Administration { get; set; }
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Coach>? Coaches { get; set; }
        public DbSet<Workout>? Workouts { get; set; }
        public DbSet<Gym>? Gyms { get; set; }

        public GymContext(DbContextOptions<GymContext>options):base(options)
        {

        }
    }

}
