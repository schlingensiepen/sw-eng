using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using BibliothekLib;
using BibliothekWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BibliothekWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private ApplicationUserManager _UserManager;

        private ApplicationUserManager UserManager =>
            _UserManager ?? (_UserManager =
                HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>());

        private BibliothekLib.BibliothekModelContainer _Context;

        private BibliothekLib.BibliothekModelContainer Context =>
            _Context ?? (_Context = new BibliothekModelContainer());

        public IEnumerable<string> createUser(string[] data)
        {
            data = data.Select(d => d.Trim()).ToArray();
            return createUser(
                data[0], data[1], data[2], data[3], data[4], data[5],
                data[6].Contains("A"), data[6].Contains("M"), data[6].Contains("K"));
        }

        public IEnumerable<string> createUser(
            string password,
            string name,
            string familyName,
            string street,
            string postCode,
            string city,
            bool roleAdministrator,
            bool roleStaff,
            bool roleCostumer)
        {
            User user = new User()
            {
                City = city,
                EMail =
                    name.ToLower() + "." + familyName.ToLower() +
                    "@" + (roleCostumer ? familyName.ToLower() + ".de" : "bibliothek.com"),
                Familyname = familyName,
                Name = name,
                PostCode = postCode,
                Street = street,
                RoleAdministrator = roleAdministrator,
                RoleStaff = roleStaff,
                RoleCostumer = roleCostumer
            };
            yield return "Try adding user: " + user;

            if (Context.Users.Any(s => s.EMail == user.EMail))
            {
                yield return "EMail schon vergeben!";
                yield break;
            }
            try
            {
                var appUser = new ApplicationUser
                {
                    UserName = user.EMail,
                    Email = user.EMail
                };
                UserManager.Create(appUser, password);
                Context.Users.Add(user);
            }
            catch (Exception ex)
            {
                yield break;
            }
            yield return "User created " + user;
        }

        public IEnumerable<string> createBook(string[] data)
        {
            data = data.Select(d => d.Trim()).ToArray();
            return createBook(data[0], data[1], data[2],
                data[3].Substring(0, 2) + "-" + data[3].Substring(2, 2),
                data[3].Split('-').ToList().Skip(1).Take(3).ToArray());
        }

        public IEnumerable<string> createBook(
            string titel,
            string author,
            string verlag,
            string standort,
            string[] mediaNumbers)
        {
            Work work = new Work()
            {
                Author = author,
                Standort = standort,
                Titel = titel,
                Verlag = verlag
            };
            yield return "Create Book " + work;

            if (Context.Works.Any(w => w.Titel.Equals(titel)))
            {
                yield return "Book already exist " + work;
                yield break;
            }

                foreach (var media in mediaNumbers
                    .Select(m => new Media() {Number = m, Work = work}))
                {
                    work.Media.Add(media);
                    yield return "Create Media " + media;
                }
                Context.Works.Add(work);
                Context.Media.AddRange(work.Media);


            yield return "Book Created " + work;
        }

        public ActionResult ClearAll()
        {
            foreach (var user in Context.Users)
            {
                user.Orders.Clear();
                user.Loans.Clear();
            }
            Context.SaveChanges();
            foreach (var work in Context.Works)
            {
                work.Media.Clear();
                work.Orderer.Clear();
            }
            Context.SaveChanges();

            Context.Media.RemoveRange(Context.Media);
            Context.SaveChanges();

            Context.Users.RemoveRange(Context.Users);
            Context.SaveChanges();

            Context.Works.RemoveRange(Context.Works);
            Context.SaveChanges();

            foreach (var loginUser in UserManager.Users.ToArray())
            {
                try
                {
                    UserManager.Delete(loginUser);
                }
                catch (Exception ex)
                {
                }
            }
            Context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Setup()
        {
            string usersData = @"
Qk7JCv5!qa!F;Anna;Administrator;Gelbe Gasse 3;16928;Kuhbier;AK
Qk7JCv5!qa!F;Anton;Administrator;Hohe Gasse 3;63589;Linsengericht;AM
Qk7JCv5!qa!F;Minna;Mitarbeiter;Breite Straße 8;21709;Himmelspforten;M
Qk7JCv5!qa!F;Marcel;Mitarbeiter;Feldstraße 8;65368;Katzenelnbogen;MK
Qk7JCv5!qa!F;Michaela;Mitarbeiter;Feldstraße 8;76889;Steinfeld;M
Qk7JCv5!qa!F;Karl;Kunde;Ackerstraße 25;88430;Rot an der Rot;K
Qk7JCv5!qa!F;Katja;Kunde;Dieselstraße 11;85276;Pfaffenhofen;K
Qk7JCv5!qa!F;Karlheinz;Kunde;Im Grund 56;03139;Schwarze Pumpe;K
Qk7JCv5!qa!F;Klaus;Kunde;Am gelben Hang 4;36110;Schlitz;K
Qk7JCv5!qa!F;Kurt;Kunde;Poststraße 3;25495;Kummerfeld;K
Qk7JCv5!qa!F;Klaus P.;Kunde;Saueressigallee 8;55595;Weinsheim;K
                ";

            string bookData = @"
Die Geschichte der Bienen;Maja Lunde;btb;ad7eccb2-1fb5-4799-b557-56993a9c5430
Selfies;Jussi Adler-Olsen;dtv;98defd7c-8c6d-41ee-9d46-16c63abea567
Die fremde Königin;Rebecca Gablé;Bastei Lübbe;466abfbb-61fe-46aa-a7cc-808bd8c43ffe
Der grosse Polt;Gerhard Polt;Kein & Aber;3b1bdacb-2e17-4a94-acda-432d2329650a
Grand Prix;Martin Walker;Diogenes;c99e3a1f-6111-4ac3-8659-2100c8578be1
Meine geniale Freundin;Elena Ferrante;Suhrkamp;3461e06f-f9ac-4d59-adaf-fbd9e6af2361
Das Labyrinth der Lichter;Carlos Ruiz Zafón;S. Fischer;d0cdabd7-0f07-43b9-8be7-a35a620ff928
Elefant;Martin Suter;Diogenes;58e0c93c-0841-42d3-9b1a-03a3dd731fa2
Der Lärm der Zeit;Julian Barnes;Kiepenheuer & Witsch;f52b412c-c86c-406c-8299-9d3cc97fcf16
Die Geschichte eines neuen Namens;Elena Ferrante;Suhrkamp;62c2c3a4-5678-4989-9711-812662f06642
Sie kam aus Mariupol;Natascha Wodin;Rowohlt;85b12bb7-ad01-4dbe-86bc-88dec6c0c1bf
Das Paket;Sebastian Fitzek;Droemer;f64c9304-73b0-4771-905f-42b3e05cda6a
Schlafen werden wir später;Zsuzsa Bánk;S. Fischer;078221c3-3b6f-4fb6-9ff9-c72cdb529c39
Bestechung;John Grisham;Heyne;631a7a73-f088-4ce6-805a-5d7ebff9d6cb
Stille Wasser;Donna Leon;Diogenes;df653603-e80e-481c-9b9a-7dc85c13e5e1
Gott, hilf dem Kind;Toni Morrison;Rowohlt;76191f11-f045-4ef9-83ac-a8b00934f86b
Ein wenig Leben;Hanya Yanagihara;Hanser Berlin;b4e0b5e9-3824-4cfb-b1d4-9045dae63229
Kämpfen;Karl Ove Knausgård;Luchterhand;936fe20b-5f01-47ec-9da5-717d0c1ea78f
Die Schattenschwester;Lucinda Riley;Goldmann;74caa60c-33f3-463d-b9f8-a665424794c0
Unterleuten;Juli Zeh;Luchterhand;fbee6e67-ed9e-445c-97a1-58f85af0a0e0
Heilen mit der Kraft der Natur;Prof. Dr. Andreas Michalsen;Insel;6ca2dc8e-fb2b-4460-9d0b-3d17d2281048
Das geheime Leben der Bäume;Peter Wohlleben;Ludwig;94d9a8ba-f564-4d00-a3ad-9de6c837ceae
Wunder wirken Wunder;Dr. med. Eckart von Hirschhausen;Rowohlt;d4260e23-c482-4396-99c0-1f06d2271b15
Penguin Bloom;Cameron Bloom und Bradley Trevor Greive;Knaus;62649d67-265e-48cc-a924-77bd2ea8b887
Homo Deus;Yuval Noah Harari;C.H. Beck;12b4617d-c43e-49c8-862d-a8bbd9e5871f
Alexander von Humboldt und die Erfindung der Natur;Andrea Wulf;C. Bertelsmann;11fbcaec-c28a-4500-943c-23c939485757
Die Getriebenen;Robin Alexander;Siedler;edaf22e9-6c96-4d80-b1d1-e16715158716
Wer wir waren;Roger Willemsen;S. Fischer;82f4fc40-51fe-42fb-8fe6-26b733bbbe26
Hillbilly-Elegie;J. D. Vance;Ullstein;e7ab950c-1abf-48e0-9c61-6756d882b86d
Ohne Liebe trauern die Sterne;Hannelore Hoger;Rowohlt;322b77c3-66e8-4369-9fa7-226e17d89360
Der rebellische Mönch, die entlaufene Nonne und der größte Bestseller aller Zeiten;Christian Nürnberger und Petra Gerster;Gabriel;2267a45d-73b8-47b6-ac00-8cf516fb51a7
Die verschleierte Gefahr;Zana Ramadani;Europa;d59b0700-f998-41cc-a6c6-8dccb3c7d107
Wer der Herde folgt, sieht nur Ärsche;Hannes Jaenicke;Gütersloher Verlagshaus;195268d8-ef76-46d7-ad84-1e1df4fa112c
Der erste Stein;Krzysztof Charamsa;C. Bertelsmann;8c45d476-9fcf-443a-8e39-5eda1b957bed
Sie werden lachen;Katrin Weber;Aufbau;3a57cb67-fe6d-4dcb-b199-b49592c27cab
Ist der Islam noch zu retten?;Hamed Abdel-Samad und Mouhanad Khorchide;Droemer;81c291d3-e40f-4491-a2f8-4a7e01e682e5
Mein Bruder Che;Juan Martín Guevara und Armelle Vincent;Tropen;432fef61-af2e-4f48-825d-5839b1b8f27b
Das Seelenleben der Tiere;Peter Wohlleben;Ludwig;f5a7527a-bb49-4b90-9eef-7a5ff9c11039
Maria Theresia;Barbara Stollberg-Rilinger;C.H. Beck;ca38a7b8-49d2-4941-af43-32bc6735e745
                ";

            IEnumerable<string>[] actions = new[]
            {
                usersData
                    .Split(Environment.NewLine.ToCharArray())
                    .Where(line => !String.IsNullOrWhiteSpace(line.Trim()))
                    .SelectMany(line => createUser(line.Split(';'))),
                bookData
                    .Split(Environment.NewLine.ToCharArray())
                    .Where(line => !String.IsNullOrWhiteSpace(line.Trim()))
                    .SelectMany(line => createBook(line.Split(';'))),
            };
            ViewBag.Messages = actions.SelectMany(a => a).ToList();
            Context.SaveChanges();
            return View();
        }
    }
}