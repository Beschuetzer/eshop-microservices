namespace Catalog.API.Models.Products.GetProductById;
 
public record GetProductByIdQuery(
    Guid ProductId
) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(
    Product Product
);

internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProductByIdQuery with {@Query}", query);
        var product = await session.LoadAsync<Product>(query.ProductId, cancellationToken);
        
        if (product == null)
        {
            logger.LogWarning("Product with ID {ProductId} not found.", query.ProductId);
            throw new ProductNotFoundException(query.ProductId);
        }

        return new GetProductByIdResult(product);
    }
}