using Microsoft.EntityFrameworkCore;

namespace reservations.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; } 
        public DbSet<Availability> Availabilities { get; set; } 
        public DbSet<Provider> Providers { get; set; } 
        public DbSet<Client> Clients { get; set; } 
        
    }
}