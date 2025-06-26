namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string ImageFile,
    string Description,
    decimal Price
) : IRequest<UpdateProductResult>;

public record UpdateProductResult(
    bool IsSuccess
);

internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
        if (product == null)
        {
            logger.LogWarning("Product with ID {Id} not found", request.Id);
            throw new ProductNotFoundException(request.Id);
        }

        product.Name = request.Name;
        product.Category = request.Category;
        product.ImageFile = request.ImageFile;
        product.Description = request.Description;
        product.Price = request.Price;

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}