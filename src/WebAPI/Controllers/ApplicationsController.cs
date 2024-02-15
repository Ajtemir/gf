using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Controllers;

[Route("[controller]")]
public partial class ApplicationsController: BaseController
{
    public ApplicationsController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}