using Microsoft.AspNetCore.Authorization;
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
            if (Guid.TryParse(Id, out Guid result) && _context.EsecuzioniKiteRunners.Any(x => x.user == User.Identity.Name && x.id == Guid.Parse(Id)) && System.IO.File.Exists($"/home/kiterunner/kiterunner-1.0.2/results/{Id}.json"))
            {
                return System.IO.File.ReadAllText($"/home/kiterunner/kiterunner-1.0.2/results/test.json");
            }
            return "Not Found";
        }
    }
}
