using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
public partial class ChildrenController : BaseController
{
    public ChildrenController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}   