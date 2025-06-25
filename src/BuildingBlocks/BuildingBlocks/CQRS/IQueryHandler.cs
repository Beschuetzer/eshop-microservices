using MediatR;
namespace BuildingBlocks.CQRS;

// Used for queries that return a response
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IRequest<TResponse> where TResponse : notnull { }