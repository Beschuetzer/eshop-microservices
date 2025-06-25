using MediatR;
namespace BuildingBlocks.CQRS;

// Used for queries that return a response
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQueryHandler<TResponse> where TResponse : notnull { }