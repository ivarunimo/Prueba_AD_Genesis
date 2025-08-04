using Microsoft.EntityFrameworkCore;

namespace ContextoDB
{
    public class BancaContext :DbContext
    {
        public BancaContext(DbContextOptions<BancaContext> options ) : base(options) { 
        }

        public DbSet<Tables.Account> Accounts { get; set; }
        public DbSet<Tables.User> Users { get; set; }
        public DbSet<Tables.Movement> Movements { get; set; }
        public DbSet<Tables.DebitCard> DebitCard { get; set; }
        public DbSet<Tables.Transfer> Transfers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuración de cuentas bancarias
            modelBuilder.Entity<Tables.Account>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.id_user);

            //Configuración de tarjetas de débito
            modelBuilder.Entity<Tables.DebitCard>()
                .HasOne(dc => dc.Account)
                .WithMany()
                .HasForeignKey(dc => dc.id_account);

            //Configuración de movimientos
            modelBuilder.Entity<Tables.Movement>()
                .HasOne(m => m.Account)
                .WithMany()
                .HasForeignKey(m => m.origin);
            modelBuilder.Entity<Tables.Movement>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.id_user)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Tables.Transfer>()
                .HasOne(t => t.OriginAccount)
                .WithMany()
                .HasForeignKey(t => t.origin)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tables.Transfer>()
                .HasOne(t => t.DestinyAccount)
                .WithMany()
                .HasForeignKey(t => t.destiny)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
