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
        public string routes { get; set; }
        public string link { get; set; }
        public string statusCode { get; set; }
        public string? executionTime { get; set; }

    }
}
