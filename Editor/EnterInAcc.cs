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
    public partial class EnterInAcc : Form
    {
        private Form form = new Form();
        public EnterInAcc(Form form)
        {
            this.form = form;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            if (textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Заполните пустые поля");
                return;
            }

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `nickname` = @nick AND `password` = @pass", db.getConnection());
            command.Parameters.Add("@nick", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBox2.Text;

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            if (table.Rows.Count != 0)
            {
                Form1 newForm = new Form1(Convert.ToInt32(table.Rows[0].ItemArray[0]));
                newForm.Show();

                form.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверно введены имя аккаунта или пароль");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registr newForm = new Registr();
            newForm.Show();
        }
    }
}
