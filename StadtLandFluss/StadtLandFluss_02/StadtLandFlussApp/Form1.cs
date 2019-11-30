using StadtLandFlussLib;
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
            Data = new DataStore();
            Data.Staedte.AddRange(DemoData.Staedte);
            Data.Laender.AddRange(DemoData.Laender);
            Data.Fluesse.AddRange(DemoData.Fluesse);
            Data.Personen.AddRange(DemoData.Personen);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Data.Personen.ForEach(
                p => Console.WriteLine(p.getDescription() + "\n"));
            Data.NamedObjects.Select(n =>
            {
                Console.WriteLine(n.getDescription() + "\n");
                return "";
            });
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Person personToDelete =
                SelectObjectForm.selectObject(
                    "Lösche Person", 
                    Data.Personen) as Person;
            if (personToDelete == null) return;

            Data.Personen.Remove(personToDelete);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                Person newPerson =
                    PersonForm.createPerson(
                        Data,
                        "Neue Person erstellen...");
                Data.Personen.Add(newPerson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            foreach(var person in Data.Personen)
            {
                Console.WriteLine(person.getFullname());
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            Console.WriteLine(
            string.Join("\n",
                Data.Staedte.Select(p => p.getDescription())));
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            Data.Laender.ForEach(
                l => Console.WriteLine(l.getDescription()));
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            Data.Fluesse.Select(
                f => f.getDescription())
                .ToList()
                .ForEach(Console.WriteLine);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            try
            {
                Person personToUpdate =
                SelectObjectForm.selectObject(
                    "Ändere Person", 
                    Data.Personen) as Person;
                if (personToUpdate == null) return;
                PersonForm.updatePerson(Data, personToUpdate);
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
                    SelectObjectForm.selectObject(
                        "Ändere Person", 
                        Data.Staedte) as Stadt;
                if (stadtToUpdate == null) return;
                StadtForm.updateStadt(Data, stadtToUpdate);
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
                    SelectObjectForm.selectObject(
                        "Ändere Land", 
                        Data.Laender) as Land;
                if (landToUpdate == null) return;
                LandForm.updateLand(Data, landToUpdate);
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
                    SelectObjectForm.selectObject(
                        "Ändere Fluß", 
                        Data.Laender) as Fluss;
                if (flussToUpdate == null) return;
                FlussForm.updateFluss(Data, flussToUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            Stadt stadtToDelete =
                SelectObjectForm.selectObject(
                    "Lösche Stadt", 
                    Data.Staedte) as Stadt;
            if (stadtToDelete == null) return;

            Data.Staedte.Remove(stadtToDelete);
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            Land landToDelete =
                SelectObjectForm.selectObject(
                    "Lösche Land", 
                    Data.Laender) as Land;
            if (landToDelete == null) return;

            Data.Laender.Remove(landToDelete);
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            Fluss flussToDelete =
                SelectObjectForm.selectObject(
                    "Lösche Land", 
                    Data.Fluesse) as Fluss;
            if (flussToDelete == null) return;

            Data.Fluesse.Remove(flussToDelete);
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            var personen = (
                from person in Data.Personen
                where person.Vorname.Contains("tt")
                select person
                );

            foreach (Person person in personen)
            {
                Console.WriteLine(person.getFullname());
            }



            var wohnOrte =
                (from person in Data.Personen
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
            var personen =
            (from person in Data.Personen
             where person.WohntIn.Any(
                 wi => wi.LiegtIn == Data.Laender.FirstOrDefault())
             orderby person.Familienname
             select person);
            
            foreach (Person person in personen)
            {
                Console.WriteLine(person.getFullname());
            }
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            Fluss flussToQuery =
                SelectObjectForm.selectObject(
                    "Wähle Fluß", 
                    Data.Fluesse) as Fluss;
            if (flussToQuery == null) return;

            var staedte =
                (from stadt in Data.Staedte
                 where stadt.LiegtAn.Contains(flussToQuery)
                 select stadt);

            foreach(var stadt in staedte)
            {
                Console.WriteLine(stadt.getDescription());
            }

            var personen =
            (from person in Data.Personen
             where person.WohntIn.Any(
                 wi => wi.LiegtAn.Contains(flussToQuery))
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
               SelectObjectForm.selectObject(
                   "Land wählen ...", 
                   Data.Laender) as Land;
            if (landToQuery == null) return;

            var staedteToExclude =
                SelectObjectForm.selectObjects("Städte wählen",
                from stadt in Data.Staedte
                where stadt.LiegtIn == landToQuery
                select stadt
                ).Select(o => o as Stadt).Where(p => p != null);

            Console.WriteLine("Auszuschließende Städte");
            staedteToExclude.ToList().ForEach(Console.WriteLine);

            var otherStaedte =
                from stadt in
                    (
                        from aStadt in Data.Staedte
                        where aStadt.LiegtIn == landToQuery
                        select aStadt
                    )
                where !staedteToExclude.Contains(stadt)
                select stadt;

            Console.WriteLine("Restliche Städte");
            otherStaedte.ToList().ForEach(Console.WriteLine);

            var persons =
                from person in Data.Personen
                where person.WohntIn.Any(wi => otherStaedte.Contains(wi))
                select person;

            Console.WriteLine("Bewohner der restlichen Städte");
            persons.ToList().ForEach(Console.WriteLine);


        }
    }
}
