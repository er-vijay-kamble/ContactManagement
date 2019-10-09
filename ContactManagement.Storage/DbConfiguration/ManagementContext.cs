namespace ContactManagement.Storage.DbConfiguration
{
    using ContactManagement.Domain.Models;
    using Microsoft.EntityFrameworkCore;
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions<ManagementContext> options)
           : base(options)
        { }

        public virtual DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ContactConfig());
        }
    }
}
