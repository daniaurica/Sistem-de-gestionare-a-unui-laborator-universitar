using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Model.Entitati;
using Model.Enumerari;

namespace SistemGestionareLaboratorWPF
{
    public partial class MainWindow : Window
    {
        private List<Echipament> echipamente = new List<Echipament>();

        public MainWindow()
        {
            InitializeComponent();

            cmbTip.SelectedIndex = 0;
            dpDataImprumut.SelectedDate = System.DateTime.Now;

            ActualizeazaLista();
        }

        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            int numarTotal;

            if (!int.TryParse(txtNumarTotal.Text, out numarTotal))
            {
                MessageBox.Show("Introduceți un număr valid.");
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

            MessageBox.Show("Echipament adăugat.");
        }

        private void BtnActualizeaza_Click(object sender, RoutedEventArgs e)
        {
            if (lstEchipamente.SelectedItem == null)
            {
                MessageBox.Show("Selectați un echipament.");
                return;
            }

            Echipament echipament = (Echipament)lstEchipamente.SelectedItem;

            int numarTotal;

            if (!int.TryParse(txtNumarTotal.Text, out numarTotal))
            {
                MessageBox.Show("Introduceți un număr valid.");
                return;
            }

            echipament.Denumire = txtDenumire.Text;
            echipament.NumarTotal = numarTotal;
            echipament.Tip = GetTipEchipament();
            echipament.Optiuni = GetOptiuni();

            ActualizeazaLista();

            MessageBox.Show("Echipament actualizat.");
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
    }
}