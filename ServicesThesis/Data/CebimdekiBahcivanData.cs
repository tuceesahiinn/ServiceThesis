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
                    string query = "select * from BlogYazisi ";

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