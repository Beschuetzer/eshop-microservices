namespace BuildingBlocks.CQRS;

using MediatR;

// Used for queries (read operations) that return a response.
public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull { }