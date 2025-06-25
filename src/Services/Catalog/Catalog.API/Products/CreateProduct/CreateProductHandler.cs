
using MediatR;

namespace Catalog.API.Models.Products.CreateProduct;

// todo: is there a way to use the Product class here instead of duplicating the properties?
// the data we need to create a product is the same as the Product class, but we don't want to expose the entire Product class in the API.
public record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
) : IRequest<CreateProductResponse>;

// represents the result/response of the CreateProduct command
public record CreateProductResponse(
    Guid Id
);

internal class CreateProductCommandHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
{

    public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price
        };

        //save to database here
        return new CreateProductResponse(Guid.NewGuid());
    }
}
