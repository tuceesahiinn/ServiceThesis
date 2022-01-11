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
        public IHttpActionResult BitkiOnerileri(string Il) => Json(CebimdekiBahcivanData.BitkiOnerileri(Il));
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
    }
}