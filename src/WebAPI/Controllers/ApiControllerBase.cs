using AutoMapper;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}

[ApiController]
[Route("[controller]")]
public class BaseApiController : ApiControllerBase
{
    protected readonly ApplicationDbContext _context;
    protected readonly IMapper _mapper;

    public BaseApiController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}

