# RemoteApiScanner


Sito applicazione web RemoteApiScanner --> [RemoteApiScanner](https://ras.etau.it/)

Presentazione progetto --> [PresentazioneRAS.pdf](https://github.com/leonardopersici/RemoteApiScanner/files/11915127/PresentazioneRAS.pdf)

# Indice

- [Idea](#idea)
- [Kiterunner](#kiterunner)
    - [Introduzione](#introduzione)
    - [Funzionamento](#funzionamento)
    - [Parametri](#parametri)
        - [Wordlist (-w)]()
        - [Max connection per host (-x)]()
        - [Max parallel hosts (-j)]()
        - [Fails status codes (—fails-status-codes)]()
        - [Output (-o)]()
    - [Risultati](#risultati)
- [RemoteApiScanner](#remoteapiscanner)
    - [Architettura](#architettura)
    - [Server](#server)
    - [Client](#client)
    - [Workflow](#workflow)
- [Conclusioni](#conclusioni)

## Idea

Il progetto RemoteApiScanner ha come obiettivo principale quello di fornire agli sviluppatori uno strumento completo per l'analisi e la scansione delle API remote. Le API, che rappresentano l'interfaccia di programmazione delle applicazioni, svolgono un ruolo fondamentale nello sviluppo del software, e garantire la loro sicurezza e affidabilità è di estrema importanza per garantire il corretto funzionamento delle applicazioni. 

RemoteApiScanner si basa sul progetto open-source Kiterunner, che consente di effettuare la scansione e, se necessario, anche il bruteforcing delle API associate a un determinato indirizzo IP.

L'obiettivo di RemoteApiScanner è offrire agli utenti la possibilità di utilizzare Kiterunner senza la necessità di installarlo ed eseguirlo localmente sulla propria macchina. Questo approccio comporta diversi vantaggi, tra cui:

1. Accessibilità: Gli utenti possono utilizzare RemoteApiScanner tramite un'interfaccia web senza dover preoccuparsi di configurare e installare Kiterunner sul proprio sistema.
2. Semplicità: RemoteApiScanner semplifica il processo di utilizzo di Kiterunner, fornendo un'interfaccia intuitiva e user-friendly che consente agli utenti di eseguire le scansioni delle API in modo semplice e veloce.
3. Portabilità: Essendo una piattaforma basata su web, RemoteApiScanner può essere utilizzato da qualsiasi dispositivo con accesso a Internet, consentendo agli utenti di eseguire le scansioni delle API da qualsiasi luogo e in qualsiasi momento.

Con RemoteApiScanner, si vuole fornire agli sviluppatori uno strumento completo, accessibile e conveniente per l'analisi e la sicurezza delle API remote, semplificando il processo e migliorando l'efficienza nello sviluppo del software.

## Kiterunner

### Introduzione

Kiterunner è uno strumento open-source sviluppato da Assetnote e disponibile su GitHub all'indirizzo **[https://github.com/assetnote/kiterunner](https://github.com/assetnote/kiterunner)**. Fornisce una soluzione multipiattaforma da linea di comando per la scansione delle API, consentendo di rilevare e analizzare vulnerabilità e configurazioni errate presenti nei servizi esposti tramite API.

### Funzionamento

Kiterunner funziona attraverso una serie di passaggi che permettono la scansione delle API e l'analisi dei risultati. Una delle principali caratteristiche di Kiterunner è la scansione delle API, che viene effettuata utilizzando una lista di parole chiave (wordlist) e il protocollo HTTP. Attraverso questa scansione, lo strumento cerca di identificare gli endpoint API, i parametri, gli header e altri elementi rilevanti per l'analisi della sicurezza.

```makefile
kr scan <host-file> -w <routes.kite> -x <int> -j <int> --fail-status-codes <400,401,404,403,501,502,426,411> -o json > <host-result> 
```

### Parametri

La sintassi del comando di scansione di Kiterunner prevede:

- La specifica del file eseguibile `kr` .
- La specifica della tipologia di comando `scan`.
- La specifica dell’host della scansione `<host-file>`.

Oltre ai parametri obbligatori sopraindicati, Kiterunner offre una serie di ulteriori parametri opzionali che consentono di personalizzare la scansione delle API, tra cui:

- Wordlist (`-w <routes.kite>`): Kiterunner supporta l'utilizzo di wordlist per effettuare la scansione delle API. Le wordlist sono file di testo contenenti una lista di parole chiave o frasi che vengono utilizzate per generare richieste valide da inviare agli endpoint delle API. Kiterunner supporta file .kite di dimensioni variabile (small.kite / large.kite) a seconda delle esigenze dell'utente.
- Max connection per host (`-x <int>`): Consente di specificare il numero massimo di connessioni simultanee per host. Questo parametro influisce sulla velocità di scansione delle API, consentendo di regolare il carico generato durante il processo.
- Max parallel hosts (`-j <int>`): Consente di specificare il numero massimo di host paralleli che possono essere analizzati contemporaneamente. Questo parametro gestisce la distribuzione delle risorse e l'efficienza della scansione delle API.
- Fail status codes (`--fail-status-codes <[int]>`): Consente di specificare una selezione di codici di stato HTTP considerati importanti durante la scansione delle API. Se una risposta HTTP ricevuta durante la scansione corrisponde a uno dei codici di stato definiti, quella richiesta non verrà considerata.
- Output (`-o <host-result>`): Consente di specificare il percorso e il nome del file di output per i risultati della scansione delle API. Il formato predefinito di output è TXT, ma è anche possibile utilizzare il formato JSON, che fornisce una struttura dati più organizzata e leggibile.

### Risultati

Dopo aver completato la scansione con Kiterunner, vengono generati dei risultati che forniscono informazioni sull'analisi delle API. I risultati possono essere ottenuti in diversi formati, come testo semplice o JSON strutturato, a seconda delle preferenze dell'utente. I risultati della scansione includono informazioni dettagliate sugli endpoint API scoperti, i parametri, gli header e gli elementi rilevanti per la sicurezza. 

I risultati possono anche includere informazioni sul codice di stato HTTP restituito per ogni richiesta, il tempo di risposta, eventuali errori o avvisi generati durante la scansione e altre informazioni pertinenti per valutare la sicurezza delle API.

In generale, i risultati della scansione forniscono una panoramica delle vulnerabilità e delle configurazioni errate presenti nelle API analizzate, consentendo agli sviluppatori e agli amministratori di sistema di prendere le opportune misure correttive per migliorare la sicurezza delle loro applicazioni.

## RemoteApiScanner

### Architettura

RemoteApiScanner è un’applicazione web MVC creata tramite l’apposito framework “[ASP.NET Core MVC 7.0](https://learn.microsoft.com/it-it/aspnet/core/mvc/overview?view=aspnetcore-7.0)”. L’applicazione quindi viene gestita tramite tre gruppi principali di componenti: modelli, visualizzazioni e controller. Con questo schema le richieste dell'utente vengono indirizzate a un controller responsabile di interagire con il modello per eseguire le azioni dell'utente e/o recuperare i risultati delle query. Il controller sceglie la visualizzazione da mostrare all'utente e le fornisce i dati del modello necessari.

Qui un esempio di Controller per gestire la lista delle esecuzioni e i dettagli dei risultati di un esecuzione:

```csharp
[Authorize]
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
}
```

Di seguito, invece, il Model che gestisce le esecuzioni:

```csharp
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemoteApiScanner.Models
{
    [Table("EsecuzioniKiteRunner")]
    [Display(Name = "EsecuzioniKiteRunner")]
    public class EsecuzioniKiteRunner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        public string user { get; set; }
        [BindProperty]
        public string routes { get; set; }
        public string link { get; set; }
        public string statusCode { get; set; }
        public string? executionTime { get; set; }
        public DateTime executionDate { get; set; }

    }
}
```

Per ultimo, un esempio di view, dove vengono visualizzate tutte le esecuzioni richieste da un utente, siano esse completate o in corso d’opera:

```csharp
@model IEnumerable<RemoteApiScanner.Models.EsecuzioniKiteRunner>

@{
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<div class="container my-3">
    <div class="col-12">
        <div class="card text-center shadow">
            <div class="card-header text-bg-lightgray container">
                <div class="row">
                    <span class="col-4 offset-4 fs-5 fw-semibold align-self-center">Scanner List</span>
                    <span class="col-4 text-end">
                        <a class="btn btn-success" asp-action="Create"><i class="bi bi-plus-lg"></i> New scan</a>
                    </span>
                </div>
            </div>
            <div class="card-body">
                <div class="container">
                    <div class="table-responsive">
                        <table class="table table-sm align-middle table-scroll-x">
                            <thead>
                                <tr>
                                    <th scope="col">Status</th>
                                    <th scope="col">Date</th>
                                    <th scope="col">Link</th>
                                    <th scope="col">Dictionary</th>
                                    <th scope="col">Status Code</th>
                                    <th scope="col">Execution Time</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>
                            <tbody>
                                @foreach (var item in Model)
                                {
                                    <tr>
                                        @if (string.IsNullOrEmpty(item.executionTime))
                                        {
                                            <td>
                                                <div class="spinner-border spinner-border-sm text-secondary" role="status" style="animation-duration: 3s"></div>
                                            </td>
                                        }
                                        else
                                        {
                                            <td>
                                                <i class="bi bi-check-circle-fill text-success"></i>
                                            </td>
                                        }
                                        <td>
                                            @item.executionDate.ToString("dd/MM/yyyy")
                                        </td>
                                        <td>
                                            <a class="text-secondary" href="https://@Html.DisplayFor(modelItem => item.link)" target="_blank">@Html.DisplayFor(modelItem => item.link)</a>
                                        </td>
                                        <td>
                                            @Html.DisplayFor(modelItem => item.routes)
                                        </td>
                                        <td>
                                            @{
                                                var originalString = "400,401,403,404,405,426,501,502";
                                                var removeString = item.statusCode;

                                                var originalArray = originalString.Split(',');
                                                var removeArray = removeString.Split(',');

                                                var resultArray = originalArray.Where(item => !removeArray.Contains(item));
                                                var resultString = string.Join(",", resultArray);

                                                @Html.Raw(resultString)
                                            }
                                        </td>
                                        <td>
                                            @Html.DisplayFor(modelItem => item.executionTime)
                                        </td>
                                        <td>
                                            @if (string.IsNullOrEmpty(item.executionTime))
                                            {
                                                <a class="btn btn-outline-primary btn-sm border-0 disabled" asp-action="Details" asp-route-id="@item.id"><i class="bi bi-info-circle"></i></a>
                                                <a class="btn btn-outline-danger btn-sm border-0 disabled" asp-action="Delete" asp-route-id="@item.id"><i class="bi bi-trash3"></i></a>
                                            }
                                            else
                                            {
                                                <a class="btn btn-outline-primary btn-sm border-0" asp-action="Details" asp-route-id="@item.id"><i class="bi bi-info-circle"></i></a>
                                                <a class="btn btn-outline-danger btn-sm border-0" asp-action="Delete" asp-route-id="@item.id"><i class="bi bi-trash3"></i></a>
                                            }
                                        </td>
                                    </tr>
                                }
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
```

Per quanto riguarda le utenze, invece, sono gestiti all’interno del progetto tramite [Entity Framework](https://learn.microsoft.com/it-it/ef/), ossia un mapper moderno di relazione con oggetti che consente di creare un livello di accesso ai dati pulito, portabile e di alto livello con .NET (C#) in un'ampia gamma di database, tra cui database SQLite. Supporta le query LINQ, il rilevamento delle modifiche, gli aggiornamenti e le migrazioni dello schema.

L’applicazione viene pubblicata all’interno del server, di cui nella sezione successiva ci sono maggiori dettagli.

### Server

Il server utilizzato per RemoteApiScanner è un server linux con installato al suo interno Ubuntu 22.04.2.

All’interno del server sono 3 le componenti fondamentali per il funzionamento di RemoteApiScanner: la cartella Aspub di pubblicazione del portale web, la cartella di Kiterunner contente l’istallazione del tool e le varie risorse utili per l’esecuzione dello stesso e per permettere al portale di mostrare i risultati delle esecuzioni ed infine il servizio che permette di avviare il portale web fino ad un’interruzione manuale dello stesso.

All’interno di Aspub è presente tutto il progetto ASP.NET Core MVC compreso anche di database. Il database è un Sqlite che viene utilizzato sia per la gestione delle utenze che per la gestione delle esecuzioni richieste dagli utenti.

Per quanto riguarda Kiterunner invece, all’interno dell’apposita cartella è stata eseguita la procedura di installazione presente nella documentazione del tool, che prevede il building e la compilazione dei file contenenti le wordlist forniti sempre all’interno della documentazione se quest’ultimi sono in formato JSON. La documentazione però mette a disposizione anche i file delle wordlist .kite che sarebbero la versione compilata dei .json. All’interno dell’apposita cartella del server abbiamo importato le wordlist già compilate, sia per evitare la procedura di compilazione e sia per questioni di dimensioni (JSON 2.6GB, KITE 183MB). I file forniti sono routes-small e routes-large, che contengono rispettivamente 35MB e 183MB di routes possibili da testare sull’IP richiesto. Inoltre, abbiamo creato una versione routes-demo che consiste in un alleggerimento della versione small da utilizzare durante i test ed eventualmente durante una demo del progetto, così da ottenere dei risultati significativi in pochi secondi. All’interno della cartella di Kiterunner presente sul server sono anche salvati i risultati delle esecuzioni richieste dagli utenti. Nello specifico il nome del file corrisponde all’id dell’esecuzione stessa, così che siano nomi univoci. Quando l’esecuzione termina, il file viene allegato all’interno della mail per l’utente, previa verifica dell’esistenza del file stesso. La verifica dell’esistenza del file viene fatta anche quando un utente accede ai dettagli di un esecuzione all’interno dell’apposita pagina del portale dove viene parsato il file JSON e mostrato in una tabella.

### Client

Dal punto di vista del client, ogni singolo utente, previa autenticazione sul portale, avrà la possibilità di inserire il link di cui è interessato fare una scansione per rilevare i percorsi nascosti e le relative vulnerabilità. Oltre al link l’utente avrà la possibilità di selezionare il dizionario da cui prendere le routes da scannerizzare (con le opzioni demo-small-large) e gli status-code di risposta alle richieste che si vogliono considerare tra quelli possibili (400, 401, 403 404, 405, 426, 501, 502). Se l’utente non seleziona alcun parametro, vengono considerati di default il dizionario small e tutti gli status code selezionabili.

Tramite il pulsante di invio l’utente manderà la richiesta al server di eseguire il comando di Kiterunner con i parametri precedentemente selezionati.

Quando l’esecuzione sarà terminata l’utente verrà avvisato tramite una email contenente le indicazioni ed un link per visualizzare il risultato all’interno del portale e anche il file json con il risultato prodotto dall’esecuzione richiesta. 

### Funzionamento e workflow

### Registrazione
![Untitled](https://github.com/leonardopersici/RemoteApiScanner/assets/58106449/78af6b4b-b157-4646-a85e-9e2a7b7f1d61)

La fase di registrazione su RemoteApiScanner permette agli utenti di creare un account per accedere alle funzionalità dell'applicazione web. Durante la registrazione, agli utenti viene richiesto di fornire un indirizzo email valido e una password. Dopo aver inserito correttamente le credenziali e completato il processo di verifica, gli utenti saranno automaticamente reindirizzati alla schermata principale di RemoteApiScanner.

### Accesso
![Screenshot_2023-06-26_alle_17 16 22](https://github.com/leonardopersici/RemoteApiScanner/assets/58106449/6e914236-94fe-438a-93de-e318faca66e0)

La fase di accesso a RemoteApiScanner consente agli utenti registrati di accedere al proprio account e utilizzare le funzionalità dell'applicazione. Per effettuare l'accesso, gli utenti devono inserire le proprie credenziali, ovvero l'indirizzo email e la password associate al proprio account. Dopo aver fornito le credenziali corrette, gli utenti saranno autenticati e reindirizzati alla schermata principale di RemoteApiScanner.

### Ricerca
![Untitled 1](https://github.com/leonardopersici/RemoteApiScanner/assets/58106449/cea327ad-271e-43b5-950b-374cd572c7c3)

La sezione di ricerca di RemoteApiScanner offre agli utenti autenticati la possibilità di effettuare scansioni di indirizzi IP o endpoint specifici. Questa funzionalità è accessibile attraverso la barra di ricerca dedicata, dove gli utenti possono inserire l'indirizzo IP o l'endpoint che desiderano analizzare.

Di default, la ricerca utilizza la wordlist "small.kite" e include tutti i possibili status code (400, 401, 403, 404, 405, 426, 501, 502) come criteri di interesse. Tuttavia, è possibile personalizzare i parametri di ricerca cliccando sul pulsante "Filtro", che consente di modificare i parametri predefiniti e adottare parametri personalizzati in base alle esigenze dell'utente.

Una volta configurati i parametri di ricerca desiderati, gli utenti possono avviare la scansione cliccando sul pulsante di invio corrispondente. A questo punto, RemoteApiScanner avvierà la scansione dell'indirizzo IP o dell'endpoint specificato e fornirà i risultati dell'analisi.

### Lista
![Untitled 2](https://github.com/leonardopersici/RemoteApiScanner/assets/58106449/b21a3e91-9c58-4756-9893-f058a0e2e410)

La sezione lista consente di vedere tutte le scansioni effettuate precedentemente elencate in ordine cronologico decrescente e monitorare l’avanzamento delle scansioni in corso.

La tabella degli elenchi delle scansioni presenta una riga per ogni scansione effettuata e fornisce dettagli quali lo stato di avanzamento della scansione (Status), la data di esecuzione (Date), l'host di ricerca (Link), la wordlist utilizzata per la scansione (Dictionary), gli status code di interesse (Status Code) e il tempo di esecuzione (Execution Time).

All’estremità di ogni scansione vi sono due bottoni che consentono di accedere alla tabella dei risultati dell’analisi corrispondente e di eliminare la scansione. Cliccando sul primo pulsante, si potranno quindi visualizzare i dettagli completi dei risultati della scansione, mentre cliccando sul secondo pulsante si potrà eliminare la scansione dalla lista degli elenchi.

### Notifica
![Untitled 3](https://github.com/leonardopersici/RemoteApiScanner/assets/58106449/d399eb65-e86f-4d92-a600-595954dd637a)

Quando il server termina l’esecuzione del comando, l’utente dev’essere notificato, così che possa andare a vedere il risultato della scansione. Per fare questo viene utilizzata la mail che l’utente ha fornito in fase di registrazione. L’email contiene del testo che, oltre ad alcuni riferimenti all’esecuzione in questione, specifica come vedere il risultato all’interno della piattaforma, insieme ad un link diretto alla pagina del risultato dell’esecuzione in questione. Inoltre, come allegato viene inserito il file JSON contenente i risultati ottenuti dall’esecuzione.

Quando l’utente clicca sul link per vedere il risultato, se è già loggato all’interno del portale potrà anche poi tornare alla lista completa e continuare la normale navigazione nell’applicazione web, se invece al momento del click l’utente non è loggato, oltre al singolo risultato non avrà possibilità di navigare ulteriormente nell’applicazione web.

### Analisi
![Untitled 4](https://github.com/leonardopersici/RemoteApiScanner/assets/58106449/f6171aef-8413-43d0-81c4-f955e6d919d9)

La sezione risultati consente di vedere i risultati ottenuti dalla scansione effettuata  precedentemente visualizzando i dettagli dei singoli risultati.

La tabella dei risultati delle scansioni presenta una riga dedicata per ogni risultato di ricerca effettuato e fornisce dettagli quali la tipologia di metodo HTTP (Method), il link target corrispondente (Target), il percorso completo dei link target (Path), il codice di stato HTTP ritornato dalla richiesta (Status Code), la dimensione del contenuto della richiesta effettuata (Length) e l’orario in cui è stata effettuata la richiesta (Time).

## Conclusioni

In conclusione, quindi, il progetto RemoteApiScanner si presenta come uno strumento utile per la scansione e l'analisi delle API remote. Le sue funzionalità di scansione, rilevamento di vulnerabilità, analisi delle risposte e reportistica forniscono un supporto significativo agli sviluppatori e agli esperti di sicurezza per garantire la corretta configurazione e l'affidabilità delle API remote. Con un utilizzo appropriato, questo strumento può contribuire a migliorare la sicurezza delle applicazioni e proteggere i dati sensibili.

I vantaggi e i benefici che RemoteApiScanner porta ad un utente, rispetto all’utilizzo di Kiterunner possono essere riassunti in:

- Semplicità di utilizzo → rispetto a Kiterunner, non bisogna seguire alcuna documentazione per il processo di installazione del tool, ma basta registrarsi sul portale per poter effettuare le scansioni.
- Computazione remota → l’utilizzo di Kiterunner implica che la computazione sia a carico del dispositivo su cui viene installato il tool, invece tramite RemoteApiScanner non c’è necessità di avere una macchina dalle alte prestazioni per ottenere risultati migliori, in quanto la computazione è interamente spostata sul server remoto.
- Persistenza e concorrenza → chi utilizza Kiterunner, una volta lanciato il comando di scansione dal terminale, deve attendere che questo sia finito senza poter chiudere il processo e vedere i risultati. Tramite RemoteApiScanner invece, l’utente può inviare la richiesta e chiudere il portale, poiché verrà avvisato tramite email quando il risultato sarà terminato. L’importanza di questo vantaggio aumenta dal momento in cui alcune esecuzioni possono tenere occupato il processo (e quindi le risorse computazionali) anche per 10 ore. Inoltre, tramite RemoteApiScanner è possibile richiedere l’esecuzione di più scannerizzazioni in contemporanea, senza dover aspettare il termine di quella precedente.
- Verifica più immediata dei risultati → quando Kiterunner termina una scansione, fornisce il risultato in vari formati, TXT o JSON, ma in entrambi i casi può risultare complesso da leggere e porta ad una perdita di tempo nell’analizzarlo, sopratutto per chi è alle prime armi. RemoteApiScanner, invece, mostra il risultato della scansione sotto forma di tabella, così da agevolare la lettura e diminuire i tempi di analisi anche per i meno esperti.
- Portabilità su qualsiasi dispositivo → essendo un’applicazione web può essere utilizzata da qualsiasi dispositivo con accesso ad internet. Potenzialmente, quindi, potrebbe essere inviata un’esecuzione da uno smartphone, pratica che tramite Kiterunner sarebbe impossibile.
