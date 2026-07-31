using System.ComponentModel.DataAnnotations;

namespace StajTakipp.WebUI.Models
{
    public class StajyerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Doğum tarihi zorunludur")]
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }

        [Required(ErrorMessage = "Okul zorunludur")]
        public string Okul { get; set; }

        [Required(ErrorMessage = "Bölüm zorunludur")]
        public string Bolum { get; set; }

        [Required(ErrorMessage = "Başlangıç tarihi zorunludur")]
        [DataType(DataType.Date)]
        public DateTime BaslangicTarihi { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BitisTarihi { get; set; }

        public bool AktifMi { get; set; }
        public int? KurumId { get; set; }
        public int? MentorId { get; set; }
    }
}