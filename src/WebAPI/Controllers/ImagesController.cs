using AutoMapper;
using Infrastructure.Persistence;

namespace WebAPI.Controllers;

public partial class ImagesController : BaseController
{
    public ImagesController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}