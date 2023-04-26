using Pudelko1;
using static Pudelko1.Pudelko;
using System;
using System.Collections.Generic;

namespace Pudelka
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pudelko> pudelka = new List<Pudelko>();

            pudelka.Add(new Pudelko(1, 2, 10));
            pudelka.Add(new Pudelko(5, 5, 5));
            pudelka.Add(new Pudelko(10));
            pudelka.Add(new Pudelko());
            pudelka.Add(new Pudelko(2, 10, 5, UnitOfMeasure.centimeter));
            pudelka.Add(new Pudelko(10, 10, 20, UnitOfMeasure.centimeter));

            Console.WriteLine("Pudełka na liście:");
            foreach (var pud in pudelka)
            {
                Console.WriteLine(pud);
            }

            pudelka.Sort(delegate (Pudelko p1, Pudelko p2)
            {
                if (p1.Objetosc < p2.Objetosc)
                    return -1;
                else if (p1.Objetosc > p2.Objetosc)
                    return 1;
                else
                {
                    if (p1.Pole < p2.Pole)
                        return -1;
                    else if (p1.Pole > p2.Pole)
                        return 1;
                    else
                    {
                        if (p1.A + p1.B + p1.C <
                            p2.A + p2.B + p2.C)
                            return -1;
                        else if (p1.A + p1.B + p1.C >
                                 p2.A + p2.B + p2.C)
                            return 1;
                        else
                            return 0;
                    }
                }
            });

            Console.WriteLine("\nPudełka na posortowanej liście:");
            foreach (var pud in pudelka)
            {
                Console.WriteLine(pud);
            }
        }
    }
}
