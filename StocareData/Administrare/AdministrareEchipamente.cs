using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model.Entitati;
using Model.Enumerari;

namespace StocareData.Administrare
{
    public class AdministrareEchipamente
    {
        private List<Echipament> echipamente;
        private string numeFisier;

        public AdministrareEchipamente(string numeFisier)
        {
            this.numeFisier = numeFisier;
            echipamente = new List<Echipament>();

            if (File.Exists(numeFisier))
            {
                string[] linii = File.ReadAllLines(numeFisier);

                foreach (string linie in linii)
                {
                    string[] parti = linie.Split(';');

                    Echipament echipament = new Echipament();
                    echipament.Id = int.Parse(parti[0]);
                    echipament.Denumire = parti[1];
                    echipament.Tip = (TipEchipament)int.Parse(parti[2]);
                    echipament.NumarTotal = int.Parse(parti[3]);
                    echipament.NumarDisponibil = int.Parse(parti[4]);
                    echipament.Optiuni = (OptiuniEchipament)int.Parse(parti[5]);

                    echipamente.Add(echipament);
                }
            }
        }

        public void AdaugaEchipament(Echipament echipament)
        {
            echipamente.Add(echipament);
            SalveazaInFisier();
        }

        public List<Echipament> GetToateEchipamentele()
        {
            return echipamente;
        }

        public List<Echipament> CautaDupaDenumire(string text)
        {
            return echipamente
                .Where(e => e.Denumire.ToLower().Contains(text.ToLower()))
                .ToList();
        }
        public void SalveazaModificari()
        {
            SalveazaInFisier();
        }

        private void SalveazaInFisier()
        {
            List<string> linii = new List<string>();

            foreach (Echipament echipament in echipamente)
            {
                string linie = echipament.Id + ";" +
                               echipament.Denumire + ";" +
                               (int)echipament.Tip + ";" +
                               echipament.NumarTotal + ";" +
                               echipament.NumarDisponibil + ";" +
                               (int)echipament.Optiuni;

                linii.Add(linie);
            }

            File.WriteAllLines(numeFisier, linii);
        }
    }
}