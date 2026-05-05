using System.Collections.Generic;
using System.IO;
using Model.Entitati;

namespace StocareData.Administrare
{
    public class AdministrareStudenti
    {
        private List<Student> studenti;
        private string numeFisier;

        public AdministrareStudenti(string numeFisier)
        {
            this.numeFisier = numeFisier;
            studenti = new List<Student>();

            if (File.Exists(numeFisier))
            {
                string[] linii = File.ReadAllLines(numeFisier);

                foreach (string linie in linii)
                {
                    string[] parti = linie.Split(';');

                    Student student = new Student();
                    student.Id = int.Parse(parti[0]);
                    student.Nume = parti[1];
                    student.Prenume = parti[2];
                    student.Grupa = parti[3];

                    studenti.Add(student);
                }
            }
        }

        public void AdaugaStudent(Student student)
        {
            studenti.Add(student);
            SalveazaInFisier();
        }

        public List<Student> GetTotiStudentii()
        {
            return studenti;
        }

        public List<Student> CautaDupaNume(string text)
        {
            List<Student> rezultate = new List<Student>();

            foreach (Student student in studenti)
            {
                if (student.Nume.ToLower().Contains(text.ToLower()))
                {
                    rezultate.Add(student);
                }
            }

            return rezultate;
        }

        private void SalveazaInFisier()
        {
            List<string> linii = new List<string>();

            foreach (Student student in studenti)
            {
                string linie = student.Id + ";" +
                               student.Nume + ";" +
                               student.Prenume + ";" +
                               student.Grupa;

                linii.Add(linie);
            }

            File.WriteAllLines(numeFisier, linii);
        }
    }
}