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
    public partial class TextDialog : Form
    {
        public TextDialog()
        {
            InitializeComponent();
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
        }

        public string getText()
        {
            return textBox1.Text;
        }

        public void setText(string text)
        {
            textBox1.Text = text;
        }
    }
}
