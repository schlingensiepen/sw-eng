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
    public partial class SelectObjectForm : Form
    {
        public SelectObjectForm()
        {
            InitializeComponent();
        }

        private List<object> _Elements;
        public IEnumerable<object> Elements
        {
            get
            {
                return _Elements;
            }
            set
            {
                _Elements = new List<object>(value);
                listBox1.Items.AddRange(value.ToArray());
            }
        }
        
        private void SelectObjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Elements = new List<object>();
            foreach (var element in listBox1.SelectedItems)
            {
                _Elements.Add(element);
            }
        }

        private bool _MultiSelect = false;
        public bool MultiSelect
        {
            get
            {
                return _MultiSelect;
            }
            set
            {
                _MultiSelect = value;
                if (value)
                {
                    listBox1.SelectionMode = SelectionMode.MultiExtended;
                }
                else
                {
                    listBox1.SelectionMode = SelectionMode.One;
                }
            }
        }

        public static object selectObject
            (string caption, 
            IEnumerable<object> elementList)
        {
            SelectObjectForm form = new SelectObjectForm();
            form.Text = caption;
            form.Elements = elementList;
            form.MultiSelect = false;
            DialogResult res = form.ShowDialog();
            if (res != DialogResult.OK) return null;
            return form.Elements.FirstOrDefault();
        }
        public static IEnumerable<object> selectObjects
            (string caption,
            IEnumerable<object> elementList)
        {
            SelectObjectForm form = new SelectObjectForm();
            form.Text = caption;
            form.Elements = elementList;
            form.MultiSelect = true;
            DialogResult res = form.ShowDialog();
            if (res != DialogResult.OK) return null;
            return form.Elements;
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
