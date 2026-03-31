using System;
using System.Collections.Generic;
using Model.Entitati;
using Model.Enumerari;
using StocareData.Administrare;

namespace SistemGestionareLaboratorApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AdministrareEchipamente adminEchipamente = new AdministrareEchipamente("echipamente.txt");
            AdministrareStudenti adminStudenti = new AdministrareStudenti("studenti.txt");
            AdministrareImprumuturi adminImprumuturi = new AdministrareImprumuturi("imprumuturi.txt");

            bool ruleaza = true;

            while (ruleaza)
            {
                Console.WriteLine("===== MENIU PRINCIPAL =====");
                Console.WriteLine("1. Adauga echipament");
                Console.WriteLine("2. Afiseaza echipamente");
                Console.WriteLine("3. Adauga student");
                Console.WriteLine("4. Afiseaza studenti");
                Console.WriteLine("5. Imprumuta echipament");
                Console.WriteLine("6. Afiseaza imprumuturi");
                Console.WriteLine("7. Returneaza echipament");
                Console.WriteLine("8. Cauta echipament dupa denumire");
                Console.WriteLine("9. Cauta student dupa nume");
                Console.WriteLine("0. Iesire");
                Console.Write("Alege optiunea: ");

                string optiune = Console.ReadLine();
                Console.WriteLine();

                switch (optiune)
                {
                    case "1":
                        AdaugaEchipament(adminEchipamente);
                        break;

                    case "2":
                        AfiseazaEchipamente(adminEchipamente);
                        break;

                    case "3":
                        AdaugaStudent(adminStudenti);
                        break;

                    case "4":
                        AfiseazaStudenti(adminStudenti);
                        break;

                    case "5":
                        ImprumutaEchipament(adminEchipamente, adminStudenti, adminImprumuturi);
                        break;

                    case "6":
                        AfiseazaImprumuturi(adminImprumuturi);
                        break;

                    case "7":
                        ReturneazaEchipament(adminEchipamente, adminImprumuturi);
                        break;

                    case "8":
                        CautaEchipamentDupaDenumire(adminEchipamente);
                        break;

                    case "9":
                        CautaStudentDupaNume(adminStudenti);
                        break;

                    case "0":
                        ruleaza = false;
                        break;

                    default:
                        Console.WriteLine("Optiune invalida.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void AdaugaEchipament(AdministrareEchipamente adminEchipamente)
        {
            Echipament echipament = new Echipament();

            Console.Write("Id: ");
            echipament.Id = int.Parse(Console.ReadLine());

            Console.Write("Denumire: ");
            echipament.Denumire = Console.ReadLine();

            Console.WriteLine("Tip echipament:");
            Console.WriteLine("0 - Laptop");
            Console.WriteLine("1 - Mouse");
            Console.WriteLine("2 - Tastatura");
            Console.WriteLine("3 - Monitor");
            Console.WriteLine("4 - Proiector");
            Console.WriteLine("5 - CameraWeb");
            Console.Write("Alege tipul: ");
            echipament.Tip = (TipEchipament)int.Parse(Console.ReadLine());

            Console.Write("Numar total: ");
            echipament.NumarTotal = int.Parse(Console.ReadLine());

            echipament.NumarDisponibil = echipament.NumarTotal;

            Console.WriteLine("Optiuni echipament:");
            Console.WriteLine("0 - Nimic");
            Console.WriteLine("1 - Wireless");
            Console.WriteLine("2 - USB");
            Console.WriteLine("4 - Bluetooth");
            Console.WriteLine("8 - HDMI");
            Console.Write("Alege o valoare: ");
            echipament.Optiuni = (OptiuniEchipament)int.Parse(Console.ReadLine());

            adminEchipamente.AdaugaEchipament(echipament);

            Console.WriteLine("Echipamentul a fost adaugat.");
        }

        static void AfiseazaEchipamente(AdministrareEchipamente adminEchipamente)
        {
            List<Echipament> echipamente = adminEchipamente.GetToateEchipamentele();

            if (echipamente.Count == 0)
            {
                Console.WriteLine("Nu exista echipamente.");
                return;
            }

            foreach (Echipament echipament in echipamente)
            {
                Console.WriteLine("Id: " + echipament.Id);
                Console.WriteLine("Denumire: " + echipament.Denumire);
                Console.WriteLine("Tip: " + echipament.Tip);
                Console.WriteLine("Numar total: " + echipament.NumarTotal);
                Console.WriteLine("Numar disponibil: " + echipament.NumarDisponibil);
                Console.WriteLine("Optiuni: " + echipament.Optiuni);
                Console.WriteLine("--------------------------");
            }
        }

        static void AdaugaStudent(AdministrareStudenti adminStudenti)
        {
            Student student = new Student();

            Console.Write("Id: ");
            student.Id = int.Parse(Console.ReadLine());

            Console.Write("Nume: ");
            student.Nume = Console.ReadLine();

            Console.Write("Prenume: ");
            student.Prenume = Console.ReadLine();

            Console.Write("Grupa: ");
            student.Grupa = Console.ReadLine();

            adminStudenti.AdaugaStudent(student);

            Console.WriteLine("Studentul a fost adaugat.");
        }

        static void AfiseazaStudenti(AdministrareStudenti adminStudenti)
        {
            List<Student> studenti = adminStudenti.GetTotiStudentii();

            if (studenti.Count == 0)
            {
                Console.WriteLine("Nu exista studenti.");
                return;
            }

            foreach (Student student in studenti)
            {
                Console.WriteLine("Id: " + student.Id);
                Console.WriteLine("Nume: " + student.Nume);
                Console.WriteLine("Prenume: " + student.Prenume);
                Console.WriteLine("Grupa: " + student.Grupa);
                Console.WriteLine("--------------------------");
            }
        }

        static void ImprumutaEchipament(
            AdministrareEchipamente adminEchipamente,
            AdministrareStudenti adminStudenti,
            AdministrareImprumuturi adminImprumuturi)
        {
            Console.Write("Id student: ");
            int idStudent = int.Parse(Console.ReadLine());

            Console.Write("Id echipament: ");
            int idEchipament = int.Parse(Console.ReadLine());

            List<Echipament> echipamente = adminEchipamente.GetToateEchipamentele();
            List<Student> studenti = adminStudenti.GetTotiStudentii();
            List<Imprumut> imprumuturi = adminImprumuturi.GetToateImprumuturile();

            Echipament echipamentGasit = echipamente.Find(e => e.Id == idEchipament);
            Student studentGasit = studenti.Find(s => s.Id == idStudent);

            if (studentGasit == null)
            {
                Console.WriteLine("Studentul nu exista.");
                return;
            }

            if (echipamentGasit == null)
            {
                Console.WriteLine("Echipamentul nu exista.");
                return;
            }

            if (echipamentGasit.NumarDisponibil <= 0)
            {
                Console.WriteLine("Nu mai exista exemplare disponibile.");
                return;
            }

            int numarMaximImprumuturi = 3;
            int numarImprumuturiActive = 0;

            foreach (Imprumut imprumut in imprumuturi)
            {
                if (imprumut.IdStudent == idStudent && imprumut.Status == StatusImprumut.Activ)
                {
                    numarImprumuturiActive++;
                }
            }

            if (numarImprumuturiActive >= numarMaximImprumuturi)
            {
                Console.WriteLine("Studentul a atins numarul maxim de imprumuturi.");
                return;
            }

            Imprumut imprumutNou = new Imprumut();
            imprumutNou.Id = imprumuturi.Count + 1;
            imprumutNou.IdStudent = idStudent;
            imprumutNou.IdEchipament = idEchipament;
            imprumutNou.DataImprumut = DateTime.Now;
            imprumutNou.DataReturnare = DateTime.Now.AddDays(7);
            imprumutNou.Status = StatusImprumut.Activ;

            echipamentGasit.NumarDisponibil--;

            adminImprumuturi.AdaugaImprumut(imprumutNou);
            adminEchipamente.SalveazaModificari();

            Console.WriteLine("Imprumutul a fost realizat cu succes.");
        }

        static void AfiseazaImprumuturi(AdministrareImprumuturi adminImprumuturi)
        {
            List<Imprumut> imprumuturi = adminImprumuturi.GetToateImprumuturile();

            if (imprumuturi.Count == 0)
            {
                Console.WriteLine("Nu exista imprumuturi.");
                return;
            }

            foreach (Imprumut imprumut in imprumuturi)
            {
                Console.WriteLine("Id imprumut: " + imprumut.Id);
                Console.WriteLine("Id student: " + imprumut.IdStudent);
                Console.WriteLine("Id echipament: " + imprumut.IdEchipament);
                Console.WriteLine("Data imprumut: " + imprumut.DataImprumut);

                if (imprumut.Status == StatusImprumut.Returnat)
                {
                    Console.WriteLine("Status: Returnat");
                }
                else
                {
                    Console.WriteLine("Status: Activ");
                    Console.WriteLine("Termen returnare: " + imprumut.DataReturnare);
                }

                Console.WriteLine("--------------------------");
            }
        }

        static void ReturneazaEchipament(
            AdministrareEchipamente adminEchipamente,
            AdministrareImprumuturi adminImprumuturi)
        {
            Console.Write("Id imprumut: ");
            int idImprumut = int.Parse(Console.ReadLine());

            List<Imprumut> imprumuturi = adminImprumuturi.GetToateImprumuturile();
            List<Echipament> echipamente = adminEchipamente.GetToateEchipamentele();

            Imprumut imprumutGasit = imprumuturi.Find(i => i.Id == idImprumut);

            if (imprumutGasit == null)
            {
                Console.WriteLine("Imprumutul nu exista.");
                return;
            }

            if (imprumutGasit.Status == StatusImprumut.Returnat)
            {
                Console.WriteLine("Echipamentul a fost deja returnat.");
                return;
            }

            Echipament echipamentGasit = echipamente.Find(e => e.Id == imprumutGasit.IdEchipament);

            if (echipamentGasit != null)
            {
                echipamentGasit.NumarDisponibil++;
            }

            imprumutGasit.Status = StatusImprumut.Returnat;
            adminEchipamente.SalveazaModificari();
            adminImprumuturi.SalveazaModificari();

            Console.WriteLine("Echipamentul a fost returnat cu succes.");
        }

        static void CautaEchipamentDupaDenumire(AdministrareEchipamente adminEchipamente)
        {
            Console.Write("Introdu denumirea cautata: ");
            string text = Console.ReadLine();

            List<Echipament> rezultate = adminEchipamente.CautaDupaDenumire(text);

            if (rezultate.Count == 0)
            {
                Console.WriteLine("Nu s-au gasit echipamente.");
                return;
            }

            foreach (Echipament echipament in rezultate)
            {
                Console.WriteLine("Id: " + echipament.Id);
                Console.WriteLine("Denumire: " + echipament.Denumire);
                Console.WriteLine("Tip: " + echipament.Tip);
                Console.WriteLine("Numar total: " + echipament.NumarTotal);
                Console.WriteLine("Numar disponibil: " + echipament.NumarDisponibil);
                Console.WriteLine("Optiuni: " + echipament.Optiuni);
                Console.WriteLine("--------------------------");
            }
        }

        static void CautaStudentDupaNume(AdministrareStudenti adminStudenti)
        {
            Console.Write("Introdu numele cautat: ");
            string text = Console.ReadLine();

            List<Student> rezultate = adminStudenti.CautaDupaNume(text);

            if (rezultate.Count == 0)
            {
                Console.WriteLine("Nu s-au gasit studenti.");
                return;
            }

            foreach (Student student in rezultate)
            {
                Console.WriteLine("Id: " + student.Id);
                Console.WriteLine("Nume: " + student.Nume);
                Console.WriteLine("Prenume: " + student.Prenume);
                Console.WriteLine("Grupa: " + student.Grupa);
                Console.WriteLine("--------------------------");
            }
        }
    }
}