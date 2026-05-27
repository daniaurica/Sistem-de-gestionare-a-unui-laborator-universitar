using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text.Json;
using Model.Entitati;
using Model.Enumerari;
using System.Windows.Media;

namespace SistemGestionareLaboratorWPF
{
    public partial class MainWindow : Window
    {
        private List<Echipament> echipamente = new List<Echipament>();
        private List<string> imprumuturi = new List<string>();

        private string fisierEchipamente = "echipamente.json";
        private string fisierImprumuturi = "imprumuturi.json";

        public MainWindow()
        {
            InitializeComponent();

            cmbTip.SelectedIndex = 0;

            IncarcaDate();

            ActualizeazaLista();
            ActualizeazaComboEchipamente();

            lstImprumuturi.ItemsSource = imprumuturi;
        }

        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            lblDenumire.Foreground = Brushes.Black;
            lblNumarTotal.Foreground = Brushes.Black;

            bool dateValide = true;

            if (txtDenumire.Text.Trim() == "")
            {
                lblDenumire.Foreground = Brushes.Red;
                dateValide = false;
            }

            int numarTotal;

            if (!int.TryParse(txtNumarTotal.Text, out numarTotal) || numarTotal <= 0)
            {
                lblNumarTotal.Foreground = Brushes.Red;
                dateValide = false;
            }

            if (!dateValide)
            {
                txtRezultat.Text = "Completați corect câmpurile marcate cu roșu.";
                return;
            }

            Echipament echipament = new Echipament
            {
                Id = echipamente.Count + 1,
                Denumire = txtDenumire.Text,
                NumarTotal = numarTotal,
                NumarDisponibil = numarTotal,
                Tip = GetTipEchipament(),
                Optiuni = GetOptiuni()
            };

            echipamente.Add(echipament);

            ActualizeazaLista();
            ActualizeazaComboEchipamente();
            SalveazaDate();
            ReseteazaCampuriEchipament();

            txtRezultat.Text = "Echipament adăugat.";
        }

        private void BtnActualizeaza_Click(object sender, RoutedEventArgs e)
        {
            if (lstEchipamente.SelectedItem == null)
            {
                txtRezultat.Text = "Selectați un echipament.";
                return;
            }

            Echipament echipament = (Echipament)lstEchipamente.SelectedItem;

            int numarTotal;

            if (!int.TryParse(txtNumarTotal.Text, out numarTotal))
            {
                txtRezultat.Text = "Introduceți un număr valid.";
                return;
            }

            echipament.Denumire = txtDenumire.Text;
            echipament.NumarTotal = numarTotal;
            echipament.Tip = GetTipEchipament();
            echipament.Optiuni = GetOptiuni();

            ActualizeazaLista();
            ActualizeazaComboEchipamente();
            SalveazaDate();

            txtRezultat.Text = "Echipament actualizat.";
        }

        private void BtnCauta_Click(object sender, RoutedEventArgs e)
        {
            string text = txtCautare.Text.ToLower();

            Echipament echipamentGasit = echipamente
                .FirstOrDefault(e => e.Denumire.ToLower().Contains(text));

            if (echipamentGasit == null)
            {
                txtRezultat.Text = "Nu s-a găsit niciun echipament.";
                return;
            }

            txtRezultat.Text =
                "ID: " + echipamentGasit.Id +
                "\nDenumire: " + echipamentGasit.Denumire +
                "\nTip: " + echipamentGasit.Tip +
                "\nNumăr total: " + echipamentGasit.NumarTotal +
                "\nNumăr disponibil: " + echipamentGasit.NumarDisponibil +
                "\nOpțiuni: " + echipamentGasit.Optiuni;
        }

