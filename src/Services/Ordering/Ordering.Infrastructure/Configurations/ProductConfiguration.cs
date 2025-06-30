namespace Ordering.Infrastructure.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(
            id => id.Value,
            dbId => ProductId.Of(dbId)
        );
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
