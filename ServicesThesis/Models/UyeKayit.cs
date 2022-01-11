using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesThesis.Models
{
    public class UyeKayit
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KullaniciAdi { get; set; }
        public bool Cinsiyet { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public int IlId { get; set; }
        public bool Durum { get; set; }   

    }
}