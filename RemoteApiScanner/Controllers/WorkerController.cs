using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemoteApiScanner.Codice;
using RemoteApiScanner.Data;
using RemoteApiScanner.Models;

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

        [AllowAnonymous]
        [HttpGet]
        public string Get(string Id)
        {
            if (Guid.TryParse(Id, out Guid result) && _context.EsecuzioniKiteRunners.Any(x => x.id == Guid.Parse(Id)) && System.IO.File.Exists($"/home/kiterunner/kiterunner-1.0.2/results/{Id}.json"))
            {
                string jsonform = System.IO.File.ReadAllText($"/home/kiterunner/kiterunner-1.0.2/results/{Id}.json").Replace("\n", ",");
                return "[" + jsonform.Remove(jsonform.Length - 1) + "]";
            }
            return "Not Found";
        }
        // POST: EsecuzioniKiteRunners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async void Post(EsecuzioniKiteRunner esecuzioniKiteRunner)
        {
            if (!string.IsNullOrEmpty(esecuzioniKiteRunner.link))
            {
                esecuzioniKiteRunner.user = User.Identity.Name;
                esecuzioniKiteRunner.id = Guid.NewGuid();

                Task.Run(() => Execute.EseguiKiteRunner(esecuzioniKiteRunner));

                _context.Add(esecuzioniKiteRunner);
                await _context.SaveChangesAsync();
            }
        }
    }
}
