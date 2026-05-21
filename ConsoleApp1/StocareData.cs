using System.Collections.Generic;

namespace TransportCompany
{
    internal interface StocareData
    {
        void AddSofer(Sofer s);
        List<Sofer> GetSoferi();
    }
}