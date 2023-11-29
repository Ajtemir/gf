using Application.Common.Dto;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Queries;

public class GetCurrentUserQuery : IRequest<UserDto>
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCurrentUserQueryHandler(ICurrentUserService currentUserService, IApplicationDbContext context, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId is 0)
            {
                throw new UnauthorizedAccessException();
            }

            var userDto = await _context.Users
                .AsNoTracking()
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == currentUserId, cancellationToken);

            if (userDto is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), currentUserId);
            }

            return userDto;
        }
    }
}