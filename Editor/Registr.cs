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
    public partial class Registr : Form
    {
        public Registr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Заполните пустые поля");
                return;
            }

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `nickname` = @nick", db.getConnection());
            command.Parameters.Add("@nick", MySqlDbType.VarChar).Value = textBox3.Text;

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Данное имя аккаунта уже занято");
                return;
            }

            command = new MySqlCommand("INSERT INTO `users` (`name`, `nickname`, `password`) VALUES (@name, @nick, @pass)", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@nick", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBox2.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт зарегистрирован");
                this.Close();
            }
            else
            {
                MessageBox.Show("Аккаунт не зарегистрирован");
            }

            db.closeConnection();
        }
    }
}
