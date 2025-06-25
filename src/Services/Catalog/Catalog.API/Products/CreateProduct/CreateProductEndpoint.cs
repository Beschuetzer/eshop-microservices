namespace Catalog.API.Models.Products.CreateProduct;

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


public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await mediator.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Creates a new product")
            .WithDescription("This endpoint allows you to create a new product in the catalog. " +
                "You need to provide the product details such as name, category, description, image file, and price.");

    }
}