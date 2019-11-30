using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StadtLandFlussLib;

namespace StadtLandFlussTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Land deutschland = new Land("Deutschland");

            Stadt bielefeld = new Stadt("Bielefeld", deutschland);
            Stadt muehlheim = new Stadt("Mühlheim", deutschland);
            Stadt essen = new Stadt("Essen", deutschland);
            Stadt berlin = new Stadt("Berlin", deutschland);
            Stadt frankfurt = new Stadt("Frankfurt", deutschland);

            deutschland.HauptStadt = berlin;

            Fluss ruhr = new Fluss("Ruhr", false);
            Fluss spree = new Fluss("Spree", false);
            Fluss havel = new Fluss("Havel", false);
            Fluss main = new Fluss("Main", true);

            muehlheim.LiegtAn.Add(ruhr);
            essen.LiegtAn.Add(ruhr);

            berlin.LiegtAn.Add(havel);
            berlin.LiegtAn.Add(spree);

            frankfurt.LiegtAn.Add(main);

            Person otto = new Person(
                "Otto",
                "Müller",
                bielefeld,
                new []{essen});

            Person erwin = new Person(
                "Erwin",
                "Schmitt",
                bielefeld,
                new[] { berlin});

            Person jan = new Person(
                "Jan",
                "Meier",
                muehlheim,
                new []{berlin});

            Person klaas = new Person(
                "Klaas",
                "Schulze",
                essen,
                new []{frankfurt});

            Person hein = new Person(
                "Hein",
                "Blöd",
                berlin,
                new[] { bielefeld});

            Person pitt = new Person(
                "Pitt",
                "Fischer",
                frankfurt,
                new[] { muehlheim, frankfurt});

            Console.Write(pitt.Vorname + " ");
            Console.Write(pitt.Familienname);
            Console.Write(" ist geboren in ");
            Console.Write(pitt.GeborenIn.Name);
            bool first = true;

            foreach (var fluss in pitt.GeborenIn.LiegtAn)
            {
                if (first)
                {
                    Console.Write(" gelegen ");
                    first = false;
                }
                else
                {
                    Console.Write(" und ");
                }

                if (fluss.Male)
                {
                    Console.Write("am ");
                }
                else
                {
                    Console.Write("an der ");
                }
                Console.Write(fluss.Name);
            }
            Console.Write(" gelegen in ");
            Console.Write(pitt.GeborenIn.LiegtIn.Name);

            Console.Write(" dessen Hauptstadt ist ");
            Console.Write(pitt.GeborenIn.LiegtIn.HauptStadt.Name);
            first = true;
            foreach (var fluss in pitt.GeborenIn.LiegtIn.HauptStadt.LiegtAn)
            {
                if (first)
                {
                    Console.Write(" gelegen ");
                    first = false;
                }
                else
                {
                    Console.Write(" und ");
                }

                if (fluss.Male)
                {
                    Console.Write("am ");
                }
                else
                {
                    Console.Write("an der ");
                }
                Console.Write(fluss.Name);
            }

            bool firstWohnort = true;
            foreach (var wohnort in pitt.WohntIn)
            {
                if (firstWohnort)
                {
                    Console.Write(" wohnhaft in ");
                    firstWohnort = false;
                }
                else
                {
                    Console.Write(" und ");    
                }

                Console.Write(wohnort.Name);
                bool firstFluss = true;
                foreach (var fluss in wohnort.LiegtAn)
                {
                    if (firstFluss)
                    {
                        Console.Write(" gelegen ");
                        firstFluss = false;
                    }
                    else
                    {
                        Console.Write(" und ");
                    }

                    if (fluss.Male)
                    {
                        Console.Write("am ");
                    }
                    else
                    {
                        Console.Write("an der ");
                    }
                    Console.Write(fluss.Name);
                }
                Console.Write(" gelegen in ");
                Console.Write(wohnort.LiegtIn.Name);

                Console.Write(" dessen Hauptstadt ist ");
                Console.Write(wohnort.LiegtIn.HauptStadt.Name);
                first = true;
                foreach (var fluss in wohnort.LiegtIn.HauptStadt.LiegtAn)
                {
                    if (first)
                    {
                        Console.Write(" gelegen ");
                        first = false;
                    }
                    else
                    {
                        Console.Write(" und ");
                    }

                    if (fluss.Male)
                    {
                        Console.Write("am ");
                    }
                    else
                    {
                        Console.Write("an der ");
                    }
                    Console.Write(fluss.Name);
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();

            Console.Write(hein.Vorname + " ");
            Console.Write(hein.Familienname);
            Console.Write(" ist geboren in ");
            Console.Write(hein.GeborenIn.getShortDescription());
            Console.Write(" gelegen in ");
            Console.Write(hein.GeborenIn.LiegtIn.Name);

            Console.Write(" dessen Hauptstadt ist ");
            Console.Write(hein
                .GeborenIn
                .LiegtIn
                .HauptStadt
                .getShortDescription());
            firstWohnort = true;
            foreach (var wohnort in hein.WohntIn)
            {
                if (firstWohnort)
                {
                    Console.Write(" wohnhaft in ");
                    firstWohnort = false;
                }
                else
                {
                    Console.Write(" und ");
                }

                Console.Write(wohnort.getShortDescription());
                Console.Write(" gelegen in ");
                Console.Write(wohnort.LiegtIn.Name);

                Console.Write(" dessen Hauptstadt ist ");
                Console.Write(wohnort
                    .LiegtIn
                    .HauptStadt
                    .getShortDescription());
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();

            Console.Write(klaas.Vorname + " ");
            Console.Write(klaas.Familienname);
            Console.Write(" ist geboren in ");
            Console.Write(klaas.GeborenIn.getShortDescription());
            Console.Write(" gelegen in ");
            Console.Write(klaas.GeborenIn.LiegtIn.getDescription());

            firstWohnort = true;
            foreach (var wohnort in klaas.WohntIn)
            {
                if (firstWohnort)
                {
                    Console.Write(" wohnhaft in ");
                    firstWohnort = false;
                }
                else
                {
                    Console.Write(" und ");
                }

                Console.Write(wohnort.getShortDescription());
                Console.Write(" gelegen in ");
                Console.Write(wohnort.LiegtIn.getDescription());
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();


            Console.Write(jan.Vorname + " ");
            Console.Write(jan.Familienname);
            Console.Write(" ist geboren in ");
            Console.Write(jan.GeborenIn.getDescription());

            firstWohnort = true;
            foreach (var wohnort in jan.WohntIn)
            {
                if (firstWohnort)
                {
                    Console.Write(" wohnhaft in ");
                    firstWohnort = false;
                }
                else
                {
                    Console.Write(" und ");
                }
                Console.Write(wohnort.getDescription());
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();

            Console.Write(erwin.getFullname());
            Console.Write(" ist geboren in ");
            Console.Write(erwin.GeborenIn.getDescription());

            firstWohnort = true;
            foreach (var wohnort in erwin.WohntIn)
            {
                if (firstWohnort)
                {
                    Console.Write(" wohnhaft in ");
                    firstWohnort = false;
                }
                else
                {
                    Console.Write(" und ");
                }
                Console.Write(wohnort.getDescription());
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();

            Console.WriteLine(otto.getDescription());
            Console.WriteLine();
            Console.ReadKey();

            Console.WriteLine(otto.getDescription());
            Console.WriteLine();
            Console.WriteLine(erwin.getDescription());
            Console.WriteLine();
            Console.WriteLine(jan.getDescription());
            Console.WriteLine();
            Console.WriteLine(klaas.getDescription());
            Console.WriteLine();
            Console.WriteLine(hein.getDescription());
            Console.WriteLine();
            Console.WriteLine(pitt.getDescription());
            Console.WriteLine();
            Console.ReadKey();


        }
    }
}
