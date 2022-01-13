using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesThesis.Models
{
    public class BenimBahcem
    {
        public int Id { get; set; }
        public int BitkiId { get; set; }
        public string BitkiAd { get; set; }
        public bool Durum { get; set; }
        public string Notlar { get; set; }
        public string KullaniciAdi { get; set; }
    }
}