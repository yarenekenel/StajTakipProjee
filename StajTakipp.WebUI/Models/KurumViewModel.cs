using System.ComponentModel.DataAnnotations;

namespace StajTakipp.WebUI.Models
{
    public class KurumViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kurum adı zorunludur.")]
        public string KurumAdi { get; set; }

        public string? Adres { get; set; }
        public string? Telefon { get; set; }
        public string? Sektor { get; set; }
    }
}
