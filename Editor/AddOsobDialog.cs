using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    public partial class AddOsobDialog : Form
    {
        public AddOsobDialog()
        {
            InitializeComponent();
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
        }

        public string[] getText()
        {
            string[] text = new string[2];

            if (comboBox1.SelectedItem == null)
            { 
                text[0] = textBox1.Text;
                text[1] = null;
            }
            else
            {
                text[0] = textBox1.Text;
                text[1] = comboBox1.SelectedItem.ToString();
            }

            return text;
        }

        public void setText(string[] text)
        {
            textBox1.Text = text[0];
            comboBox1.SelectedItem = text[1];
        }
    }
}
