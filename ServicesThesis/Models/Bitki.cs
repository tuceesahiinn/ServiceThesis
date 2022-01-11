using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesThesis.Models
{
    public class Bitki
    {
        public int Id { get; set; }
        public string BitkiAd { get; set; }
        public string BitkiAciklama { get; set; }
        public string Fotograf { get; set; }
        public string BitkiKategoriAd { get; set; }
        public int BitkiKategoriId { get; set; }
        public bool Durum { get; set; }
    

    }
}