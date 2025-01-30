using System;
using System.Linq;

public class DomainRecord
{
    public string DomainNev { get; private set; }
    public string IpCim { get; private set; }

    // Konstruktor, amely egy fájlból olvasott sort kap paraméterként
    public DomainRecord(string sor)
    {
        var adatok = sor.Split(';');
        if (adatok.Length == 2)
        {
            DomainNev = adatok[0];
            IpCim = adatok[1];
        }
        else
        {
            throw new ArgumentException("Hibás sorformátum: " + sor);
        }
    }

    
    public string GetDomainSzint(int szint)
    {
        return DomainSzint(this.DomainNev, szint);
    }

   
    public static string DomainSzint(string domainnev, int szint)
    {
        var reszek = domainnev.Split('.');
        if (szint >= 1 && szint <= reszek.Length)
        {
            return string.Join(".", reszek.Skip(reszek.Length - szint));
        }
        else
        {
            return "nincs";
        }
    }

    public override string ToString()
    {
        return $"{DomainNev} - {IpCim}";
    }
}
