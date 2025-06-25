using MediatR;

namespace BuildingBlocks.CQRS;

// Represents a command in the CQRS pattern.
public interface ICommand : ICommand<Unit> { }

//Used for commands that return a response
public interface ICommand<out TResponse> : IRequest<TResponse> { }
