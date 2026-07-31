using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Core.Entity
{
  //  Neden Entity'yi direkt kullanmıyoruz da ayrı Dto yazıyoruz?
  //  Çünkü Entity veritabanı tablosunun birebir yansımasıdır dışarıya (Api üzerinden internete) açtığında hem gereksiz alanları sızdırmış olursun hem de veritabanı yapını değiştirdiğinde dışarıdaki sözleşmeyi bozarsın. Dto, dışarıyla konuşmak için ayrı, kontrollü bir zarf görevi görür.
    public class Stajyer
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime DogumTarihi {get; set;}
        public string Okul {  get; set; }
        public string Bolum { get; set; }
        public DateTime BaslangicTarihi {get; set;}
        public DateTime? BitisTarihi {get; set;} //boş geçilebilir
        public bool AktifMi { get; set;}
        public int? KurumId { get; set; }
        public int? MentorId { get; set; }
    }
}
