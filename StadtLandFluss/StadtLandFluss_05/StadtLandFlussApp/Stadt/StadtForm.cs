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
    public partial class StadtForm : Form
    {
        public StadtForm()
        {
            InitializeComponent();
        }

        public DataStore Data;
        
        public string ObjName
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        private Land _GelegenIn;
        public Land GelegenIn{
            get => _GelegenIn;
            set
            {
                _GelegenIn = value;
                textBox3.Text = value.Name;
            }
        }

        private IEnumerable<Fluss> GelegenAn
        {
            set
            {
                listBox1.Items.Clear();
                listBox1.Items.AddRange(value.ToArray());
            }
            get
            {
                List<Fluss> res = new List<Fluss>();
                foreach (var elem in listBox1.Items)
                {
                    res.Add(elem as Fluss);
                }
                return res;
            }
        }


        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            var selectedFluesse = SelectObjectForm.selectObjects(
                "Flüsse auswählen",
                Data.Fluesse);
            if (selectedFluesse == null) return;
            listBox1.Items.AddRange(selectedFluesse.ToArray());
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
            Land selectedLand = SelectObjectForm.selectObject(
                "Geburtsort auswählen",
                Data.Laender) as Land;
            if (selectedLand == null) return;
            GelegenIn = selectedLand;
        }

        private void PersonForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        public static void updateStadt(
            DataStore data,
            Stadt stadt)
        {
            StadtForm form = new StadtForm();
            form.Data = data;
            form.Text = stadt.getShowTitle();

            form.ObjName = stadt.Name;
            form.GelegenIn = stadt.LiegtIn;
            form.GelegenAn = stadt.LiegtAn;

            if (form.ShowDialog() != DialogResult.OK) return;

            stadt.Name = form.ObjName;
            stadt.LiegtIn = form.GelegenIn;
            stadt.LiegtAn.Clear();
            form.GelegenAn.ToList().ForEach(stadt.LiegtAn.Add);
        }


        public static Stadt createStadt(
            DataStore data,
            string caption)
        {
            StadtForm form = new StadtForm();
            form.Data = data;
            form.Text = caption;
            if (form.ShowDialog() != DialogResult.OK) return null;

            return new StadtBuilder()
                .setName(form.ObjName)
                .setLiegtIn(form.GelegenIn)
                .addLiegtAn(form.GelegenAn)
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

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
