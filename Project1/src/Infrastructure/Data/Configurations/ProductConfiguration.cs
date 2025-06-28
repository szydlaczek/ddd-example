using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project1.Domain.Entities;
using Project1.Domain.ValueObjects;

namespace Project1.Infrastructure.Data.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

        builder.OwnsOne(p => p.Price, price =>
        {
            price.Property(p => p.Amount).HasColumnName($"{nameof(Product.Price)}_{nameof(Money.Amount)}").IsRequired();
            price.Property(p => p.Currency).HasColumnName($"{nameof(Product.Price)}_{nameof(Money.Currency)}").IsRequired().HasMaxLength(3);
        });

        builder.Ignore(p => p.DomainEvents);
    }
}
