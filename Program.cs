﻿using System;
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
                Console.WriteLine("\n5. feladat: Az első domain felépítése");

                var reszek = elsoDomain.Split('.');

                string szint1 = reszek.Length >= 3 ? reszek[2] : "nincs"; // 1. szint = edu
                string szint2 = reszek.Length >= 2 ? reszek[1] : "nincs"; // 2. szint = csudh
                string szint3 = reszek.Length >= 1 ? reszek[0] : "nincs"; // 3. szint = szervernév
                string szint4 = reszek.Length >= 4 ? reszek[3] : "nincs"; // 4. szint
                string szint5 = reszek.Length >= 5 ? reszek[4] : "nincs"; // 5. szint

                Console.WriteLine($"1. szint: {szint1}");
                Console.WriteLine($"2. szint: {szint2}");
                Console.WriteLine($"3. szint: {szint3}");
                Console.WriteLine($"4. szint: {szint4}");
                Console.WriteLine($"5. szint: {szint5}");
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
                sw.WriteLine("<tr><th>Ssz</th><th>Host domainneve</th><th>Host IP címe</th><th>1. szint</th><th>2. szint</th><th>3. szint</th><th>4. szint</th><th>5. szint</th></tr>");

                int index = 1;
                foreach (var rekord in lista)
                {
                    var reszek = rekord.DomainNev.Split('.');

                    string szint1 = reszek.Length >= 3 ? reszek[2] : "nincs"; // 1. szint = edu
                    string szint2 = reszek.Length >= 2 ? reszek[1] : "nincs"; // 2. szint = csudh
                    string szint3 = reszek.Length >= 1 ? reszek[0] : "nincs"; // 3. szint = szervernév
                    string szint4 = reszek.Length >= 4 ? reszek[3] : "nincs"; // 4. szint
                    string szint5 = reszek.Length >= 5 ? reszek[4] : "nincs"; // 5. szint

                    sw.WriteLine(
                        $"<tr><td>{index}</td>" + // Sorszám
                        $"<td>{rekord.DomainNev}</td>" + // Domain név
                        $"<td>{rekord.IpCim}</td>" + // IP-cím
                        $"<td>{szint1}</td>" + // 1. szint (edu)
                        $"<td>{szint2}</td>" + // 2. szint (csudh)
                        $"<td>{szint3}</td>" + // 3. szint (gépnév)
                        $"<td>{szint4}</td>" + // 4. szint (ha nincs, "nincs")
                        $"<td>{szint5}</td></tr>"); // 5. szint (ha nincs, "nincs")

                    index++;
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


