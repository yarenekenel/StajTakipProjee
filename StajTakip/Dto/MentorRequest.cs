using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajTakip.Core.Dto
{
    public class MentorRequest
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string? Unvan { get; set; }
        public int? KurumId { get; set; }
    }
}
