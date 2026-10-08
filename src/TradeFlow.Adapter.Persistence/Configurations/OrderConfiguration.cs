using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeFlow.Domain.Entities;

namespace TradeFlow.Adapter.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(o => o.OrderNumber).IsUnique();
        builder.HasIndex(o => o.CustomerId);

        builder.OwnsOne(o => o.ShippingAddress, a =>
        {
            a.Property(x => x.Street).HasMaxLength(200).IsRequired();
            a.Property(x => x.City).HasMaxLength(100).IsRequired();
            a.Property(x => x.State).HasMaxLength(100).IsRequired();
            a.Property(x => x.ZipCode).HasMaxLength(20).IsRequired();
            a.Property(x => x.Country).HasMaxLength(100).IsRequired();
        });

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.ProductName)
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsOne(i => i.UnitPrice, m =>
        {
            m.Property(x => x.Amount).HasColumnName("UnitPrice").HasPrecision(18, 2);
            m.Property(x => x.Currency).HasColumnName("Currency").HasMaxLength(3);
        });
    }
}
