using BibliothekLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using de.i2cm.helper.HelperLib.Configuration;
using de.i2cm.helper.HelperLib.Forms;

namespace BibliothekApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User()
                {
                    Name = "Otto",
                    Familyname = "Mustermann",
                    Street = "Musterstraße",
                    PostCode = "12345",
                    City = "Ingolstadt"
                };
                if (ConfigForm.showDialog("Neuer User",
                    user, true) == DialogResult.OK)
                {
                    using (BibliothekModelContainer container =
                        new BibliothekModelContainer())
                    {
                        container.UserSet.Add(user);
                        container.SaveChanges();
                    }
                }
            }
            catch (Exception ex)

            {
                handle(ex);
            }
        }

        private void handle(Exception exception)
        {
            string msg =
                Environment.NewLine +
                Environment.NewLine +
                @"Copy to Clipboard?";

            Exception ex = exception;
            string report = "";
            while (ex != null)
            {
                report += 
                    String.Format("{0}{1}", exception, Environment.NewLine);
                ex = ex.InnerException;
            }
            DbEntityValidationException valEx =
                exception as DbEntityValidationException;
            if (valEx != null)
            {
                msg = "Validation Error" + msg;
                report += String.Format("{0}Validation errors:{0}{1}",
                    Environment.NewLine,
                    string.Join(
                        Environment.NewLine,
                        valEx.EntityValidationErrors.Select(
                            err => err.Entry.Entity.GetType().Name +
                                   string.Join(Environment.NewLine,
                                       err.ValidationErrors.Select(
                                           v => $"{v.PropertyName} - {v.ErrorMessage}")))));

            }
            else
            {
                msg = "Error" + msg;
            }

            report = report.Replace(":", ":" + Environment.NewLine + "    ");
            report = report.Replace("--->", Environment.NewLine + "--->");

            Trace.WriteLine(report);
            if (MessageBox.Show(msg, @"Error",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Clipboard.SetText(report);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Work work = new Work()
                {
                    Titel = "Titel",
                    Author = "Ernest Hemmingway",
                    Verlag = "dtv",
                    Standort = "K/R13"
                };
                if (ConfigForm.showDialog("Neues Werk",
                    work, true) == DialogResult.OK)
                {
                    using (BibliothekModelContainer container =
                        new BibliothekModelContainer())
                    {
                        container.WorkSet.Add(work);
                        container.SaveChanges();
                    }
                }
            }
            catch (Exception ex)

            {
                handle(ex);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {

                    Work work = SelectDialog<Work>.show(
                        "Werkauswahl",
                        "Werk für neues Medium auswählen.",
                        null,
                        container.WorkSet);
                    if (work == null) return;

                    Media media = new Media()
                    {
                        Number = Guid.NewGuid()
                            .ToString()
                            .Replace("-", string.Empty)
                            .Substring(0, 6)
                    };
                    media.Work = work;
                    if (ConfigForm.showDialog("Neues Medium",
                        media, true) == DialogResult.OK)
                    {
                        container.MediaSet.Add(media);
                        container.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    User user = SelectDialog<User>.show(
                        "Bestellerauswahl",
                        "Besteller auswählen",
                        null,
                        container.UserSet);
                    if (user == null) return;

                    Work work = SelectDialog<Work>.show(
                        "Werkauswahl",
                        "Werk zur Bestellung auswählen.",
                        null,
                        container.WorkSet);
                    if (work == null) return;

                    work.Orderer.Add(user);
                    container.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    User user = SelectDialog<User>.show(
                        "Bestellerauswahl",
                        "Besteller auswählen",
                        null,
                        container.UserSet);
                    if (user == null) return;
                    SelectDialog<Work>.show(
                        "Bestellungen",
                        $"Bestellungen von {user}",
                        null,
                        user.Orders);
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    Work work = SelectDialog<Work>.show(
                        "Werkauswahl",
                        "Werk auswählen",
                        null,
                        container.WorkSet);
                    if (work == null) return;
                    SelectDialog<User>.show(
                        "Bestellungen",
                        $"Bestellungen für {work}",
                        null,
                        work.Orderer);
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    User user = SelectDialog<User>.show(
                        "Ausleiherwahl",
                        "Ausleiher auswählen",
                        null,
                        container.UserSet);
                    if (user == null) return;

                    // Hier sollten nur Medien gewählt werden können,
                    // die auch da sind, also keinen Ausleiher haben.
                    var medias = (from m in container.MediaSet
                        where m.User == null
                        select m);

                    Media media = SelectDialog<Media>.show(
                        "Medium",
                        "Medium zum Leihen auswählen.",
                        null,
                        medias);
                    if (media == null) return;

                    user.Loans.Add(media);
                    user.Orders.Remove(media.Work);
                    container.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    User user = SelectDialog<User>.show(
                        "Ausleiherwahl",
                        "Ausleiher auswählen",
                        null,
                        container.UserSet);
                    if (user == null) return;

                    Media media = SelectDialog<Media>.show(
                        "Medium",
                        "Medium zum Leihen auswählen.",
                        null,
                        user.Loans);
                    if (media == null) return;

                    user.Loans.Remove(media);
                    container.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    User user = SelectDialog<User>.show(
                        "Ausleiherwahl",
                        "Ausleiher auswählen",
                        null,
                        container.UserSet);
                    if (user == null) return;

                    Media media = SelectDialog<Media>.show(
                        "Medium",
                        "Medium zum Leihen auswählen.",
                        null,
                        user.Loans);
                }
            }
            catch
                (Exception ex)
            {
                handle(ex);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    Work work = SelectDialog<Work>.show(
                        "Werkauswahl",
                        "Werk auswählen",
                        null,
                        container.WorkSet);
                    if (work == null) return;

                    var users = (
                        from m in work.Media
                        where m.User != null
                        select m.User
                        );

                    SelectDialog<User>.show(
                        "Ausgeliehen Werke",
                        $"Ausleiher für {work}",
                        null,
                        users);
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    User user = SelectDialog<User>.show(
                        "Kundenauswahl",
                        "Kunden auswählen",
                        null,
                        container.UserSet);
                    if (user == null) return;

                    if (ConfigForm.showDialog("Kundenänderung",
                        user, true) != DialogResult.OK) return;

                    container.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    User user = SelectDialog<User>.show(
                        "Kundenauswahl",
                        "Kunden auswählen",
                        null,
                        container.UserSet);
                    if (user == null) return;
                    container.UserSet.Remove(user);
                    container.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    Work work = SelectDialog<Work>.show(
                        "Werkauswahl",
                        "Werk auswählen",
                        null,
                        container.WorkSet);
                    if (work == null) return;

                    if (ConfigForm.showDialog("Werkänderung",
                        work, true) != DialogResult.OK) return;

                    container.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    Work work = SelectDialog<Work>.show(
                        "Werkauswahl",
                        "Werk auswählen",
                        null,
                        container.WorkSet);
                    if (work == null) return;
                    container.WorkSet.Remove(work);
                    container.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    Media media = SelectDialog<Media>.show(
                        "Medienauswahl",
                        "Medium auswählen",
                        null,
                        container.MediaSet);
                    if (media == null) return;

                    if (ConfigForm.showDialog("Werkänderung",
                        media, true) != DialogResult.OK) return;

                    container.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }


        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    Media media = SelectDialog<Media>.show(
                        "Medienauswahl",
                        "Medium auswählen",
                        null,
                        container.MediaSet);
                    if (media == null) return;
                    container.MediaSet.Remove(media);
                    container.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                using (BibliothekModelContainer container =
                    new BibliothekModelContainer())
                {
                    foreach (Work work in (from w in container.WorkSet
                        where w.Orderer.Count() > 0
                              && w.Media.Count(m => m.User == null) > 0
                        select w))
                    {
                        MessageBox.Show(
                            $"Das bestellte Werk{Environment.NewLine}" +
                            $"{work}{Environment.NewLine}" +
                            $"ist verfügbar.{Environment.NewLine}" +
                            $"Besteller:{Environment.NewLine}" +
                            string.Join(Environment.NewLine,
                                work.Orderer.Select(u => u.ToString())),
                            @"Bestelltes Werk verfügbar");
                    }
                }
            }
            catch (Exception ex)
            {
                handle(ex);
            }
        }
    }
}

    

