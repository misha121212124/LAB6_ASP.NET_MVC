using lab6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;


namespace lab6
{
    public class BdContext : DbContext
    {

/*        public BdContext() : base() {

        }*/

        public BdContext(DbContextOptions<BdContext> options)
            : base(options)
        {
        }
        public DbSet<Countries> Countries { get; set; }

        public DbSet<Cities> Cities { get; set; }

        public DbSet<Cinemas> Cinemas { get; set; }


/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("Server =DESKTOP-NJBSOAM\\SQLEXPRESS;Initial Catalog=Lab4;Integrated Security=True");
            optionsBuilder.UseSqlServer("Server =DESKTOP-NJBSOAM\\SQLEXPRESS;Database=lab6;; Trusted_Connection = True; MultipleActiveResultSets = true", x => x.MigrationsAssembly("lab4"));

        }*/



        public override int SaveChanges()
        {
            audit();
            var result = base.SaveChanges();
            return result;
        }
        private void audit()
        {
            ChangeTracker.DetectChanges();
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged) continue;
                
                var properties = entry.Properties;

                foreach (var property in entry.Properties)
                {
                    if (entry.State == EntityState.Added)
                        Console.WriteLine("Поле " + property.Metadata.Name + "  доданого елементу " + property.CurrentValue);
                    else if ( !property.OriginalValue.Equals( property.CurrentValue ) )
                        Console.WriteLine("Поле " + property.Metadata.Name + " мало значення:" + property.OriginalValue +
                            ", а стало: " + property.CurrentValue);
                }
            }
        }
    }
}
