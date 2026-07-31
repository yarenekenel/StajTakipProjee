namespace StajTakip.Core.Dto
{
    // DİKKAT: Bu DTO sadece Repository <-> Service arasında kullanılır.
    // Sifre alanı (hashlenmiş) hiçbir zaman Controller'a veya dışarıya döndürülmemeli.
    public class KullaniciAuthDto
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string? AdSoyad { get; set; }
    }
}