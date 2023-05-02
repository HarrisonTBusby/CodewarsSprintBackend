using CodewarsSprintBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CodewarsSprintBackend.Services.Context
{
    public class DataContext : DbContext
    {
        public DbSet<UserModel> UserInfo { get; set; }
        public DbSet<ReservationModel> ReservationInfo { get; set; }
        
        public DataContext(DbContextOptions options): base(options)
        {}


        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
        }

    }
}