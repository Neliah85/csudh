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
            foreach (var sor in File.ReadAllLines("csudh.txt").Skip(1)) // Fejléc kihagyása
            {
                domainLista.Add(new DomainRecord(sor));
            }

            // 3. feladat: Kiírás a konzolra
            Console.WriteLine("Beolvasott domain-IP párosok:");
            foreach (var record in domainLista)
            {
                Console.WriteLine(record);
            }
            Console.WriteLine($"\nÖsszesen {domainLista.Count} cím van a listában.");

            // 4. feladat: Domain szint kezelése
            Console.Write("\nAdj meg egy domain nevet: ");
            string domain = Console.ReadLine();
            Console.Write("Adj meg egy szintet (1-5): ");
            int szint = int.Parse(Console.ReadLine());
            Console.WriteLine($"A {szint}. szintű domain: {DomainRecord.DomainSzint(domain, szint)}");

            // 5. feladat: Az első domain struktúrájának kiírása
            if (domainLista.Count > 0)
            {
                string elsoDomain = domainLista[0].DomainNev;
                Console.WriteLine($"\nAz első domain: {elsoDomain}");

                for (int i = 1; i <= 5; i++)
                {
                    Console.WriteLine($"{i}. szint: {DomainRecord.DomainSzint(elsoDomain, i)}");
                }
            }
            else
            {
                Console.WriteLine("Nincs elérhető domain az adatbázisban.");
            }

            // 6. feladat: HTML táblázat mentése
            MentesHTML(domainLista, "table.html");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hiba történt: " + ex.Message);
        }
    }


    public static void MentesHTML(List<DomainRecord> lista, string fajlnev)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(fajlnev))
            {
                sw.WriteLine("<table border='1'>");
                sw.WriteLine("<tr><th>Domain név</th><th>IP cím</th></tr>");

                foreach (var rekord in lista)
                {
                    sw.WriteLine($"<tr><td>{rekord.DomainNev}</td><td>{rekord.IpCim}</td></tr>");
                }

                sw.WriteLine("</table>");
            }
            Console.WriteLine($"A HTML táblázat sikeresen mentve: {fajlnev}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hiba történt a HTML mentése közben: " + ex.Message);
        }
    }
}
