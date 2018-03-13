using Data_Access_Layer.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace Data_Access_Layer.DBContext
{
    public class TelephoneDirectoryDbContext : DbContext
    {
        public TelephoneDirectoryDbContext() : base("name=Default") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TelephoneDirectoryRecord>().Property(t => t.FirstName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<TelephoneDirectoryRecord>().Property(t => t.LastName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<TelephoneDirectoryRecord>().Property(t => t.Address).IsRequired();
            modelBuilder.Entity<TelephoneDirectoryRecord>().Property(t => t.PhoneType).HasColumnType("varchar").HasMaxLength(15).IsRequired();
            modelBuilder.Entity<TelephoneDirectoryRecord>().Property(t => t.PhoneNumber).HasColumnType("varchar").IsUnicode(true).HasMaxLength(15).IsRequired();
        }
        public DbSet<TelephoneDirectoryRecord> Directory { get; set; }
    }
}
