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
        public string path { get; set; }
        public string command { get; set; }
        public string link { get; set; }

    }
}
