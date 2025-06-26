namespace Catalog.API.Models.Products.GetProducts;

public record GetProductsResponse(
    IEnumerable<Product> Products
);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductQuery());
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get Products")
        .WithDescription("This endpoint retrieves a list of products from the catalog. " +
            "You can filter the products by providing an optional product ID.");
    }
}