        private void BtnCautaStudent_Click(object sender, RoutedEventArgs e)
        {
            string numeCautat = txtCautareStudent.Text.Trim().ToLower();

            if (numeCautat == "")
            {
                txtRezultatStudent.Text = "Introduceți numele studentului.";
                return;
            }

            List<string> rezultate = imprumuturi
                .Where(i => i.ToLower().Contains(numeCautat))
                .ToList();

            if (rezultate.Count == 0)
            {
                txtRezultatStudent.Text = "Studentul nu are împrumuturi active.";
                return;
            }

            txtRezultatStudent.Text = string.Join("\n", rezultate);
        }

        private void BtnAdaugaImprumut_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtNumeImprumut.Text.Trim();
            string prenume = txtPrenumeImprumut.Text.Trim();

            if (nume == "" || prenume == "")
            {
                txtRezultatImprumut.Text = "Completați numele și prenumele studentului.";
                return;
            }

            if (cmbSpecializareImprumut.SelectedItem == null)
            {
                txtRezultatImprumut.Text = "Selectați specializarea.";
                return;
            }

            if (cmbEchipamentImprumut.SelectedItem == null)
            {
                txtRezultatImprumut.Text = "Selectați echipamentul.";
                return;
            }

            if (dpDataStart.SelectedDate == null || dpDataReturnare.SelectedDate == null)
            {
                txtRezultatImprumut.Text = "Selectați datele împrumutului.";
                return;
            }

            string specializare = ((ComboBoxItem)cmbSpecializareImprumut.SelectedItem).Content.ToString();
            Echipament echipament = (Echipament)cmbEchipamentImprumut.SelectedItem;

            if (echipament.NumarDisponibil <= 0)
            {
                txtRezultatImprumut.Text = "Nu există exemplare disponibile pentru acest echipament.";
                return;
            }

            echipament.NumarDisponibil--;

            string imprumut =
                nume + " " + prenume +
                " - " + specializare +
                " a împrumutat: " + echipament.Denumire +
                " | Data: " + dpDataStart.SelectedDate.Value.ToShortDateString() +
                " | Returnare: " + dpDataReturnare.SelectedDate.Value.ToShortDateString();

            imprumuturi.Add(imprumut);

            lstImprumuturi.ItemsSource = null;
            lstImprumuturi.ItemsSource = imprumuturi;

            ActualizeazaLista();
            ActualizeazaComboEchipamente();
            SalveazaDate();
            ReseteazaCampuriImprumut();

