using Application.Common.Interfaces;
using MediatR;

namespace Application.Account.Commands;

public class LogoutCommand : ICommand
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly IAccountService _accountService;

        public LogoutCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            _accountService.Logout();

            return Unit.Task;
        }
    }
}