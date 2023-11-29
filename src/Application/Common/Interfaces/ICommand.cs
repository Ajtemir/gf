using MediatR;

namespace Application.Common.Interfaces;

public interface ICommand : IRequest<Unit>
{
}