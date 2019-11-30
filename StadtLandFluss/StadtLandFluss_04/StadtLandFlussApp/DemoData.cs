using StadtLandFlussLibDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussApp
{
    class DemoData
    {
        public static Land Deutschland = new LandBuilder()
            .setName("Deutschland")
            .create();


        public static Fluss Ruhr = new FlussBuilder()
            .setName("Ruhr")
            .resetMale()
            .create();

        public static Fluss Spree = new FlussBuilder()
            .setName("Spree")
            .resetMale()
            .create();

        public static Fluss Havel = new FlussBuilder()
            .setName("Havel")
            .resetMale()
            .create();

        public static Fluss Main = new FlussBuilder()
            .setName("Main")
            .setMale()
            .create();

        public static Stadt Bielefeld = new StadtBuilder()
            .setName("Bielefeld")
            .setLiegtIn(Deutschland)
            .create();

        public static Stadt Muehlheim = new StadtBuilder()
            .setName("Mühlheim")
            .setLiegtIn(Deutschland)
            .addLiegtAn(Ruhr)
            .create();

        public static Stadt Essen = new StadtBuilder()
            .setName("Essen")
            .setLiegtIn(Deutschland)
            .addLiegtAn(Ruhr)
            .create();

        public static Stadt Berlin = new StadtBuilder()
            .setName("Berlin")
            .setLiegtIn(Deutschland)
            .addLiegtAn(Spree)
            .addLiegtAn(Havel)
            .create();

        public static Stadt Frankfurt = new StadtBuilder()
            .setName("Frankfurt")
            .setLiegtIn(Deutschland)
            .addLiegtAn(Main)
            .create();

        public static Person Otto = new PersonBuilder()
            .setVorname("Otto")
            .setNachname("Müller")
            .setGeborenIn(Bielefeld)
            .addWohntIn(Essen)
            .create();

        public static Person Erwin = new PersonBuilder()
            .setVorname("Erwin")
            .setNachname("Schmitt")
            .setGeborenIn(Bielefeld)
            .addWohntIn(Berlin)
            .create();

        public static Person Jan = new PersonBuilder()
            .setVorname("Jan")
            .setNachname("Meier")
            .setGeborenIn(Muehlheim)
            .addWohntIn(Berlin)
            .create();

        public static Person Klaas = new PersonBuilder()
            .setVorname("Klaas")
            .setNachname("Schulze")
            .setGeborenIn(Essen)
            .addWohntIn(Frankfurt)
            .create();

        public static Person Hein = new PersonBuilder()
            .setVorname("Hein")
            .setNachname("Blöd")
            .setGeborenIn(Berlin)
            .addWohntIn(Bielefeld)
            .create();

        public static Person Pitt = new PersonBuilder()
            .setVorname("Pitt")
            .setNachname("Fischer")
            .setGeborenIn(Frankfurt)
            .addWohntIn(Muehlheim)
            .addWohntIn(Frankfurt)
            .create();

        public DemoData()
        {
            Deutschland.HauptStadt = Berlin;
        }

        public IEnumerable<Fluss> Fluesse
        {
            get
            {
                yield return Ruhr;
                yield return Spree;
                yield return Havel;
                yield return Main;
            }
        }
        public IEnumerable<Stadt> Staedte
        {
            get
            {
                yield return Bielefeld;
                yield return Muehlheim;
                yield return Essen;
                yield return Berlin;
                yield return Frankfurt;
            }
        }

        public IEnumerable<Land> Laender
        {
            get
            {
                yield return Deutschland;
            }
        }

        public IEnumerable<Person> Personen
        {
            get
            {
                yield return Otto;
                yield return Erwin;
                yield return Jan;
                yield return Klaas;
                yield return Hein;
                yield return Pitt;
            }
        }
    }
}
