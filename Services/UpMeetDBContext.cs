using AngularProjectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularProjectAPI.Services
{
    public class UpMeetDBContext : DbContext
    {
        public UpMeetDBContext(DbContextOptions options) : base(options) 
        {

        }
        public DbSet<Events> Events { get; set; }

        public DbSet<Favorites> Favorites { get; set; }
    }
}

