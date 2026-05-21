using System;
using System.Collections.Generic;
using System.IO;
using TransportCompany;

class AdministrareMasiniFisier : IStocareMasini
{
    private string numeFisier = "masini.txt";

    public void AddMasina(Masina m)
    {
        using (StreamWriter sw = new StreamWriter(numeFisier, true))
        {
            sw.WriteLine($"{m.Model},{m.NumarInmatriculare},{m.Kilometri},{m.culoare},{m.optiuni}");
        }
    }

    public List<Masina> GetMasini()
    {
        List<Masina> lista = new List<Masina>();

        if (!File.Exists(numeFisier))
            return lista;

        using (StreamReader sr = new StreamReader(numeFisier))
        {
            string linie;

            while ((linie = sr.ReadLine()) != null)
            {
                string[] date = linie.Split(',');

                Masina m = new Masina(
                    date[0],
                    date[1],
                    (Culoare)Enum.Parse(typeof(Culoare), date[3]),
                    (Optiuni)Enum.Parse(typeof(Optiuni), date[4])
                );

                m.Kilometri = double.Parse(date[2]);

                lista.Add(m);
            }
        }

        return lista;
    }
}