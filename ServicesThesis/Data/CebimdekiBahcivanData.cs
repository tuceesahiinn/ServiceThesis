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
                    string query = @"Update Uye Set Ad=@Ad,Soyad=@Soyad,KullaniciAdi=@KullaniciAdi,Cinsiyet=@Cinsiyet,Telefon=@Telefon, " +
                    "Eposta=@Eposta,Sifre=@Sifre,IlId=@IlId ";
  
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
        public static object BitkiOnerileri(string Il)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "select bo.Id,b.BitkiAd,b.BitkiAciklama,b.Fotograf,bk.Ad[KategoriAd],i.Il from BitkiOnerileri bo "+
                    "INNER JOIN Bitki b on b.Id = bo.BitkiId "+
                    "INNER JOIN BitkiKategori bk on bk.Id = b.BitkiKategoriId "+
                    "INNER JOIN Il i on i.Id = bo.IlId "+
                    "where b.Durum = 1 and bo.Durum = 1 and bk.Durum = 1 ";
                   
                    if (!string.IsNullOrEmpty(Il) && Il != "null")
                    {
                        query += " AND i.Il = @Il ";
                    }
                   
                    return new { state = "OK", content = conn.Query(query, new { Il }) };
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
        public static string BitkiOnerisiEkle(BitkiOnerisi bitkiOnerisi)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
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