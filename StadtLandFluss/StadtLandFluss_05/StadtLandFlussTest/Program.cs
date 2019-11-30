using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StadtLandFlussLibDB;

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
                new[] { essen });

            Person erwin = new Person(
                "Erwin",
                "Schmitt",
                bielefeld,
                new[] { berlin });

            Person jan = new Person(
                "Jan",
                "Meier",
                muehlheim,
                new[] { berlin });

            Person klaas = new Person(
                "Klaas",
                "Schulze",
                essen,
                new[] { frankfurt });

            Person hein = new Person(
                "Hein",
                "Blöd",
                berlin,
                new[] { bielefeld });

            Person pitt = new Person(
                "Pitt",
                "Fischer",
                frankfurt,
                new[] { muehlheim, frankfurt });



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
            Console.Write(hein.GeborenIn.LiegtIn.HauptStadt.getShortDescription());
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
                Console.Write(wohnort.LiegtIn.HauptStadt.getShortDescription());
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

            DemoData demoData = new DemoData();

            StadtLandFlussModelContainer context = new StadtLandFlussModelContainer();

            Console.WriteLine("Empty Database");

            try
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {

                    foreach (var stadt in context.Staedte.ToArray())
                    {
                        stadt.Einwohner.Clear();
                    }
                    context.SaveChanges();

                    context.People.RemoveRange(context.People.ToArray());
                    context.SaveChanges();
                    foreach (var stadt in context.Staedte.ToArray())
                    {
                        stadt.LiegtAn.Clear();
                        context.Staedte.Remove(stadt);
                    }
                    context.SaveChanges();
                    context.Staedte.RemoveRange(context.Staedte.ToArray());
                    context.SaveChanges();
                    context.Fluesse.RemoveRange(context.Fluesse.ToArray());
                    context.SaveChanges();
                    context.Laender.RemoveRange(context.Laender.ToArray());
                    context.SaveChanges();

                    dbContextTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();


            Console.WriteLine("Fill Database");
            try
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    context.People.AddRange(demoData.Personen);
                    context.Fluesse.AddRange(demoData.Fluesse);
                    context.Staedte.AddRange(demoData.Staedte);
                    context.Laender.AddRange(demoData.Laender);

                    context.SaveChanges();

                    dbContextTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();

            var Data = context;

            Console.WriteLine("List People with tt");
            var personen = (
                from person in Data.People
                where person.Vorname.Contains("tt")
                select person
            );

            foreach (Person person in personen)
            {
                Console.WriteLine(person.getFullname());
            }
            Console.ReadKey();

            Console.WriteLine("List People with tt's residence ");
            var wohnOrte =
                (from person in Data.People
                 where person.Vorname.Contains("tt")
                 from wi in person.WohntIn
                 orderby wi.Name
                 select wi)
                .Distinct();

            foreach (Stadt wohnOrt in wohnOrte)
            {
                Console.WriteLine(wohnOrt.getShortDescription());
            }
            Console.ReadKey();

            Console.WriteLine("List People living in first country");
            personen =
            (from person in Data.People
             where person.WohntIn.Any(
                 wi => wi.LiegtIn == Data.Laender.FirstOrDefault())
             orderby person.Familienname
             select person);

            foreach (Person person in personen)
            {
                Console.WriteLine(person.getFullname());
            }
            Console.ReadKey();

            Console.WriteLine("List Cities next to first river");
            Fluss flussToQuery = Data.Fluesse.FirstOrDefault();                
            if (flussToQuery == null) return;

            var staedte =
            from stadt in flussToQuery.Durchfließt                   
            select stadt;

            foreach (var stadt in staedte)
            {
                Console.WriteLine(stadt.getDescription());
            }
            Console.ReadKey();

            Console.WriteLine("List People living next to first river");


            personen =
                (
                    from person in Data.People
                    where person.WohntIn.Any(
                        wi => wi.LiegtAn.Any(
                            la => la == Data.Fluesse.FirstOrDefault()))
                    orderby person.Familienname ascending
                    select person
                )            
             .Distinct();


            foreach (var person in personen)
            {
                Console.WriteLine(person.getFullname());
            }
            Console.ReadKey();

            Land landToQuery = Data.Laender.FirstOrDefault();
            if (landToQuery == null) return;

            Console.WriteLine("List cities in first country");
            var staedteInLand = landToQuery.Staedte;

            foreach (var stadt in staedteInLand)
            {
                Console.WriteLine(stadt.getShortDescription());
            }
            Console.ReadKey();

            Random rnd = new Random();
            var staedteToExclude = staedteInLand.Where(s => rnd.NextDouble() < 0.33);
            
            Console.WriteLine("Auszuschließende Städte");
            staedteToExclude.ToList().ForEach(Console.WriteLine);

            var otherStaedte =
                (from stadt in landToQuery.Staedte
                 where !staedteToExclude.Any(s => s == stadt)
                 select stadt
                ).ToArray();


            Console.WriteLine("Restliche Städte");
            otherStaedte.ToList().ForEach(Console.WriteLine);

            var persons =
            (
            from stadt in otherStaedte
            from einw in stadt.Einwohner
            orderby einw.Familienname
            select einw)
             .Distinct();

            Console.WriteLine("Bewohner der restlichen Städte");
            persons.ToList().ForEach(Console.WriteLine);

            Console.ReadKey();

        }
    }
}
