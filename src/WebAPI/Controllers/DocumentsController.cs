using System.Reflection.Metadata;
using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public partial class DocumentsController : BaseController
{
    public DocumentsController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}