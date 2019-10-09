namespace ContactManagement.Storage.DbConfiguration
{
    using ContactManagement.Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable(nameof(Contact), "dbo");
            builder.HasKey(c => c.ContactId);
            builder.Property(c => c.ContactId).HasColumnName(nameof(Contact.ContactId));
            builder.Property(c => c.FirstName).HasColumnName(nameof(Contact.FirstName));
            builder.Property(c => c.LastName).HasColumnName(nameof(Contact.LastName));
            builder.Property(c => c.Email).HasColumnName(nameof(Contact.Email));
            builder.Property(c => c.PhoneNumber).HasColumnName(nameof(Contact.PhoneNumber));
            builder.Property(c => c.IsActive).HasColumnName(nameof(Contact.IsActive));
        }
    }
}