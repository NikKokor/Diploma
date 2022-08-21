using MySql.Data.MySqlClient;
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
    public partial class ComboBoxDialog : Form
    {
        private TabControl tab = null;
        public ComboBoxDialog(string type, TabControl tab = null)
        {
            InitializeComponent();
            if (type == "etap")
            {
                this.tab = tab;
                getEtap(this.tab);
            }
            else
            {
                button3.Visible = false;
                getOsob(type);
            }
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
        }

        private void ComboBoxDialog_Load(object sender, EventArgs e)
        {

        }

        public string getText()
        {
            if (comboBox1.SelectedItem == null)
                return null;
            return comboBox1.SelectedItem.ToString();
        }

        private void getEtap(TabControl tab = null)
        {
            comboBox1.Items.Clear();

            List<TabPage> tabPages = new List<TabPage>();

            for(int i = 0; i < tab.TabCount; i++)
            {
                tabPages.Add(tab.TabPages[i]);
            }

            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT `name` FROM `vos_etap`", db.getConnection());

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            foreach (DataRow row in table.Rows)
            {
                var cells = row.ItemArray;
                bool flag = true;
                for (int i = 0; i < tabPages.Count; i++)
                {
                    if (cells[0].ToString() == tabPages[i].Text)
                        flag = false;
                }

                if (flag)
                    comboBox1.Items.Add(cells[0].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextDialog frm2 = new TextDialog();

            DialogResult dr = frm2.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                frm2.Close();
            }
            else if (dr == DialogResult.OK)
            {
                DB db = new DB();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                if (frm2.getText() == "")
                    MessageBox.Show("Введите не пустое название этапа");
                else
                {
                    if (db.searchByName("id_etap", "vos_etap", frm2.getText()) != -1)
                        MessageBox.Show("Этап с данным именем уже существует");
                    else
                    {
                        MySqlCommand command = new MySqlCommand("INSERT INTO `vos_etap` (`name`) VALUES (@nameEtap)", db.getConnection());
                        command.Parameters.Add("@nameEtap", MySqlDbType.VarChar).Value = frm2.getText();

                        db.openConnection();

                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Новый этап добавлен");
                            frm2.Close();
                        }

                        else
                        {
                            MessageBox.Show("Новый этап не добавлен");
                        }

                        db.closeConnection();

                        getEtap(this.tab);
                    }
                }
            }
        }

        public bool getOsob(string osob)
        {
            comboBox1.Items.Clear();
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT `str_value` FROM `vos_znach` WHERE `id_osob` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = db.searchByName("id_osob", "vos_osob", osob);

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            foreach (DataRow row in table.Rows)
            {
                var cells = row.ItemArray;
                comboBox1.Items.Add(cells[0].ToString());
            }

            if (table.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}
