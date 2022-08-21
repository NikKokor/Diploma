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
    public partial class MinMaxDialog : Form
    {
        public MinMaxDialog()
        {
            InitializeComponent();
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
        }

        public int[] getValue()
        {
            int[] value = { Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value) };
            return value;
        }
    }
}
