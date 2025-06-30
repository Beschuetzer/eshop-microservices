using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(
                // this tell EF Core how to convert the OrderId ValueObject to a database type
                orderId => orderId.Value,

                // this tells EF Core how to convert the database type back to the OrderId ValueObject
                dbId => OrderId.Of(dbId)
            );


        // The CustomerId type is a value object(record) that wraps a Guid and uniquely identifies a customer.
        // There is no direct navigation property to a Customer entity in the Order class, only the CustomerId value object. This is a common DDD(Domain-Driven Design) pattern to decouple aggregates and avoid direct entity references.

        builder.HasOne(o => o.CustomerId)
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        // configures the one to many relationship between Order and OrderItem
        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(o => o.OrderName, orderNameBuilder =>
        {
            orderNameBuilder
                .Property(on => on.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
        {
          addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();

            addressBuilder.Property(a => a.EmailAddress).HasMaxLength(180);

            addressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();

            addressBuilder.Property(a => a.Country).HasMaxLength(50);

            addressBuilder.Property(a => a.ZipCode).HasMaxLength(50);
            addressBuilder.Property(a => a.State).HasMaxLength(50);
            addressBuilder.Property(a => a.City).HasMaxLength(50);
        });

        builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();

            addressBuilder.Property(a => a.EmailAddress).HasMaxLength(180);

            addressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();

            addressBuilder.Property(a => a.Country).HasMaxLength(50);

            addressBuilder.Property(a => a.ZipCode).HasMaxLength(50);
            addressBuilder.Property(a => a.State).HasMaxLength(50);
            addressBuilder.Property(a => a.City).HasMaxLength(50);
        });

        builder.ComplexProperty(o => o.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(p => p.CardName).HasMaxLength(50);
            paymentBuilder.Property(p => p.CardNumber).HasMaxLength(24).IsRequired();
            paymentBuilder.Property(p => p.Expiration)
                .HasMaxLength(10);
            paymentBuilder.Property(p => p.CVV).HasMaxLength(3);
            paymentBuilder.Property(p=> p.PaymentMethod).HasMaxLength(50).IsRequired();
        });

        builder.Property(o => o.Status)
            .HasConversion(
                // this tells EF Core how to convert the OrderStatus enum to a database type
                status => status.ToString(),
                // this tells EF Core how to convert the database type back to the OrderStatus enum
                dbStatus => Enum.Parse<OrderStatus>(dbStatus)
            )
            .HasDefaultValue(OrderStatus.Draft);
    }
}
