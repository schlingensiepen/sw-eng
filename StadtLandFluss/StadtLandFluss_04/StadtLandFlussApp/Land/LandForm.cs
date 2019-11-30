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
    public partial class LandForm : Form
    {
        public LandForm()
        {
            InitializeComponent();
        }

        public DataStore Data;
        
        public string ObjName
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        private Stadt _HauptStadt;
        public Stadt HauptStadt
        {
            get => _HauptStadt;
            set
            {
                _HauptStadt = value;
                textBox3.Text = value.Name;
            }
        }


        private void TextBox3_DoubleClick(object sender, EventArgs e)
        {
            Stadt selectedStadt = SelectObjectForm.selectObject(
                "Geburtsort auswählen",
                Data.Staedte) as Stadt;
            if (selectedStadt == null) return;
            HauptStadt = selectedStadt;
        }

        private void PersonForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        public static void updateLand(
            DataStore data,
            Land land)
        {
            LandForm form = new LandForm();
            form.Data = data;
            form.Text = land.Name;

            form.ObjName = land.Name;
            form.HauptStadt = land.HauptStadt;

            if (form.ShowDialog() != DialogResult.OK) return;

            land.Name = form.ObjName;
            land.HauptStadt = form.HauptStadt;
        }


        public static Land createLand(
            DataStore data,
            string caption)
        {
            LandForm form = new LandForm();
            form.Data = data;
            form.Text = caption;

            if (form.ShowDialog() != DialogResult.OK) return null;

            return new LandBuilder()
                .setName(form.ObjName)
                .setHauptStadt(form.HauptStadt)
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
