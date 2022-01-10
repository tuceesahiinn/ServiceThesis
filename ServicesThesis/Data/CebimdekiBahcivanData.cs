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
                        string query = @"Insert into Uye (Ad,Soyad,KullaniciAdi,Cinsiyet,Telefon,Eposta,Sifre,IlId) " +
                        "values (@Ad,@Soyad,@KullaniciAdi,@Cinsiyet,@Telefon,@Eposta,@Sifre,@IlId )";

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