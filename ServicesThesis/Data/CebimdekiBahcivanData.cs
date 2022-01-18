using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Collections;
using System.Net;
using System.Configuration;
using Dapper;
using ServicesThesis.Models;

namespace ServicesThesis.Data
{
    public class CebimdekiBahcivanData
    {
       
        public static object IlGetir ()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query ="SELECT * from Il  ";

                    return new { state = "OK", content=conn.Query(query, new { })};
                }
                catch (Exception exp)
                {
                    return new { state = "NOK", content = $"Sistem Hatası!!!<br />Hata Mesajı: {exp.Message}<br />Ayrıntılar: {exp.StackTrace}" };
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static object IlceGetir()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * from Ilce  ";

                    return new { state = "OK", content = conn.Query(query, new { }) };
                }
                catch (Exception exp)
                {
                    return new { state = "NOK", content = $"Sistem Hatası!!!<br />Hata Mesajı: {exp.Message}<br />Ayrıntılar: {exp.StackTrace}" };
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static object BlogYazisiGetir()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "select bly.Id,bly.Baslik,bly.Aciklama,bk.Ad[BlogKategori] from BlogYazisi bly "+
                    "INNER JOIN BlogKategori bk on bly.KategoriBlogId = bk.Id where bly.Durum = 1 and bk.Durum = 1 ";

                    return new { state = "OK", content = conn.Query(query, new { }) };
                }
                catch (Exception exp)
                {
                    return new { state = "NOK", content = $"Sistem Hatası!!!<br />Hata Mesajı: {exp.Message}<br />Ayrıntılar: {exp.StackTrace}" };
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        public static string KayitOl(UyeKayit uyeKayit)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string kullaniciVarMi = "Select * from Uye where KullaniciAdi=@KullaniciAdi";

                    List<UyeKayit> uyeListesi = conn.Query<UyeKayit>(kullaniciVarMi, new { uyeKayit.KullaniciAdi }).ToList();

                    if (uyeListesi.Count < 1)
                    {
                        string query = @"Insert into Uye (Ad,Soyad,KullaniciAdi,Cinsiyet,Telefon,Eposta,Sifre,IlId,Durum) " +
                        "values (@Ad,@Soyad,@KullaniciAdi,@Cinsiyet,@Telefon,@Eposta,@Sifre,@IlId,1 )";

                        conn.Execute(query, uyeKayit);

                        return "OK";
                    }
                    else
                    {
                        return "NOK";
                    }

              
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        public static string GirisYap(UyeKayit uyeKayit)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string kullaniciVarMi = "Select * from Uye where KullaniciAdi=@KullaniciAdi and Sifre=@Sifre ";
                    
                    List<UyeKayit> uyeListesi = conn.Query<UyeKayit>(kullaniciVarMi, new { uyeKayit.KullaniciAdi,uyeKayit.Sifre }).ToList();

                    if (uyeListesi.Count >= 1)
                    {
                        string query = "Update Uye set GirisDurumu=1 where KullaniciAdi=@KullaniciAdi and Sifre=@Sifre ";
                        conn.Execute(query, new { uyeKayit.KullaniciAdi, uyeKayit.Sifre });

                        return "OK";
                    }
                    else
                    {
                        return "NOK";
                    }

          

                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        public static string CikisYap(UyeKayit uyeKayit)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "Update Uye set GirisDurumu=0 where KullaniciAdi=@KullaniciAdi ";
                    conn.Execute(query, new { uyeKayit.KullaniciAdi });

                    return "OK";

                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        public static string HesabiPasifeAl(UyeKayit uyeKayit)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "Update Uye set Durum=0 where KullaniciAdi=@KullaniciAdi ";

                    conn.Execute(query, uyeKayit );
                   
                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        public static string BilgilerimiGuncelle(UyeKayit uyeKayit)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"Update Uye Set Sifre=@Sifre,IlId=@IlId where KullaniciAdi=@KullaniciAdi ";
  
                    conn.Execute(query, uyeKayit);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        public static string HesapSil(UyeKayit uyeKayit)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string kullaniciVarMi = "Select * from Uye where KullaniciAdi=@KullaniciAdi";

                    List<UyeKayit> uyeListesi = conn.Query<UyeKayit>(kullaniciVarMi, new { uyeKayit.KullaniciAdi }).ToList();

                    if(uyeListesi.Count != 0)
                    {
                        conn.Execute("Delete from Uye where Id=@Id ", new { uyeKayit.Id });


                        return "OK";
                    }
                    else
                    {
                        return "NOK";
                    }
                    
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        public static List<BitkiOnerisi> BitkiOnerileri(BitkiOnerisi bitkiOnerisi1)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<BitkiOnerisi> bitkiOneriListesi = new List<BitkiOnerisi>();
                try
                {
                    conn.Open();
                    string query = "select bo.Id,b.BitkiAd,b.BitkiAciklama,b.Fotograf,bk.Ad,i.Il from BitkiOnerileri bo " +
                    " INNER JOIN Bitki b on b.Id = bo.BitkiId " +
                    " INNER JOIN BitkiKategori bk on bk.Id = b.BitkiKategoriId " +
                    " INNER JOIN Il i on i.Id = bo.IlId " +
                    " where b.Durum = 1 and bo.Durum = 1 and bk.Durum = 1 "+
                     " AND i.Id = (select IlId from Uye where KullaniciAdi = @KullaniciAdi)";
                    bitkiOneriListesi = conn.Query<BitkiOnerisi>(query, bitkiOnerisi1).ToList();


                    return bitkiOneriListesi;
                }
                catch (Exception exp)
                {
                    throw new Exception("Bitki önerileri listelenirken bir hata meydana geldi.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BitkiOnerisiEkle(BitkiOnerisi bitkiOnerisi)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO BitkiOnerileri (Id,BitkiId,IlId,Durum) " +
                    "VALUES((SELECT COUNT(ID) FROM BitkiOnerileri) + 1,(SELECT Id FROM Bitki b WHERE b.BitkiAd =@BitkiAd), " +
                    "(SELECT Id FROM Il WHERE Il =@Il),1)";


                    conn.Execute(query, bitkiOnerisi);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BitkiOnerisiSil(BitkiOnerisi bitkiOnerisi)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string bitkiOnerisiVarMi = "Select * from BitkiOnerileri where Id=@Id";

                    List<BitkiOnerisi> oneriListesi = conn.Query<BitkiOnerisi>(bitkiOnerisiVarMi, new { bitkiOnerisi.Id }).ToList();

                    if (oneriListesi.Count != 0)
                    {
                        conn.Execute("delete BitkiOnerileri where Id=@Id ", new { bitkiOnerisi.Id });


                        return "OK";
                    }
                    else
                    {
                        return "NOK";
                    }
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BitkiEkle(Bitki bitki)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Bitki (BitkiAd,BitkiAciklama,Fotograf,BitkiKategoriId,Durum) "+
                    "VALUES(@BitkiAd, @BitkiAciklama, NULL, (SELECT bk.Id FROM BitkiKategori bk WHERE bk.Ad = @BitkiKategoriAd),1)";


                    conn.Execute(query, bitki);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BitkiBilgisiGuncelleme(Bitki bitki)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Bitki SET BitkiAd=@BitkiAd,BitkiAciklama=@BitkiAciklama,Fotograf=@Fotograf, "+
                    " BitkiKategoriId=(SELECT bk.Id FROM BitkiKategori bk WHERE bk.Ad = @BitkiKategoriAd),Durum=1 WHERE Id=@Id";


                    conn.Execute(query, bitki);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BitkiSil(Bitki bitki)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "Delete Bitki where Id=@Id";


                    conn.Execute(query, bitki);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static object BitkiArama(Bitki bitki)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "select b.BitkiAd,b.BitkiAciklama,bk.Ad from bitki b "+
                    "INNER JOIN BitkiKategori bk on bk.Id = b.BitkiKategoriId ";

                    if (!string.IsNullOrEmpty(bitki.BitkiAd) && bitki.BitkiAd != "null")
                    {
                        query += " AND b.BitkiAd like '%'+ @BitkiAd +'%' ";
                    }

                    return new { state = "OK", content = conn.Query(query, new { BitkiAd = bitki.BitkiAd }) };
                }
                catch (Exception exp)
                {
                    return new { state = "NOK", content = $"Sistem Hatası!!!<br />Hata Mesajı: {exp.Message}<br />Ayrıntılar: {exp.StackTrace}" };
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BitkiyiFavorilereEkleme(Bitki bitki)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO FavorilereEklenenBitki (BitkiId,UyeId) "+
                    "VALUES((SELECT Id FROM Bitki WHERE BitkiAd = @BitkiAd and Durum = 1), "+
                    "(SELECT Id from Uye where KullaniciAdi = @KullaniciAdi and Durum = 1 ))";


                    conn.Execute(query, bitki);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static List<Bitki> FavorilereEklenenBitkiyiGoruntuleme(Bitki bitki1)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Bitki> favorilereEklenenBitkiListesi = new List<Bitki>();
                try
                {
                    conn.Open();
                    string query = "select b.BitkiAd,b.BitkiAciklama,b.Fotograf,bk.Ad from FavorilereEklenenBitki feb "+
                    "INNER JOIN Bitki b on b.Id = feb.BitkiId "+
                    "INNER JOIN BitkiKategori bk on bk.Id = b.BitkiKategoriId "+
                    "where feb.UyeId = (select Id from Uye where KullaniciAdi = @KullaniciAdi ) and b.Durum = 1 and bk.Durum = 1";

                    favorilereEklenenBitkiListesi = conn.Query<Bitki>(query, bitki1).ToList();

                    return favorilereEklenenBitkiListesi;
                }
                catch (Exception exp)
                {
                    throw new Exception("Favorilere eklenen bitkiler listelenirken bir hata meydana geldi.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BitkiyiFavorilerdenSilme(Bitki bitki)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "Delete FavorilereEklenenBitki where UyeId=(select Id from Uye where KullaniciAdi=@KullaniciAdi )"+
                    " AND BitkiId=(select Id from Bitki where BitkiAd=@BitkiAd)";


                    conn.Execute(query, bitki);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BlogYazisiniFavorilereEkleme(BlogYazisi blogYazisi)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO FavorilereEklenenBlogYazi (BlogYazisiId,UyeId) " +
                    "VALUES((SELECT Id FROM BlogYazisi WHERE Baslik = @Baslik and Durum = 1), " +
                    " (SELECT Id from Uye where KullaniciAdi = @KullaniciAdi and Durum = 1 ))";


                    conn.Execute(query, blogYazisi);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static List<BlogYazisi> FavorilereEklenenBlogYazisiniGoruntuleme(BlogYazisi blogYazisi1)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<BlogYazisi> blogYazisiListesi = new List<BlogYazisi>();
                try
                {
                    conn.Open();
                    string query = "select bly.Baslik,bly.Aciklama,bk.Ad from FavorilereEklenenBlogYazi feby "+
                    " INNER JOIN BlogYazisi bly on feby.BlogYazisiId = bly.Id "+
                    " INNER JOIN BlogKategori bk on bk.Id = bly.KategoriBlogId "+
                    " where feby.UyeId = (select Id from Uye where KullaniciAdi = @KullaniciAdi ) and bly.Durum = 1 and bk.Durum = 1";

                    blogYazisiListesi = conn.Query<BlogYazisi>(query, blogYazisi1).ToList();

                    return blogYazisiListesi;
                }
                catch (Exception exp)
                {
                    throw new Exception("Favorilere eklenen blog yazıları listelenirken bir hata meydana geldi.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BlogYazisiniFavorilerdenSilme(BlogYazisi blogYazisi)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "Delete FavorilereEklenenBlogYazi where UyeId=(select Id from Uye where KullaniciAdi=@KullaniciAdi )" +
                    " AND BlogYazisiId=(select Id from BlogYazisi where Baslik=@Baslik)";


                    conn.Execute(query, blogYazisi);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BenimBahcemeBitkiEkleme(BenimBahcem benimBahcem)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();     
                       
                    string query = "INSERT INTO BenimBahcem (BitkiAd,Notlar,UyeId) "+
                    "VALUES(@BitkiAd,@Notlar,(select Id from Uye where KullaniciAdi = @KullaniciAdi))";


                    conn.Execute(query, benimBahcem);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BenimBahcemdenBitkiSilme(BenimBahcem benimBahcem)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "delete BenimBahcem where BitkiAd=@BitkiAd "+
                    " and UyeId=(select Id from Uye where KullaniciAdi=@KullaniciAdi) ";


                    conn.Execute(query, benimBahcem);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string BenimBahcemdenBitkiNotuGuncelleme(BenimBahcem benimBahcem)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "Update BenimBahcem set Notlar=@Notlar where BitkiAd=@BitkiAd  "+
                    "and UyeId = (select Id from Uye where KullaniciAdi = @KullaniciAdi)  ";


                    conn.Execute(query, benimBahcem);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static object BitkiListele()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "select b.Id,b.BitkiAd,b.BitkiAciklama,b.Fotograf,bk.Ad from Bitki b "+
                    " INNER JOIN BitkiKategori bk on bk.Id = b.BitkiKategoriId where b.Durum = 1 and bk.Durum = 1 ";


                    return new { state = "OK", content = conn.Query(query, new { }) };
                }
                catch (Exception exp)
                {
                    return new { state = "NOK", content = $"Sistem Hatası!!!<br />Hata Mesajı: {exp.Message}<br />Ayrıntılar: {exp.StackTrace}" };
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static object KategoriListele()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "select bk.Ad from  BitkiKategori bk ";


                    return new { state = "OK", content = conn.Query(query, new { }) };
                }
                catch (Exception exp)
                {
                    return new { state = "NOK", content = $"Sistem Hatası!!!<br />Hata Mesajı: {exp.Message}<br />Ayrıntılar: {exp.StackTrace}" };
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static List<BenimBahcem> BenimBahcemBitkiListele(BenimBahcem benimBahcem1)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<BenimBahcem> benimBahcemListesi = new List<BenimBahcem>();
                try
                {
                    conn.Open();
                    string query = "select bb.BitkiAd,bb.Notlar from BenimBahcem bb "+
                    " where bb.UyeId = (select Id from Uye where KullaniciAdi = @KullaniciAdi) ";
                    benimBahcemListesi = conn.Query<BenimBahcem>(query, benimBahcem1).ToList();

                    return benimBahcemListesi;
                }
                catch (Exception exp)
                {
                    throw new Exception("Benim bahçem bitkileri listelenirken bir hata meydana geldi.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string SonKullaniciEkle(UyeKayit uyeKayit)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO VeritabaniLoglari (KullaniciAdi) VALUES (@KullaniciAdi) ";

                    conn.Execute(query, uyeKayit);

                    return "OK";
                }
                catch (Exception exp)
                {
                    return exp.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static object SonKullaniciGetir()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "select TOP 1 KullaniciAdi from  VeritabaniLoglari  order by Id DESC ";


                    return new { state = "OK", content = conn.Query(query, new { }) };
                }
                catch (Exception exp)
                {
                    return new { state = "NOK", content = $"Sistem Hatası!!!<br />Hata Mesajı: {exp.Message}<br />Ayrıntılar: {exp.StackTrace}" };
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static List<UyeKayit> KullaniciBilgileriGetir(UyeKayit uyeKayit1)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<UyeKayit> uyeListesi = new List<UyeKayit>();
                try
                {
                    conn.Open();
                    string query = "select u.Ad,u.Soyad,u.Telefon,u.Eposta,u.Sifre,u.IlId from Uye u where u.KullaniciAdi=@KullaniciAdi ";

                    uyeListesi = conn.Query<UyeKayit>(query, uyeKayit1).ToList();

                    return uyeListesi;
                }
                catch (Exception exp)
                {
                    throw new Exception("Uye bilgileri listelenirken bir hata meydana geldi.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }

   

    internal class NewClass
    {
        public string State { get; }
        public object Content { get; }

        public NewClass(string state, object content)
        {
            State = state;
            Content = content;
        }

        public override bool Equals(object obj)
        {
            return obj is NewClass other &&
                   State == other.State &&
                   EqualityComparer<object>.Default.Equals(Content, other.Content);
        }

        public override int GetHashCode()
        {
            int hashCode = 1925302632;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(State);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Content);
            return hashCode;
        }
    }
}