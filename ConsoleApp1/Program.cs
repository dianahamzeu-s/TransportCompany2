using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportCompany
{
    class Sofer
    {
        public int Id;
        public string Nume;
        public double Kilometri;

        public Sofer(int id, string nume)
        {
            Id = id;
            Nume = nume;
            Kilometri = 0;
        }
    }

    class Masina
    {
        public string Model;
        public string NumarInmatriculare;
        public double Kilometri;

        public Culoare culoare;
        public Optiuni optiuni;

        public Masina(string model, string numar, Culoare c, Optiuni o)
        {
            Model = model;
            NumarInmatriculare = numar;
            Kilometri = 0;
            culoare = c;
            optiuni = o;
        }
    }
    class Traseu
    {
        public string Plecare;
        public string Destinatie;
        public double Distanta;

        public Traseu(string plecare, string destinatie, double distanta)
        {
            Plecare = plecare;
            Destinatie = destinatie;
            Distanta = distanta;
        }
    }

    class Cursa
    {
        public Sofer sofer;
        public Masina masina;
        public double distanta;

        public Cursa(Sofer s, Masina m, double d)
        {
            sofer = s;
            masina = m;
            distanta = d;

            sofer.Kilometri += d;
            masina.Kilometri += d;
        }
    }
}

namespace TransportCompany
{
    class Program
    {
        static List<Sofer> soferi = new List<Sofer>();

        static void Main(string[] args)
        {
            string optiune;

            do
            {
                Console.WriteLine("\n=== MENIU ===");
                Console.WriteLine("1. Adauga sofer");
                Console.WriteLine("2. Afiseaza soferi");
                Console.WriteLine("3. Cauta sofer dupa nume");
                Console.WriteLine("4. Adauga masina");
                Console.WriteLine("5. Afiseaza masini");
                Console.WriteLine("0. Iesire");

                Console.Write("Alege optiunea: ");
                optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1":
                        AdaugaSofer();
                        break;
                    case "2":
                        AfiseazaSoferi();
                        break;
                    case "3":
                        CautaSofer();
                        break;
                    case "4":
                        masini.Add(CitesteMasina());
                        break;

                    case "5":
                        AfiseazaMasini();
                        break;

                }

            } while (optiune != "0");
        }
        static StocareData adminSoferi = new AdministrareSoferi();
        static IStocareMasini adminMasini = new AdministrareMasiniFisier();
        static StocareData admin = new AdministrareSoferi();

        static List<Masina> masini = new List<Masina>();

        static Masina CitesteMasina()
        {
            Console.Write("Model: ");
            string model = Console.ReadLine();

            Console.Write("Numar: ");
            string numar = Console.ReadLine();

            Console.WriteLine("Alege culoare: 0-Rosu, 1-Alb, 2-Negru");
            int c = int.Parse(Console.ReadLine());

            Console.WriteLine("Optiuni (1-Aer, 2-Navigatie, 4-Automata): ");
            int o = int.Parse(Console.ReadLine());

            return new Masina(model, numar, (Culoare)c, (Optiuni)o);
        }

        static void AdaugaMasina()
        {
            Masina m = CitesteMasina();
            adminMasini.AddMasina(m);

            Console.WriteLine("Masina salvata!");
        }

        static void AfiseazaMasini()
        {
            if (masini.Count == 0)
            {
                Console.WriteLine("Nu exista masini!");
                return;
            }

            foreach (Masina m in masini)
            {
                Console.WriteLine($"Model: {m.Model}");
                Console.WriteLine($"Numar: {m.NumarInmatriculare}");
                Console.WriteLine($"Culoare: {m.culoare}");
                Console.WriteLine($"Optiuni: {m.optiuni}");
                Console.WriteLine("-------------------");
            }
        }

        static void AdaugaSofer()
        {
                Console.Write("ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Nume: ");
                string nume = Console.ReadLine();

                Sofer s = new Sofer(id, nume);

                admin.AddSofer(s);

            Console.WriteLine("Salvat in fisier!");
        }

        static void AfiseazaSoferi()
        {
            var soferi = admin.GetSoferi();

            foreach (Sofer s in soferi)
             {
                 Console.WriteLine($"ID:{s.Id} Nume:{s.Nume} Km:{s.Kilometri}");
             }
        }

        static void CautaSofer()
        {
                Console.Write("Nume: ");
                string nume = Console.ReadLine();

                var soferi = admin.GetSoferi();

                var rezultat = soferi
                    .FirstOrDefault(s => s.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase));

                if (rezultat != null)
                    Console.WriteLine($"Gasit: {rezultat.Nume}");
                else
                    Console.WriteLine("Nu exista");
        }
    }
}