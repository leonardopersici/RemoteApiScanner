using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using RemoteApiScanner.Data;
using RemoteApiScanner.Models;
using System.Diagnostics;
using System.Security.Authentication;
using static System.Net.Mime.MediaTypeNames;

namespace RemoteApiScanner.Controllers
{
    public class EsecuzioniKiteRunnersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WorkerController> _logger;

        public EsecuzioniKiteRunnersController(ApplicationDbContext context, ILogger<WorkerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: EsecuzioniKiteRunners
        public async Task<IActionResult> Index()
        {
            return _context.EsecuzioniKiteRunners != null ?
                        View(await _context.EsecuzioniKiteRunners.Where(x => x.user == User.Identity.Name).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.EsecuzioniKiteRunners'  is null.");
        }

        // GET: EsecuzioniKiteRunners/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.EsecuzioniKiteRunners == null)
            {
                return NotFound();
            }

            var esecuzioniKiteRunner = await _context.EsecuzioniKiteRunners
                .FirstOrDefaultAsync(m => m.id == id);
            if (esecuzioniKiteRunner == null)
            {
                return NotFound();
            }

            return View(esecuzioniKiteRunner);
        }

        // GET: EsecuzioniKiteRunners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EsecuzioniKiteRunners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(EsecuzioniKiteRunner esecuzioniKiteRunner)
        {
            if (esecuzioniKiteRunner.link != null)
            {
                esecuzioniKiteRunner.user = User.Identity.Name;
                esecuzioniKiteRunner.id = Guid.NewGuid();

                Task.Run(() => EseguiKiteRunner(esecuzioniKiteRunner));

                _context.Add(esecuzioniKiteRunner);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Ok();
        }
        public async Task EseguiKiteRunner(EsecuzioniKiteRunner Modello)
        {
            Stopwatch sw = Stopwatch.StartNew();
#if DEBUG

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "C:\\Windows\\system32\\cmd.exe",
                WorkingDirectory = @"C:\\Users\\leo1-\\Desktop\\kiterunner\\dist",
                Arguments = $"/c kr scan --kitebuilder-full-scan host.txt -w routes-{Modello.routes}.kite -x 20 -j 100 -o json > {Modello.id}.json"
            };

            Process.Start(startInfo).WaitForExit();
            //Aspetto che il processo finisca
#else
            Console.WriteLine($"-c \"kr scan --kitebuilder-full-scan {Modello.link} -w routes/routes-{Modello.routes}.kite -x 20 -j 100 -o json > /home/kiterunner/kiterunner-1.0.2/results/{Modello.id}.json\"");
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "/bin/bash",
                WorkingDirectory = "/home/kiterunner/kiterunner-1.0.2",
                Arguments = $"-c \"kr scan --kitebuilder-full-scan {Modello.link} -w routes/routes-{Modello.routes}.kite -x 20 -j 100 -o json > /home/kiterunner/kiterunner-1.0.2/results/{Modello.id}.json\"",
            };
            Process.Start(startInfo).WaitForExit();
            
            //Aspetto che il processo finisca
#endif
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            //inserisco a database quanto tempo e' passato
            Modello.executionTime = elapsedTime;


            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
#if DEBUG
            .UseSqlite(@"DataSource=//192.168.3.144/sambashare/app.db;Cache=Shared")
#else
            .UseSqlite(@"DataSource=app.db;Cache=Shared")
#endif
    .Options;

            using var contextdb = new ApplicationDbContext(contextOptions);

            contextdb.Update(Modello);
            contextdb.SaveChangesAsync().Wait();



            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("RemoteApiScanner", "noreply@etau.it"));
            message.To.Add(new MailboxAddress(Modello.user, Modello.user));
            message.Subject = $"Risultato esecuzione KiteRunner {Modello.link}";

            var builder = new BodyBuilder();
            builder.HtmlBody = $"CIAO PIETRO + {elapsedTime}";
            if (System.IO.File.Exists($"/home/kiterunner/kiterunner-1.0.2/results/{Modello.id}.json"))
            {
                builder.Attachments.Add($"/home/kiterunner/kiterunner-1.0.2/results/{Modello.id}.json");
            }

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
                client.Connect("smtps.aruba.it", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate("noreply@etau.it", "VhS$a9cwAsVeh5b");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        // GET: EsecuzioniKiteRunners/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.EsecuzioniKiteRunners == null)
            {
                return NotFound();
            }

            var esecuzioniKiteRunner = await _context.EsecuzioniKiteRunners.FindAsync(id);
            if (esecuzioniKiteRunner == null)
            {
                return NotFound();
            }
            return View(esecuzioniKiteRunner);
        }

        // POST: EsecuzioniKiteRunners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EsecuzioniKiteRunner esecuzioniKiteRunner)
        {
            if (id != esecuzioniKiteRunner.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(esecuzioniKiteRunner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EsecuzioniKiteRunnerExists(esecuzioniKiteRunner.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(esecuzioniKiteRunner);
        }

        // GET: EsecuzioniKiteRunners/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.EsecuzioniKiteRunners == null)
            {
                return NotFound();
            }

            var esecuzioniKiteRunner = await _context.EsecuzioniKiteRunners
                .FirstOrDefaultAsync(m => m.id == id);
            if (esecuzioniKiteRunner == null)
            {
                return NotFound();
            }

            return View(esecuzioniKiteRunner);
        }

        // POST: EsecuzioniKiteRunners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.EsecuzioniKiteRunners == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EsecuzioniKiteRunners'  is null.");
            }
            var esecuzioniKiteRunner = await _context.EsecuzioniKiteRunners.FindAsync(id);
            if (esecuzioniKiteRunner != null)
            {
                _context.EsecuzioniKiteRunners.Remove(esecuzioniKiteRunner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EsecuzioniKiteRunnerExists(Guid id)
        {
            return (_context.EsecuzioniKiteRunners?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
