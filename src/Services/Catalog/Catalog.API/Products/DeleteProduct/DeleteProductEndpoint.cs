namespace Catalog.API.Products.DeleteProduct;
public record DeleteProductRequest(
    Guid ProductId
) : IRequest<DeleteProductResponse>;

public record DeleteProductResponse(
    bool IsSuccess
);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{productId:guid}", async (Guid productId, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(productId));
            var response = result.Adapt<DeleteProductResponse>();
            return response;
        })
            .WithName("DeleteProduct")
            .WithSummary("Deletes an existing product")
            .WithDescription("Deletes an existing product by its ID")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}