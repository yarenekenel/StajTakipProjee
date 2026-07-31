using System;

namespace StajTakip.Core.Dto
{
    // JOIN sorgusundan dönen, Kurum ve Mentör bilgisini hazır içeren listeleme DTO'su
    public class StajyerListDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Okul { get; set; }
        public string Bolum { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public bool AktifMi { get; set; }

        public int? KurumId { get; set; }
        public string? KurumAdi { get; set; }

        public int? MentorId { get; set; }
        public string? MentorAd { get; set; }
        public string? MentorSoyad { get; set; }
    }
}