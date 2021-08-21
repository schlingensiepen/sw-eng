using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace de.i2cm.helper.HelperLib.Forms
{
    /// <summary>   Dialog for selecting an entry from a list. </summary>
    ///
    /// <remarks>   Jsp, 20.11.2015. </remarks>
    ///
    /// <typeparam name="TTag"> Type of the tag. </typeparam>

    public partial class SelectDialog<TTag> : Form
    {
        /// <summary>   Gets the selected tag after dialog is closed. </summary>
        ///
        /// <value> The selected tag. </value>

        public TTag SelectedTag { get; private set; }

        /// <summary>   Gets the cancel tag. Cancel tag is the default tag stored in selected tag after user ends dialog with cancle button.</summary>
        ///
        /// <value> The cancel tag. </value>

        public TTag CancelTag { get; private set; }

        /// <summary>   Helper class, representing a selectable item in a list shown in a SelectDialog </summary>
        ///
        /// <remarks>   Jsp, 20.11.2015. </remarks>

        public class SelectItem
        {
            private readonly string _Caption;

            /// <summary>   Gets the caption, this text is shown in the list </summary>
            ///
            /// <value> The caption. </value>

            public string Caption { get { return _Caption; } }

            private readonly string _Tooltip;

            /// <summary>   Gets the tooltip, this text is shown as tooltip of the list entry </summary>
            ///
            /// <value> The tooltip. </value>

            public string Tooltip { get { return _Tooltip; } }

            private readonly TTag _Tag;

            /// <summary>   Gets the tag, the tag represents the selected object </summary>
            ///
            /// <value> The tag. </value>

            public TTag Tag { get { return _Tag; } }

            /// <summary>   Constructor, assigning caption, tooltip and tag. </summary>
            ///
            /// <remarks>   Jsp, 20.11.2015. </remarks>
            ///
            /// <param name="caption">  The caption. </param>
            /// <param name="tooltip">  The tooltip. </param>
            /// <param name="tag">      The tag. </param>

            public SelectItem(string caption, string tooltip, TTag tag)
            {
                _Caption = caption;
                _Tooltip = tooltip;
                _Tag = tag;
            }

        }

        /// <summary>   Shows the select dialog, with assigned caption, text, cancel tag and selectable tags, function uses toString-method of tags to generate text and tooltip </summary>
        ///
        /// <remarks>   Jsp, 20.11.2015. </remarks>
        ///
        /// <param name="caption">      The caption. </param>
        /// <param name="text">         The text. </param>
        /// <param name="cancelTag">    The cancel tag. </param>
        /// <param name="items">        The items. </param>
        ///
        /// <returns>  In case user selects a list entry by double click or ends dialog by Ok button the selected tag, 
        ///            in case user ends dialog by Cancle button the cancleTag. </returns>

        public static TTag show(
            string caption,
            string text,
            TTag cancelTag,
            IEnumerable<TTag> items)
        {
            return show(caption, text, cancelTag,
                items.ToList().Select(item => new SelectItem(item.ToString(), item.ToString(), item)));
        }

        /// <summary>   Shows the select dialog, with assigned caption, text, cancel tag and selectable items</summary>
        ///
        /// <remarks>   Jsp, 20.11.2015. </remarks>
        ///
        /// <param name="caption">      The caption. </param>
        /// <param name="text">         The text. </param>
        /// <param name="cancelTag">    The cancel tag. </param>
        /// <param name="items">        The items </param>
        ///
        /// <returns>  In case user selects a list entry by double click or ends dialog by Ok button the tag of selected item,
        ///            in case user ends dialog by Cancle button the cancleTag. </returns>

        public static TTag show(
            string caption,
            string text,
            TTag cancelTag,
            IEnumerable<SelectItem> items)
        {
            SelectDialog<TTag> dlg = new SelectDialog<TTag>(caption, text, cancelTag, items);
            return dlg.ShowDialog() == DialogResult.OK ? dlg.SelectedTag : cancelTag;
        }
        /// <summary>   Constructor assigning caption, text, cancel tag and selectable items </summary>
        ///
        /// <remarks>   Jsp, 20.11.2015. </remarks>
        ///
        /// <param name="caption">      The caption. </param>
        /// <param name="text">         The text. </param>
        /// <param name="cancelTag">    The cancel tag. </param>
        /// <param name="items">        The items. </param>

        public SelectDialog(string caption, string text, TTag cancelTag, IEnumerable<SelectItem> items)
        {
            InitializeComponent();
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Text = caption;
            label1.Text = text;
            SelectedTag = cancelTag;
            CancelTag = cancelTag;
           foreach (var item in items)
           {
                listView1.Items.Add(new ListViewItem(item.Caption)
                {Tag = item.Tag, ToolTipText = item.Tooltip});
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                SelectedTag = CancelTag;
            }
            else
            {
                SelectedTag = (TTag)listView1.SelectedItems[0].Tag;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

    }
}
