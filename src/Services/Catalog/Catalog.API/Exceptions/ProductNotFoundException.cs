namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(Guid productId)
        : base($"Product with ID '{productId}' not found.")
    {
    }

    public ProductNotFoundException(Guid productId, Exception innerException)
        : base($"Product with ID '{productId}' not found.", innerException)
    {
    }
}