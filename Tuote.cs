using System.ComponentModel.DataAnnotations;

namespace Varastotietokanta_harjoitus
{
    class Tuote
    {
        [Key]
        public int? ID { get; set; }
        public string? Tuotenimi { get; set; }
        public int Hinta { get; set; }
        public int VarastoSaldo { get; set; }
    }
}
