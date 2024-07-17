using System;
using System.Data;
using System.Data.SQLite;

namespace WpfApp1
{
    public class Polaczenie
    {
        public static string[] paths = { @"D:\Studia\Sem 4\Programy\Programowanie\WPF", "baza.db" };
        public static SQLiteConnection conn = new SQLiteConnection(string.Format("Data Source={0}", System.IO.Path.Combine(paths)));
        public static SQLiteCommand komenda;
        public static SQLiteDataReader czytnik;
        public string zapytanie = "";

        public void Polacz()
        {
            conn.Open();

            if (conn.State == ConnectionState.Open)
                Console.WriteLine("Udało się połączyć z bazą.");
            else
                Console.WriteLine("Nie udało się połączyć z bazą.");
        }

        public void Rozlacz()
        {
            conn.Close();

            if (conn.State == ConnectionState.Open)
                Console.WriteLine("Nie udało się rozłączyć z bazą.");
            else
                Console.WriteLine("Udało się rozłączyć z bazą.");
        }
    }
}
