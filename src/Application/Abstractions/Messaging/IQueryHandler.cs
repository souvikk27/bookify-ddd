using Domain.Abstractions;
using MediatR;
// ReSharper disable TypeParameterCanBeVariant

namespace Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
	where TQuery : IQuery<TResult>
{
}