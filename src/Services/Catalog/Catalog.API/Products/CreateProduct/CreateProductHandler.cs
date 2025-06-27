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

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Product category is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Product image file is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
    }
}

// represents the result/response of the CreateProduct command
public record CreateProductResult(
    Guid Id
);

//injecting Marten's IDocumentSession to handle database operations
internal class CreateProductCommandHandler(IDocumentSession session) : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        // NOTE: If you weren't to use pipeline behaviors for validation, you could validate directly in the handler by using DI to get the validator and do manual checks.  This is not recommended though as it couples the validation logic to the handler and has to be written for every handler:

        // var result = await validator.ValidateAsync(request, cancellationToken);
        // var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

        // if(errors.Any())
        // {
        //     logger.LogWarning("Validation failed for CreateProductCommand: {@Errors}", errors);
        //     throw new ValidationException(errors.FirstOrDefault());
        // }

        var product = new Product
        {
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);

    }
}
