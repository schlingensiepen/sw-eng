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
    public partial class PersonForm : Form
    {
        public PersonForm()
        {
            InitializeComponent();
        }

        public DataStore Data;
        
        public string Vorname
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }
        public string Nachname
        {
            get => textBox2.Text;
            set => textBox2.Text = value;
        }

        private Stadt _GeburtsOrt;
        public Stadt GeburtsOrt {
            get => _GeburtsOrt;
            set
            {
                _GeburtsOrt = value;
                textBox3.Text = value.Name;
            }
        }

        public IEnumerable<Stadt> Wohnorte
        {
            set
            {
                listBox1.Items.Clear();
                listBox1.Items.AddRange(value.ToArray());
            }
            get
            {
                List<Stadt> res = new List<Stadt>();
                foreach (var elem in listBox1.Items)
                {
                    res.Add(elem as Stadt);
                }
                return res;
            }
        }


        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            var selectedStaedte = SelectObjectForm.selectObjects(
                "Wohnorte auswählen",
                Data.Staedte);
            if (selectedStaedte == null) return;
            listBox1.Items.AddRange(selectedStaedte.ToArray());
        }

        private void ListBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var sel = listBox1.SelectedItem;
                if (sel != null)
                    listBox1.Items.Remove(sel);
            }
        }

        private void TextBox3_DoubleClick(object sender, EventArgs e)
        {
            Stadt selectedStadt = SelectObjectForm.selectObject(
                "Geburtsort auswählen",
                Data.Staedte) as Stadt;
            if (selectedStadt == null) return;
            GeburtsOrt = selectedStadt;
        }

        private void PersonForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        public static void updatePerson(
            DataStore data,
            Person person)
        {
            PersonForm form = new PersonForm();
            form.Data = data;
            form.Text = person.getFullname();

            form.Vorname = person.Vorname;
            form.Nachname = person.Familienname;
            form.GeburtsOrt = person.GeborenIn;
            form.Wohnorte = person.WohntIn;

            if (form.ShowDialog() != DialogResult.OK) return;

            person.Vorname = form.Vorname;
            person.Familienname = form.Nachname;
            person.GeborenIn = form.GeburtsOrt;
            person.WohntIn.Clear();
            person.WohntIn.AddRange(form.Wohnorte);
        }


        public static Person createPerson(
            DataStore data,
            string caption)
        {
            PersonForm form = new PersonForm();
            form.Data = data;
            form.Text = caption;
            if (form.ShowDialog() != DialogResult.OK) return null;

            return new PersonBuilder()
                .setVorname(form.Vorname)
                .setNachname(form.Nachname)
                .setGeborenIn(form.GeburtsOrt)
                .addWohntIn(form.Wohnorte)
                .create();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
