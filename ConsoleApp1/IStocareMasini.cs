using System.Collections.Generic;
using TransportCompany;

interface IStocareMasini
{
    void AddMasina(Masina m);
    List<Masina> GetMasini();
}