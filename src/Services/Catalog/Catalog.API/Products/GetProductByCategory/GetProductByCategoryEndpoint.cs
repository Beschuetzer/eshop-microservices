namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResponse(
    IEnumerable<Product> Products
);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            try
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));
                var response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 Internal Server Error
                return Results.Problem("An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError);
            }

        })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get Products by Category")
        .WithDescription("This endpoint retrieves a list of products filtered by the specified category.");
    }
}