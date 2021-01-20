using Microsoft.EntityFrameworkCore;
using VolumeWebService.Models;

namespace VolumeWebService.DataAccess
{
    public class VolumeDBContext : DbContext
    {
        public DbSet<VolumeResult> VolumeResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Volume.db");
        }
    }
}