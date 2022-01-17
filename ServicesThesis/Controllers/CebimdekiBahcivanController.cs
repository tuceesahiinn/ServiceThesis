using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using ServicesThesis.Data;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Win32;
using ServicesThesis.Models;

namespace ServicesThesis.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CebimdekiBahcivanController : ApiController
    {
        [HttpGet]
        public IHttpActionResult IlGetir()
        {
            return Json(CebimdekiBahcivanData.IlGetir());
        }

        [HttpGet]
        public IHttpActionResult IlceGetir()
        {
            return Json(CebimdekiBahcivanData.IlceGetir());
        }

        [HttpGet]
        public IHttpActionResult BlogYazisiGetir()
        {
            return Json(CebimdekiBahcivanData.BlogYazisiGetir());
        }


        [HttpPost]
        public IHttpActionResult KayitOl([FromBody] UyeKayit uyeKayit)
        {
            string sonuc = CebimdekiBahcivanData.KayitOl(uyeKayit);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Üye kaydı başarılı." });

            else
                return Json(new { state = "NOK", content = "Böyle bir üye zaten var. Lütfen farklı bir kullanıcı adıyla kayıt olunuz." });
        }
        [HttpPost]
        public IHttpActionResult GirisYap([FromBody] UyeKayit uyeKayit)
        {
            string sonuc = CebimdekiBahcivanData.GirisYap(uyeKayit);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Sisteme giriş başarılı." });

            else
                return Json(new { state = "NOK", content = "Kullanıcı adı veya şifre hatalı. Tekrar deneyiniz." });
        }
        [HttpPost]
        public IHttpActionResult CikisYap([FromBody] UyeKayit uyeKayit)
        {
            string sonuc = CebimdekiBahcivanData.CikisYap(uyeKayit);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Sistemden çıkış başarılı." });

            else
                return Json(new { state = "NOK", content = "Sistemden çıkış yapılırken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpPost]
        public IHttpActionResult HesabiPasifeAl([FromBody] UyeKayit uyeKayit)
        {
            string sonuc = CebimdekiBahcivanData.HesabiPasifeAl(uyeKayit);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Hesap başarılı bir şekilde pasife alındı." });

            else
                return Json(new { state = "NOK", content = "Hesap pasife alınırken bir hata meydana geldi. Lütfen tekrar deneyiniz." });
        }
        [HttpPost]
        public IHttpActionResult BilgilerimiGuncelle([FromBody] UyeKayit uyeKayit)
        {
            string sonuc = CebimdekiBahcivanData.BilgilerimiGuncelle(uyeKayit);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Bilgileriniz başarılı bir şekilde güncellenmiştir." });

            else
                return Json(new { state = "NOK", content = "Bilgilerinizi değiştirirken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpPost]
        public IHttpActionResult HesapSil([FromBody] UyeKayit uyeKayit)
        {
            string sonuc = CebimdekiBahcivanData.HesapSil(uyeKayit);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Hesabınız başarılı bir şekilde silinmiştir." });

            else
                return Json(new { state = "NOK", content = "Hesabınızı silerken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpGet]
        public IHttpActionResult BitkiOnerileri([FromUri] BitkiOnerisi bitkiOnerisi)
        {
            List<BitkiOnerisi> bitkiOneriListesi = CebimdekiBahcivanData.BitkiOnerileri(bitkiOnerisi);
            if(bitkiOneriListesi.Count != 0)
            {
                return Json(new { state = "OK", content = bitkiOneriListesi });
            }
            else
            {
                return Json(new { state = "NOK", content = "Bitki önerileri getirilirken bir hata meydana geldi." });
            }
            

        }
        [HttpPost]
        public IHttpActionResult BitkiOnerisiEkle([FromBody] BitkiOnerisi bitkiOnerisi)
        {
          
            string sonuc = CebimdekiBahcivanData.BitkiOnerisiEkle(bitkiOnerisi);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Bitki önerisi başarılı bir şekilde eklendi." });

            else
                return Json(new { state = "NOK", content = "Bitki önerisi eklenirken bir hata meydana geldi. Lütfen tekrar deneyiniz." });
            
        }
        [HttpPost]
        public IHttpActionResult BitkiOnerisiSil([FromBody] BitkiOnerisi bitkiOnerisi)
        {

            string sonuc = CebimdekiBahcivanData.BitkiOnerisiSil(bitkiOnerisi);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Bitki önerisi başarılı bir şekilde silindi." });

            else
                return Json(new { state = "NOK", content = "Bitki önerisi silinirken bir hata meydana geldi. Lütfen tekrar deneyiniz." });

        }
        [HttpPost]
        public IHttpActionResult BitkiEkle([FromBody] Bitki bitki)
        {

            string sonuc = CebimdekiBahcivanData.BitkiEkle(bitki);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Bitki başarılı bir şekilde eklendi." });

            else
                return Json(new { state = "NOK", content = "Bitki eklenirken bir hata meydana geldi. Lütfen tekrar deneyiniz." });

        }
        [HttpPost]
        public IHttpActionResult BitkiBilgisiGuncelleme([FromBody] Bitki bitki)
        {
            string sonuc = CebimdekiBahcivanData.BitkiBilgisiGuncelleme(bitki);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Bitki bilgileri başarılı bir şekilde güncellenmiştir." });

            else
                return Json(new { state = "NOK", content = "Bitki bilgileri güncellenirken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpPost]
        public IHttpActionResult BitkiSil([FromBody] Bitki bitki)
        {
            string sonuc = CebimdekiBahcivanData.BitkiSil(bitki);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Bitki bilgileri başarılı bir şekilde silindi." });

            else
                return Json(new { state = "NOK", content = "Bitki bilgileri silinirken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpGet]
        public IHttpActionResult BitkiArama([FromBody] Bitki bitki)
        {
            return Json(CebimdekiBahcivanData.BitkiArama(bitki));
        }
        [HttpPost]
        public IHttpActionResult BitkiyiFavorilereEkleme([FromBody] Bitki bitki)
        {
            string sonuc = CebimdekiBahcivanData.BitkiyiFavorilereEkleme(bitki);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Bitki başarılı bir şekilde favorilere eklenmiştir." });

            else
                return Json(new { state = "NOK", content = "Bitki favorilere eklenirken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpGet]
        public IHttpActionResult FavorilereEklenenBitkiyiGoruntuleme([FromUri] Bitki bitki)
        {
            List<Bitki> favorilereEklenenBitkiListesi= CebimdekiBahcivanData.FavorilereEklenenBitkiyiGoruntuleme(bitki);
            if (favorilereEklenenBitkiListesi.Count != 0)
            {
                return Json(new { state = "OK", content = favorilereEklenenBitkiListesi });
            }
            else
            {
                return Json(new { state = "NOK", content = "Favorilere eklenen bitkiler listelenirken bir hata meydana geldi." });
            }
        }
        [HttpPost]
        public IHttpActionResult BitkiyiFavorilerdenSilme([FromBody] Bitki bitki)
        {
            string sonuc = CebimdekiBahcivanData.BitkiyiFavorilerdenSilme(bitki);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Bitki başarılı bir şekilde favorilerden silinmiştir." });

            else
                return Json(new { state = "NOK", content = "Bitki favorilerden silinirken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpPost]
        public IHttpActionResult BlogYazisiniFavorilereEkleme([FromBody] BlogYazisi blogYazisi)
        {
            string sonuc = CebimdekiBahcivanData.BlogYazisiniFavorilereEkleme(blogYazisi);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Blog yazısı başarılı bir şekilde favorilere eklenmiştir." });

            else
                return Json(new { state = "NOK", content = "Blog yazısı favorilere eklenirken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpGet]
        public IHttpActionResult FavorilereEklenenBlogYazisiniGoruntuleme([FromUri] BlogYazisi blogYazisi)
        {
            List<BlogYazisi> favorilereEklenenBlogYazisiListesi = CebimdekiBahcivanData.FavorilereEklenenBlogYazisiniGoruntuleme(blogYazisi);
            if (favorilereEklenenBlogYazisiListesi.Count != 0)
            {
                return Json(new { state = "OK", content = favorilereEklenenBlogYazisiListesi });
            }
            else
            {
                return Json(new { state = "NOK", content = "Favorilere eklenen blog yazıları listelenirken bir hata meydana geldi." });
            }
        }
        [HttpPost]
        public IHttpActionResult BlogYazisiniFavorilerdenSilme([FromBody] BlogYazisi blogYazisii)
        {
            string sonuc = CebimdekiBahcivanData.BlogYazisiniFavorilerdenSilme(blogYazisii);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Blog yazısı başarılı bir şekilde favorilerden silinmiştir." });

            else
                return Json(new { state = "NOK", content = "Blog yazısı favorilerden silinirken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpPost]
        public IHttpActionResult BenimBahcemeBitkiEkleme([FromBody] BenimBahcem benimBahcem)
        {
            string sonuc = CebimdekiBahcivanData.BenimBahcemeBitkiEkleme(benimBahcem);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Benim bahçeme bitki başarılı bir şekilde eklenmiştir." });

            else
                return Json(new { state = "NOK", content = "Bitki benim bahçeme eklenirken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpPost]
        public IHttpActionResult BenimBahcemdenBitkiSilme([FromBody] BenimBahcem benimBahcem)
        {
            string sonuc = CebimdekiBahcivanData.BenimBahcemdenBitkiSilme(benimBahcem);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Benim bahçemden bitki başarılı bir şekilde silinmiştir." });

            else
                return Json(new { state = "NOK", content = "Bitki benim bahçemden bitki silinirken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpPost]
        public IHttpActionResult BenimBahcemdenBitkiNotuGuncelleme([FromBody] BenimBahcem benimBahcem)
        {
            string sonuc = CebimdekiBahcivanData.BenimBahcemdenBitkiNotuGuncelleme(benimBahcem);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Benim bahçemdeki bitki notu başarılı bir şekilde güncellenmiştir." });

            else
                return Json(new { state = "NOK", content = "Benim bahçemdeki bitki notu güncellenirken bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpGet]
        public IHttpActionResult BitkiListele()
        {
            return Json(CebimdekiBahcivanData.BitkiListele());
        }
        [HttpGet]
        public IHttpActionResult KategoriListele()
        {
            return Json(CebimdekiBahcivanData.KategoriListele());
        }
        [HttpGet]
        public IHttpActionResult BenimBahcemBitkiListele([FromUri] BenimBahcem benimBahcem)
        {
            List<BenimBahcem> benimBahcemListesi = CebimdekiBahcivanData.BenimBahcemBitkiListele(benimBahcem);
            if (benimBahcemListesi.Count != 0)
            {
                return Json(new { state = "OK", content = benimBahcemListesi });
            }
            else
            {
                return Json(new { state = "NOK", content = "Benim bahçem bitkileri listelenirken bir hata meydana geldi." });
            }
        }
        [HttpPost]
        public IHttpActionResult SonKullaniciEkle([FromBody] UyeKayit uyeKayit)
        {
            string sonuc = CebimdekiBahcivanData.SonKullaniciEkle(uyeKayit);

            if (sonuc == "OK")
                return Json(new { state = "OK", content = "Son kullanıcı başarılı bir şekilde getirilmiştir." });

            else
                return Json(new { state = "NOK", content = "Bir hata meydana geldi. Tekrar deneyiniz." });
        }
        [HttpGet]
        public IHttpActionResult SonKullaniciGetir()
        {
            return Json(CebimdekiBahcivanData.SonKullaniciGetir());
        }
        [HttpGet]
        public IHttpActionResult KullaniciBilgileriGetir([FromUri] UyeKayit uyeKayit)
        {
            List<UyeKayit> uyeListesi = CebimdekiBahcivanData.KullaniciBilgileriGetir(uyeKayit);
            if (uyeListesi.Count != 0)
            {
                return Json(new { state = "OK", content = uyeListesi });
            }
            else
            {
                return Json(new { state = "NOK", content = "Uye bilgileri listelenirken bir hata meydana geldi." });
            }
        }
    }
}