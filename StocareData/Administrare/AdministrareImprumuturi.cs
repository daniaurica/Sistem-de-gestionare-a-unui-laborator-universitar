using System;
using System.Collections.Generic;
using System.IO;
using Model.Entitati;
using Model.Enumerari;

namespace StocareData.Administrare
{
    public class AdministrareImprumuturi
    {
        private List<Imprumut> imprumuturi;
        private string numeFisier;

        public AdministrareImprumuturi(string numeFisier)
        {
            this.numeFisier = numeFisier;
            imprumuturi = new List<Imprumut>();

            if (File.Exists(numeFisier))
            {
                string[] linii = File.ReadAllLines(numeFisier);

                foreach (string linie in linii)
                {
                    string[] parti = linie.Split(';');

                    Imprumut imprumut = new Imprumut();
                    imprumut.Id = int.Parse(parti[0]);
                    imprumut.IdStudent = int.Parse(parti[1]);
                    imprumut.IdEchipament = int.Parse(parti[2]);
                    imprumut.DataImprumut = DateTime.Parse(parti[3]);
                    imprumut.DataReturnare = DateTime.Parse(parti[4]);
                    imprumut.Status = (StatusImprumut)int.Parse(parti[5]);

                    imprumuturi.Add(imprumut);
                }
            }
        }

        public void AdaugaImprumut(Imprumut imprumut)
        {
            imprumuturi.Add(imprumut);
            SalveazaInFisier();
        }

        public List<Imprumut> GetToateImprumuturile()
        {
            return imprumuturi;
        }

        public void SalveazaModificari()
        {
            SalveazaInFisier();
        }

        private void SalveazaInFisier()
        {
            List<string> linii = new List<string>();

            foreach (Imprumut imprumut in imprumuturi)
            {
                string linie = imprumut.Id + ";" +
                               imprumut.IdStudent + ";" +
                               imprumut.IdEchipament + ";" +
                               imprumut.DataImprumut + ";" +
                               imprumut.DataReturnare + ";" +
                               (int)imprumut.Status;

                linii.Add(linie);
            }

            File.WriteAllLines(numeFisier, linii);
        }
    }
}