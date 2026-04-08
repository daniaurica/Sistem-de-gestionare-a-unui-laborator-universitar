using System.Windows;
using Model.Entitati;
using Model.Enumerari;

namespace SistemGestionareLaboratorWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Echipament echipament = new Echipament
            {
                Id = 1,
                Denumire = "Laptop Dell",
                Tip = TipEchipament.Laptop,
                NumarTotal = 10,
                NumarDisponibil = 7,
                Optiuni = OptiuniEchipament.USB | OptiuniEchipament.HDMI | OptiuniEchipament.Wireless
            };

            txtId.Text = echipament.Id.ToString();
            txtDenumire.Text = echipament.Denumire;
            txtTip.Text = echipament.Tip.ToString();
            txtNumarTotal.Text = echipament.NumarTotal.ToString();
            txtNumarDisponibil.Text = echipament.NumarDisponibil.ToString();
            txtOptiuni.Text = echipament.Optiuni.ToString();
        }
    }
}