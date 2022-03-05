using Microsoft.EntityFrameworkCore;
using WaterDataAPI.Models;
using WaterDataAPI.Models.Concrete;

namespace WaterDataAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>().HasData(
                new Channel { Id = 1, Name = "River1"},
                new Channel { Id = 2, Name = "River2"}
            );

            modelBuilder.Entity<GroundWaterReservoir>().HasData(
                new GroundWaterReservoir { Id=1,Name="GS1",CurrentWaterLevel=200,Height=220,Width=100,PollutionLevel=8},  
                new GroundWaterReservoir { Id=2,Name="GS2",CurrentWaterLevel=220,Height=320,Width=170,PollutionLevel=12}  
            );
        }


        public DbSet<Channel> Channels { get; set; }
        public DbSet<GroundWaterReservoir> GroundWaterReservoirs { get; set; }
        public DbSet<RainWaterReservoir> RainWaterReservoirs { get; set; }
        public DbSet<WaterReservoir> WaterReservoirs { get; set; }


    }
}