            txtRezultatImprumut.Text = "Împrumut adăugat cu succes.";
        }

        private void lstEchipamente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstEchipamente.SelectedItem == null)
                return;

            Echipament echipament = (Echipament)lstEchipamente.SelectedItem;

            txtDenumire.Text = echipament.Denumire;
            txtNumarTotal.Text = echipament.NumarTotal.ToString();

            SeteazaTip(echipament.Tip);

            cbWireless.IsChecked = echipament.Optiuni.HasFlag(OptiuniEchipament.Wireless);
            cbUSB.IsChecked = echipament.Optiuni.HasFlag(OptiuniEchipament.USB);
            cbBluetooth.IsChecked = echipament.Optiuni.HasFlag(OptiuniEchipament.Bluetooth);
            cbHDMI.IsChecked = echipament.Optiuni.HasFlag(OptiuniEchipament.HDMI);
        }

        private void ActualizeazaLista()
        {
            lstEchipamente.ItemsSource = null;
            lstEchipamente.ItemsSource = echipamente;
            lstEchipamente.DisplayMemberPath = "Denumire";
        }

        private void ActualizeazaComboEchipamente()
        {
            cmbEchipamentImprumut.ItemsSource = null;
            cmbEchipamentImprumut.ItemsSource = echipamente;
            cmbEchipamentImprumut.DisplayMemberPath = "Denumire";
        }

        private void SalveazaDate()
        {
            string jsonEchipamente = JsonSerializer.Serialize(echipamente);
            File.WriteAllText(fisierEchipamente, jsonEchipamente);

            string jsonImprumuturi = JsonSerializer.Serialize(imprumuturi);
            File.WriteAllText(fisierImprumuturi, jsonImprumuturi);
        }

        private void IncarcaDate()
        {
            if (File.Exists(fisierEchipamente))
            {
                string jsonEchipamente = File.ReadAllText(fisierEchipamente);
                echipamente = JsonSerializer.Deserialize<List<Echipament>>(jsonEchipamente);
            }

            if (File.Exists(fisierImprumuturi))
            {
                string jsonImprumuturi = File.ReadAllText(fisierImprumuturi);
                imprumuturi = JsonSerializer.Deserialize<List<string>>(jsonImprumuturi);
            }

            if (echipamente == null)
                echipamente = new List<Echipament>();

            if (imprumuturi == null)
                imprumuturi = new List<string>();
        }

        private TipEchipament GetTipEchipament()
        {
            ComboBoxItem item = (ComboBoxItem)cmbTip.SelectedItem;
            string tip = item.Content.ToString();

            switch (tip)
            {
                case "Mouse":
                    return TipEchipament.Mouse;

                case "Tastatura":
                    return TipEchipament.Tastatura;

                case "Monitor":
                    return TipEchipament.Monitor;

                default:
                    return TipEchipament.Laptop;
            }
        }

        private void SeteazaTip(TipEchipament tip)
        {
            switch (tip)
            {
                case TipEchipament.Mouse:
                    cmbTip.SelectedIndex = 1;
                    break;

                case TipEchipament.Tastatura:
                    cmbTip.SelectedIndex = 2;
                    break;

                case TipEchipament.Monitor:
                    cmbTip.SelectedIndex = 3;
                    break;

                default:
                    cmbTip.SelectedIndex = 0;
                    break;
            }
        }

        private OptiuniEchipament GetOptiuni()
        {
            OptiuniEchipament optiuni = OptiuniEchipament.Nimic;

            if (cbWireless.IsChecked == true)
                optiuni |= OptiuniEchipament.Wireless;

            if (cbUSB.IsChecked == true)
                optiuni |= OptiuniEchipament.USB;

            if (cbBluetooth.IsChecked == true)
                optiuni |= OptiuniEchipament.Bluetooth;

            if (cbHDMI.IsChecked == true)
                optiuni |= OptiuniEchipament.HDMI;

            return optiuni;
        }

        private void ReseteazaCampuriEchipament()
        {
            txtDenumire.Clear();
            txtNumarTotal.Clear();

            cmbTip.SelectedIndex = 0;

            cbWireless.IsChecked = false;
            cbUSB.IsChecked = false;
            cbBluetooth.IsChecked = false;
            cbHDMI.IsChecked = false;
        }

        private void ReseteazaCampuriImprumut()
        {
            txtNumeImprumut.Clear();
            txtPrenumeImprumut.Clear();

            cmbSpecializareImprumut.SelectedItem = null;
            cmbEchipamentImprumut.SelectedItem = null;

            dpDataStart.SelectedDate = System.DateTime.Now;
            dpDataReturnare.SelectedDate = System.DateTime.Now;
        }

        private void BtnReturneazaImprumut_Click(object sender, RoutedEventArgs e)
        {
            if (lstImprumuturi.SelectedItem == null)
            {
                txtRezultatImprumut.Text = "Selectați un împrumut pentru returnare.";
                return;
            }

            string imprumutSelectat = lstImprumuturi.SelectedItem.ToString();

            foreach (Echipament echipament in echipamente)
            {
                if (imprumutSelectat.Contains(echipament.Denumire))
                {
                    echipament.NumarDisponibil++;
                    break;
                }
            }

            imprumuturi.Remove(imprumutSelectat);

            lstImprumuturi.ItemsSource = null;
            lstImprumuturi.ItemsSource = imprumuturi;

            ActualizeazaLista();
            ActualizeazaComboEchipamente();
            SalveazaDate();

            txtRezultatImprumut.Text = "Echipamentul a fost returnat cu succes.";
        }
    }

}