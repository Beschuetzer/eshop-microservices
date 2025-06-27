namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string ImageFile,
    string Description,
    decimal Price
) : IRequest<UpdateProductResult>;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
    }
}

public record UpdateProductResult(
    bool IsSuccess
);

internal class UpdateProductCommandHandler(IDocumentSession session) : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = !string.IsNullOrWhiteSpace(command.Name) ? command.Name : product.Name;
        product.Category = command.Category is { Count: > 0 } ? command.Category : product.Category;
        product.ImageFile = !string.IsNullOrWhiteSpace(command.ImageFile) ? command.ImageFile : product.ImageFile;
        product.Description = !string.IsNullOrWhiteSpace(command.Description) ? command.Description : product.Description;
        product.Price = command.Price > 0 ? command.Price : product.Price;

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}