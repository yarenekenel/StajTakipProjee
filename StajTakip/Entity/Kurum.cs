using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Core.Entity
{
    public class Kurum
    {
        public int Id { get; set; }
        public string KurumAdi { get; set; }
        public string? Adres { get; set; }
        public string? Telefon { get; set; }
        public string? Sektor { get; set; }
    }
}
