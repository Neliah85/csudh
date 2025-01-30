using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        List<DomainRecord> domainLista = new List<DomainRecord>();

        try
        {
            foreach (var sor in File.ReadAllLines("csudh.txt"))
            {
                if (!sor.StartsWith("domain-name"))  // Az első sor fejléc, azt kihagyjuk
                {
                    domainLista.Add(new DomainRecord(sor));
                }
            }

            // Adatok kiírása
            Console.WriteLine("Beolvasott domain-IP párosok:");
            foreach (var record in domainLista)
            {
                Console.WriteLine(record);
            }

            Console.WriteLine($"\nÖsszesen {domainLista.Count} cím van a listában.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hiba történt: " + ex.Message);
        }
    }

    public static string DomainSzint(string domainnev, int szint)
    {
        var reszek = domainnev.Split('.');
        if (szint > 0 && szint <= reszek.Length)
        {
            return string.Join(".", reszek.Skip(reszek.Length - szint));
        }
        else
        {
            throw new ArgumentException("Érvénytelen szint érték: " + szint);
        }
    }


}
