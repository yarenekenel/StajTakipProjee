using System.ComponentModel.DataAnnotations;

namespace StajTakipp.WebUI.Models
{
    public class MentorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur.")]
        public string Soyad { get; set; }

        public string? Unvan { get; set; }
        public int? KurumId { get; set; }

        public string AdSoyad => $"{Ad} {Soyad}";
    }
}
