using StadtLandFlussLibDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StadtLandFlussApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataStore Data = new DataStore();
        DemoData DemoData = new DemoData();

        private void Button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(DemoData.Otto.getDescription());
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine(DemoData.Erwin.getDescription());
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine(DemoData.Jan.getDescription());            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine(DemoData.Klaas.getDescription());            
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Console.WriteLine(DemoData.Hein.getDescription());            
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Console.WriteLine(DemoData.Pitt.getDescription());            
        }

        private void Button7_Click(object sender, EventArgs e)
        {
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


            Console.WriteLine("Fill Database");
            try
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    context.Fluesse.AddRange(DemoData.Fluesse);
                    context.SaveChanges();

                    DemoData.Deutschland.HauptStadt = null;
                    context.Laender.AddRange(DemoData.Laender);
                    context.SaveChanges();

                    context.Staedte.AddRange(DemoData.Staedte);
                    context.SaveChanges();

                    DemoData.Deutschland.HauptStadt = DemoData.Berlin;
                    context.SaveChanges();


                    context.People.AddRange(DemoData.Personen);
                    context.SaveChanges();

                    dbContextTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Data.People.ToList().ForEach(p => Console.WriteLine(p.getDescription() + "\n"));
            Data.NamedObjects.ToList().Select(n =>
            {
                Console.WriteLine(n.getDescription() + "\n");
                return "";
            });
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Person personToDelete =
                SelectObjectForm.selectObject("Lösche Person", Data.People) as Person;
            if (personToDelete == null) return;

            Data.People.Remove(personToDelete);
            Data.SaveChanges();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                Person newPerson =
                    PersonForm.createPerson(
                        Data,
                        "Neue Person erstellen...");
                Data.People.Add(newPerson);
                Data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            foreach(var person in Data.People)
            {
                Console.WriteLine(person.getFullname());
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            Console.WriteLine(
            string.Join("\n",
                Data.Staedte.ToList().Select(p => p.getDescription())));
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            Data.Laender.ToList().ForEach(
                l => Console.WriteLine(l.getDescription()));
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            Data.Fluesse.ToList().Select(
                f => f.getDescription())
                .ToList()
                .ForEach(Console.WriteLine);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            try
            {
                Person personToUpdate =
                SelectObjectForm.selectObject("Ändere Person", Data.People) as Person;
                if (personToUpdate == null) return;
                PersonForm.updatePerson(Data, personToUpdate);
                Data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            try
            {
                Stadt newStadt =
                    StadtForm.createStadt(Data, "Neue Stadt anlegen...");
                if (newStadt == null) return;
                Data.Staedte.Add(newStadt);
                Data.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            try
            {
                Land newLand =
                    LandForm.createLand(Data, "Neues Land anlegen...");
                if (newLand == null) return;
                Data.Laender.Add(newLand);
                Data.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            try
            {
                Fluss newFluss =
                    FlussForm.createFluss(Data, "Neuen Fluß anlegen...");
                if (newFluss == null) return;
                Data.Fluesse.Add(newFluss);
                Data.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            try
            {
                Stadt stadtToUpdate =
                SelectObjectForm.selectObject("Ändere Person", Data.Staedte) as Stadt;
                if (stadtToUpdate == null) return;
                StadtForm.updateStadt(Data, stadtToUpdate);
                Data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            try
            {
                Land landToUpdate =
                SelectObjectForm.selectObject("Ändere Land", Data.Laender) as Land;
                if (landToUpdate == null) return;
                LandForm.updateLand(Data, landToUpdate);
                Data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            try
            {
                Fluss flussToUpdate =
                SelectObjectForm.selectObject("Ändere Fluß", Data.Laender) as Fluss;
                if (flussToUpdate == null) return;
                FlussForm.updateFluss(Data, flussToUpdate);
                Data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            Stadt stadtToDelete =
                SelectObjectForm.selectObject("Lösche Stadt", Data.Staedte) as Stadt;
            if (stadtToDelete == null) return;

            Data.Staedte.Remove(stadtToDelete);
            Data.SaveChanges();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            Land landToDelete =
                SelectObjectForm.selectObject("Lösche Land", Data.Laender) as Land;
            if (landToDelete == null) return;

            Data.Laender.Remove(landToDelete);
            Data.SaveChanges();
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            Fluss flussToDelete =
                SelectObjectForm.selectObject("Lösche Fluss", Data.Fluesse) as Fluss;
            if (flussToDelete == null) return;

            Data.Fluesse.Remove(flussToDelete);
            Data.SaveChanges();
        }

        private void Button25_Click(object sender, EventArgs e)
        {
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
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            Land landToQuery =
                SelectObjectForm.selectObject("Wähle Land", Data.Laender.ToList()) as Land;
            if (landToQuery == null) return;

            Console.WriteLine("List People living in country " + landToQuery.Name);
            var personen =
                (
                from stadt in landToQuery.Staedte
                from einw in stadt.Einwohner             
                orderby einw.Familienname
                select einw
                )
            .Distinct();

            foreach (Person person in personen)
            {
                Console.WriteLine(person.getFullname());
            }
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            Fluss flussToQuery =
                SelectObjectForm.selectObject("Wähle Fluß", Data.Fluesse) as Fluss;
            if (flussToQuery == null) return;

            var staedte =
            from stadt in flussToQuery.Durchfließt
            select stadt;

            foreach (var stadt in staedte)
            {
                Console.WriteLine(stadt.getDescription());
            }

            Console.WriteLine("List People living next to first river");


            var personen =
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

        }

        private void Button28_Click(object sender, EventArgs e)
        {
            Land landToQuery =
               SelectObjectForm.selectObject("Land wählen ...", Data.Laender.ToList()) as Land;
            if (landToQuery == null) return;

            var staedteToExclude =
                SelectObjectForm.selectObjects("Städte wählen",
                landToQuery.Staedte.ToList()
                ).Select(o => o as Stadt).Where(p => p != null).ToList();

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



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Data = new DataStore();
        }
    }
}
