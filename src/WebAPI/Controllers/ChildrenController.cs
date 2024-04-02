using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
public partial class ChildrenController : BaseApiController
{
    public ChildrenController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}   