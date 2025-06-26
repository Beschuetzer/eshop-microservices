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
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            logger.LogWarning("Product with ID {Id} not found", command.Id);
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.ImageFile = command.ImageFile;
        product.Description = command.Description;
        product.Price = command.Price;

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}