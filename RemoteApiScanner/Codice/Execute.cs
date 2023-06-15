﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using RemoteApiScanner.Data;
using RemoteApiScanner.Models;
using System.Diagnostics;
using System.Security.Authentication;

namespace RemoteApiScanner.Codice
{
    public class Execute
    {
        public static async Task EseguiKiteRunner(EsecuzioniKiteRunner Modello)
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
    }
}
