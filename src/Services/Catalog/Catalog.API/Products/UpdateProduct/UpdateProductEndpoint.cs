namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(
    Guid Id,
    string Name,
    List<string> Category,
    string ImageFile,
    string Description,
    decimal Price
);

public record UpdateProductResponse(
    bool IsSuccess
);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            try
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            }
            catch (ValidationException ex)
            {
                return Results.BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return Results.Problem("Something went wrong while updating the product.");
            }

        })
            .WithName("UpdateProduct")
            .WithSummary("Updates an existing product")
            .WithDescription("Updates an existing product by its ID")
            .Produces<Product>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);
    }
}