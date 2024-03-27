using AutoMapper;
using Infrastructure.Persistence;

namespace WebAPI.Controllers;

public partial class MembersController : BaseController
{
    public MembersController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}