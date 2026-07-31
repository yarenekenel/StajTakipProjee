using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StajTakip.Core.Dto;

namespace StajTakip.Core.Dto
{
    public class LoginRequest
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
    }
}
