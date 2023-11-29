using Application.Common.Dto;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries;

public class GetUsersListQuery : IRequest<PaginatedList<UserSummaryDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, PaginatedList<UserSummaryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserSummaryDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var count = await _context.Users.CountAsync(cancellationToken: cancellationToken);
            var users = _context.Users
                .AsNoTracking()
                .ProjectTo<UserSummaryDto>(_mapper.ConfigurationProvider);

            var paginatedList = await PaginatedList<UserSummaryDto>.CreateAsync(users,
                request.Page,
                request.PageSize,
                count,
                cancellationToken);

            return paginatedList;
        }
    }

    public class GetUsersListQueryValidator : AbstractValidator<GetUsersListQuery>
    {
        public GetUsersListQueryValidator()
        {
            RuleFor(x => x.Page).Page();
            RuleFor(x => x.PageSize).PageSize();
        }
    }
}