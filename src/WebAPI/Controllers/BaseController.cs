using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    protected readonly ApplicationDbContext _context;
    protected readonly IMapper _mapper;

    public BaseController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}