using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace de.i2cm.helper.HelperLib.Configuration
{
    /// <summary>   Form for viewing a configuration. Includes a simple PropertyGrid, configuration object has to respect the rules for PropertyGrid viewable objects</summary>
    ///
    /// <remarks>   Jsp, 20.11.2015. </remarks>

    public partial class ConfigForm : Form
    {
        /// <summary>   Constructor, assigning a cation and a configuration object </summary>
        ///
        /// <remarks>   Jsp, 20.11.2015. </remarks>
        ///
        /// <param name="caption">              The caption. </param>
        /// <param name="configuration">        The configuration. </param>
        /// <param name="showToClipboardBtn">   Indicates if the button 'Copy to clipboard' ist shown.</param>

        public ConfigForm(string caption, object configuration, bool showToClipboardBtn = true)
        {
            InitializeComponent();
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Text = caption;
            configGrid.SelectedObject = configuration;
            if (!showToClipboardBtn)
                ToClipboardBtn.Visible = false;
        }

        /// <summary>   Shows the dialog by instanciating a  ConfigForm and execute ShowDialog on it.</summary>
        ///
        /// <remarks>   Jsp, 17.08.2016. </remarks>
        ///
        /// <param name="caption">              The caption. </param>
        /// <param name="configuration">        The configuration. </param>
        /// <param name="showToClipboardBtn"> Indicates if the button 'Copy to clipboard' ist shown. </param>
        ///
        /// <returns>   A DialogResult. </returns>

        public static DialogResult showDialog(string caption, object configuration, bool showToClipboardBtn = true)
        {
            ConfigForm dlg = new ConfigForm(caption, configuration, showToClipboardBtn);
            return dlg.ShowDialog();
        }

        /// <summary>   Event handler. Called by ToClipboardBtn for click events. Serialize configuration object from PropertyGrid to Clibboard as a XML text</summary>
        ///
        /// <remarks>   Jsp, 20.11.2015. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        private void ToClipboardBtn_Click(object sender, EventArgs e)
        {
            object obj = configGrid.SelectedObject;
            XmlSerializer ser = new XmlSerializer(obj.GetType());
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, obj);
            Clipboard.SetText(writer.ToString());
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            if (!Modal)
            {
                OkButton.Visible = false;
                CButton.Visible = false;
            }
        }
    }
}
