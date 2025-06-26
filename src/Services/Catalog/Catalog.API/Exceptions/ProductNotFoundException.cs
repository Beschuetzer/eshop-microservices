namespace Catalog.API.Exceptions;

public class ProductNotFoundException(Guid productId) : NotFoundException("Product", productId)
{
}