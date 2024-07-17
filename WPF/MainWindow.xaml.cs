using System;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Data;
using System.Data.SQLite;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Polaczenie polaczenie = new Polaczenie();

        public bool Wish;
        public bool Powrot;
        public bool Zakazana;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            string Tytul = tytul.Text;
            string Nazwa = nazwa.Text;
            string Gatunek = gatunek.Text;
            string Ocena = ocena.Text;

            if (Tytul == "" ||  Nazwa == "" || Gatunek == "" || Ocena == "")
            {
                MessageBox.Show("Masz 1 lub więcej pustych pól!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Insert(Tytul, Nazwa, Gatunek, Ocena, Wish, Powrot, Zakazana);
        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            dodaj_gre.Visibility = Visibility.Visible;
            wszystkie_gry.Visibility = Visibility.Hidden;
            lista_zyczen.Visibility = Visibility.Hidden;
            lista_powrotu.Visibility = Visibility.Hidden;
            lista_zakazana.Visibility = Visibility.Hidden;

            Ribbon.IsMinimized = false;
        }

        private void RibbonButton_Click_1(object sender, RoutedEventArgs e)
        {
            dodaj_gre.Visibility = Visibility.Hidden;
            wszystkie_gry.Visibility = Visibility.Visible;
            lista_zyczen.Visibility = Visibility.Hidden;
            lista_powrotu.Visibility = Visibility.Hidden;
            lista_zakazana.Visibility = Visibility.Hidden;

            Ribbon.IsMinimized = false;

            SelectAll();
        }

        private void RibbonButton_Click_2(object sender, RoutedEventArgs e)
        {
            dodaj_gre.Visibility = Visibility.Hidden;
            wszystkie_gry.Visibility = Visibility.Hidden;
            lista_zyczen.Visibility = Visibility.Visible;
            lista_powrotu.Visibility = Visibility.Hidden;
            lista_zakazana.Visibility = Visibility.Hidden;

            Ribbon.IsMinimized = false;

            SelectListeZyczen();
        }

        private void RibbonButton_Click_3(object sender, RoutedEventArgs e)
        {
            dodaj_gre.Visibility = Visibility.Hidden;
            wszystkie_gry.Visibility = Visibility.Hidden;
            lista_zyczen.Visibility = Visibility.Hidden;
            lista_powrotu.Visibility = Visibility.Visible;
            lista_zakazana.Visibility = Visibility.Hidden;

            Ribbon.IsMinimized = false;

            SelectListePowrotu();
        }

        private void RibbonButton_Click_4(object sender, RoutedEventArgs e)
        {
            dodaj_gre.Visibility = Visibility.Hidden;
            wszystkie_gry.Visibility = Visibility.Hidden;
            lista_zyczen.Visibility = Visibility.Hidden;
            lista_powrotu.Visibility = Visibility.Hidden;
            lista_zakazana.Visibility = Visibility.Visible;

            Ribbon.IsMinimized = false;

            SelectListeZakazana();
        }

        private void ListaZyczen_Checked(object sender, RoutedEventArgs e)
        {
            Wish = true;
            lista_powrotu1.IsEnabled = false;
            lista_zakazana1.IsEnabled = false;
        }

        private void ListaZyczen_Unchecked(object sender, RoutedEventArgs e)
        {
            Wish = false;
            lista_powrotu1.IsEnabled = true;
            lista_zakazana1.IsEnabled = true;
        }

        private void ListaPowrotu_Checked(object sender, RoutedEventArgs e)
        {
            Powrot = true;
            lista_zyczen1.IsEnabled = false;
            lista_zakazana1.IsEnabled = false;
        }

        private void ListaPowrotu_Unchecked(object sender, RoutedEventArgs e)
        {
            Powrot = false;
            lista_zyczen1.IsEnabled = true;
            lista_zakazana1.IsEnabled = true;
        }

        private void ListaZakazana_Checked(object sender, RoutedEventArgs e)
        {
            Zakazana = true;
            lista_zyczen1.IsEnabled = false;
            lista_powrotu1.IsEnabled = false;
        }

        private void ListaZakazana_Unchecked(object sender, RoutedEventArgs e)
        {
            Zakazana = false;
            lista_zyczen1.IsEnabled = true;
            lista_powrotu1.IsEnabled = true;
        }

        public void Insert(string tytul, string firma, string gatunek, string ocena, bool wish, bool powrot, bool zakazana)
        {
            if (Polaczenie.conn.State != ConnectionState.Open)
                polaczenie.Polacz();

            try
            {
                if (wish)
                {
                    int ocena2 = Int32.Parse(ocena);
                    if (ocena2 > 10 || ocena2 < 1) 
                        MessageBox.Show("Wprowadzono niepoprawną ocenę!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else 
                    { 
                        polaczenie.zapytanie = string.Format("INSERT INTO wszystkie_gry ('tytul', 'firma', 'gatunek', 'ocena', 'lista_zyczen') VALUES ('{0}', '{1}', '{2}', '{3}', {4})", tytul, firma, gatunek, ocena2, wish);
                        Polaczenie.komenda = new SQLiteCommand(polaczenie.zapytanie, Polaczenie.conn);
                        int rezultat = Polaczenie.komenda.ExecuteNonQuery();

                        MessageBox.Show("Pomyślnie dodano rekord do bazy!", "Komunikat", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                else if (powrot)
                {
                    int ocena2 = Int32.Parse(ocena);
                    if (ocena2 > 10 || ocena2 < 1)
                        MessageBox.Show("Wprowadzono niepoprawną ocenę!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        polaczenie.zapytanie = string.Format("INSERT INTO wszystkie_gry ('tytul', 'firma', 'gatunek', 'ocena', 'lista_powrotu') VALUES ('{0}', '{1}', '{2}', '{3}', {4})", tytul, firma, gatunek, ocena2, powrot);
                        Polaczenie.komenda = new SQLiteCommand(polaczenie.zapytanie, Polaczenie.conn);
                        int rezultat = Polaczenie.komenda.ExecuteNonQuery();

                        MessageBox.Show("Pomyślnie dodano rekord do bazy!", "Komunikat", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else if (zakazana)
                {
                    int ocena2 = Int32.Parse(ocena);
                    if (ocena2 > 10 || ocena2 < 1)
                        MessageBox.Show("Wprowadzono niepoprawną ocenę!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        polaczenie.zapytanie = string.Format("INSERT INTO wszystkie_gry ('tytul', 'firma', 'gatunek', 'ocena', 'lista_zakazana') VALUES ('{0}', '{1}', '{2}', '{3}', {4})", tytul, firma, gatunek, ocena2, zakazana);
                        Polaczenie.komenda = new SQLiteCommand(polaczenie.zapytanie, Polaczenie.conn);
                        int rezultat = Polaczenie.komenda.ExecuteNonQuery();

                        MessageBox.Show("Pomyślnie dodano rekord do bazy!", "Komunikat", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
                else
                {
                    int ocena2 = Int32.Parse(ocena);
                    if (ocena2 > 10 || ocena2 < 1)
                        MessageBox.Show("Wprowadzono niepoprawną ocenę!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        polaczenie.zapytanie = string.Format("INSERT INTO wszystkie_gry ('tytul', 'firma', 'gatunek', 'ocena') VALUES ('{0}', '{1}', '{2}', '{3}')", tytul, firma, gatunek, ocena2);
                        Polaczenie.komenda = new SQLiteCommand(polaczenie.zapytanie, Polaczenie.conn);
                        int rezultat = Polaczenie.komenda.ExecuteNonQuery();

                        MessageBox.Show("Pomyślnie dodano rekord do bazy!", "Komunikat", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception blad)
            {
                MessageBox.Show(blad.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            polaczenie.Rozlacz();
        }

        public void SelectListeZakazana()
        {
            if (Polaczenie.conn.State != ConnectionState.Open)
                polaczenie.Polacz();

            try
            {
                polaczenie.zapytanie = string.Format("select id_gry, tytul, firma, gatunek, ocena from wszystkie_gry where lista_zakazana IS NOT NULL");
                Polaczenie.komenda = new SQLiteCommand(polaczenie.zapytanie, Polaczenie.conn);
                Polaczenie.komenda.ExecuteNonQuery();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(Polaczenie.komenda);
                DataTable dataTable = new DataTable("wszystkie_gry");
                adapter.Fill(dataTable);

                grid_lista_zakazana.ItemsSource = dataTable.DefaultView;
                adapter.Update(dataTable);
            }
            catch (Exception blad)
            {
                MessageBox.Show(blad.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            polaczenie.Rozlacz();
        }

        public void SelectListePowrotu()
        {
            if (Polaczenie.conn.State != ConnectionState.Open)
                polaczenie.Polacz();

            try
            {
                polaczenie.zapytanie = string.Format("select id_gry, tytul, firma, gatunek, ocena from wszystkie_gry where lista_powrotu IS NOT NULL");
                Polaczenie.komenda = new SQLiteCommand(polaczenie.zapytanie, Polaczenie.conn);
                Polaczenie.komenda.ExecuteNonQuery();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(Polaczenie.komenda);
                DataTable dataTable = new DataTable("wszystkie_gry");
                adapter.Fill(dataTable);

                grid_lista_powrotu.ItemsSource = dataTable.DefaultView;
                adapter.Update(dataTable);
            }
            catch (Exception blad)
            {
                MessageBox.Show(blad.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            

            polaczenie.Rozlacz();
        }

        public void SelectListeZyczen()
        {
            if (Polaczenie.conn.State != ConnectionState.Open)
                polaczenie.Polacz();

            try
            {
                polaczenie.zapytanie = string.Format("select id_gry, tytul, firma, gatunek, ocena from wszystkie_gry where lista_zyczen IS NOT NULL");
                Polaczenie.komenda = new SQLiteCommand(polaczenie.zapytanie, Polaczenie.conn);
                Polaczenie.komenda.ExecuteNonQuery();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(Polaczenie.komenda);
                DataTable dataTable = new DataTable("wszystkie_gry");
                adapter.Fill(dataTable);

                grid_lista_zyczen.ItemsSource = dataTable.DefaultView;
                adapter.Update(dataTable);
            }
            catch(Exception blad)
            {
                MessageBox.Show(blad.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            polaczenie.Rozlacz();
        }

        public void SelectAll()
        {
            if (Polaczenie.conn.State != ConnectionState.Open)
                polaczenie.Polacz();

            try
            {
                polaczenie.zapytanie = string.Format("select * from wszystkie_gry");
                Polaczenie.komenda = new SQLiteCommand(polaczenie.zapytanie, Polaczenie.conn);
                Polaczenie.komenda.ExecuteNonQuery();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(Polaczenie.komenda);
                DataTable dataTable = new DataTable("wszystkie_gry");
                adapter.Fill(dataTable);

                lista_wszystkie.ItemsSource = dataTable.DefaultView;
                adapter.Update(dataTable);
            }

            catch (Exception blad)
            {
                MessageBox.Show(blad.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }           

            polaczenie.Rozlacz();
        }
    }
}
