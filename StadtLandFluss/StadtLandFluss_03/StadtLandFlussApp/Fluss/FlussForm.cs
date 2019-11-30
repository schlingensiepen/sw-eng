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
    public partial class FlussForm : Form
    {
        public FlussForm()
        {
            InitializeComponent();
        }

        public DataStore Data;
        
        public string ObjName
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }


        private void PersonForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        public static void updateFluss(
            DataStore data,
            Fluss fluss)
        {
            FlussForm form = new FlussForm();
            form.Data = data;
            form.Text = fluss.Name;

            form.ObjName = fluss.Name;
            form.Male = fluss.Male;

            if (form.ShowDialog() != DialogResult.OK) return;

            fluss.Name = form.ObjName;
            fluss.Male = form.Male;
        }


        public static Fluss createFluss(
            DataStore data,
            string caption)
        {
            FlussForm form = new FlussForm();
            form.Data = data;
            form.Text = caption;
            if (form.ShowDialog() != DialogResult.OK) return null;

            return new FlussBuilder()
                .setName(form.ObjName)
                .setMale(form.Male)
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

        public bool Male
        {
            get
            {
                return (comboBox1.SelectedIndex == 0);
            }
            set
            {
                comboBox1.SelectedIndex = (value ? 0 : 1);
            }
        }

        private void updateDisplay()
        {
            textBox3.Text = (Male ? "Der " : "Die ") + textBox1.Text;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateDisplay();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            updateDisplay();
        }
    }
}
