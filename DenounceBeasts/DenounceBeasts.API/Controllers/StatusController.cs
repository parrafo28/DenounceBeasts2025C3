using DenounceBeasts.API.Data;
using DenounceBeasts.API.Models.DTOs;
using DenounceBeasts.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : BaseController<Status>
    {
        //readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context): base(context)  
        {
            //_context = context;
        }

    }
}
