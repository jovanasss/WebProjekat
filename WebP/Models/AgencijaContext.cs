using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class AgencijaContext : DbContext
    {
        
        public DbSet<Radnik> Radnici { get; set; }  
        public DbSet<Posao> Poslovi { get; set; }
        public  DbSet<Dan> Dani { get; set; }
        public DbSet<Agencija> Agencije { get; set; }
        public DbSet<Spoj> RadniciPoslovi { get; set; }


        public AgencijaContext(DbContextOptions options) : base(options)
        {
            
        }
        
    }
}