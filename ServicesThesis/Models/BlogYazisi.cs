using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesThesis.Models
{
    public class BlogYazisi
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string KategoriBlogAd { get; set; }
        public int KategoriBlogId { get; set; }
        public bool Durum { get; set; }
        public string KullaniciAdi { get; set; }
    }
}
