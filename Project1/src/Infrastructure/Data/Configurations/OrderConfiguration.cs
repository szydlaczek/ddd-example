using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project1.Domain.Entities;

namespace Project1.Infrastructure.Data.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CreatedAt)
            .IsRequired();

        builder.Ignore(p => p.DomainEvents);

        builder.Metadata.FindNavigation(nameof(Order.Items))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(o => o.Items, itemBuilder =>
        {
            itemBuilder.ToTable("OrderItems");

            itemBuilder.WithOwner().HasForeignKey("OrderId");

            itemBuilder.HasKey("OrderId", "ProductId");

            itemBuilder.Property(i => i.ProductId)
                .IsRequired();

            itemBuilder.Property(i => i.Quantity)
                .IsRequired();

            itemBuilder.OwnsOne(i => i.UnitPrice, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("UnitPriceAmount")
                    .HasColumnType("decimal(18,2)");

                price.Property(p => p.Currency)
                    .HasColumnName("UnitPriceCurrency")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        });

        builder.OwnsOne(o => o.Status, status =>
        {
            status.Property(s => s.Value)
                .HasColumnName("Status")
                .HasMaxLength(20)
                .IsRequired();
        });
    }
}
