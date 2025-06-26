namespace Catalog.API.Models.Products.GetProductById;

public record GetProductByIdResponse(
    Product Product
);


public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/{id}", async (Guid id, ISender sender) =>
        {
            try
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            }
            catch (ProductNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 Internal Server Error
                return Results.Problem("An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("GetProductByIdPathParam")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get Product By Id")
        .WithDescription("This endpoint retrieves a product by its ID from the catalog.");

        app.MapGet("/product", async ([FromQuery(Name = "id")] Guid id, ISender sender) =>
        {
            try
            {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
            }
            catch (ProductNotFoundException ex)
            {
            return Results.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
            // Log the exception and return a 500 Internal Server Error
            return Results.Problem("An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("GetProductByIdQueryParam")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get Product By Id (Query Param)")
        .WithDescription("This endpoint retrieves a product by its ID from the catalog using a query parameter named 'id'.");
    }
}

