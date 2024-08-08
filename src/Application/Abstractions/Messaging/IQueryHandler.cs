using Domain.Abstractions;
using MediatR;

namespace Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
	where TQuery : IQuery<TResult>
{
}