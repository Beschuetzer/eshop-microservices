namespace Catalog.API.Models.Products.CreateProduct;

// todo: is there a way to use the Product class here instead of duplicating the properties?
// the data we need to create a product is the same as the Product class, but we don't want to expose the entire Product class in the API.
public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
) : IRequest<CreateProductResult>;

// represents the result/response of the CreateProduct command
public record CreateProductResult(
    Guid Id
);

//injecting Marten's IDocumentSession to handle database operations
internal class CreateProductCommandHandler(IDocumentSession session) : IRequestHandler<CreateProductCommand, CreateProductResult>
{

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price
        };

        Console.WriteLine($"Creating product: {product.Name} with price: {product.Price}");
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
    }
}
