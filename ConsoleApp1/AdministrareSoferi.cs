using System;
using System.Collections.Generic;
using System.IO;
using TransportCompany;

namespace TransportCompany
{
    class AdministrareSoferi : StocareData
    {
        private string numeFisier = "soferi.txt";

        public void AddSofer(Sofer s)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine($"{s.Id},{s.Nume},{s.Kilometri}");
            }
        }

        public List<Sofer> GetSoferi()
        {
            List<Sofer> lista = new List<Sofer>();

            if (!File.Exists(numeFisier))
                return lista;

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    string[] date = linie.Split(',');

                    Sofer s = new Sofer(
                        int.Parse(date[0]),
                        date[1]
                    );

                    s.Kilometri = double.Parse(date[2]);

                    lista.Add(s);
                }
            }

            return lista;
        }
    }
}