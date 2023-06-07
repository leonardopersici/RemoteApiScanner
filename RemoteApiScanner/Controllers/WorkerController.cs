using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RemoteApiScanner.Data;

namespace RemoteApiScanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WorkerController> _logger;

        public WorkerController(ApplicationDbContext context, ILogger<WorkerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public string Get(string Id)
        {
            if (_context.EsecuzioniKiteRunners.Any(x => x.user == User.Identity.Name && x.id == Guid.Parse(Id)))
            {
                if (System.IO.File.Exists($"/home/kiterunner/kiterunner-1.0.2/results/{Id}.json"))
                {
                    return System.IO.File.ReadAllText($"/home/kiterunner/kiterunner-1.0.2/results/{Id}.json");
                }
            }
            return "NOT FOUND";
        }
    }
}
