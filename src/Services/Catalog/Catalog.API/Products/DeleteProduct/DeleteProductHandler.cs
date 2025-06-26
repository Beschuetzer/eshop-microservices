namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(
    Guid Id
) : IRequest<DeleteProductResult>;

public record DeleteProductResult(
    bool IsSuccess
);

internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling DeleteProductCommand with {@Request}", command);

        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        
        return new DeleteProductResult(true);
    }
}
