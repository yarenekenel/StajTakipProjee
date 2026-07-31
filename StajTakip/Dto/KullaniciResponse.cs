using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Core.Dto
{
    public class KullaniciResponse
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string? AdSoyad { get; set; }
    }
}
