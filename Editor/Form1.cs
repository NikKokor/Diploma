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
    public partial class Form1 : Form
    {
        private int user;
        public Form1(int user)
        {
            this.user = user;
            InitializeComponent();
            getTehno(user);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextDialog frm2 = new TextDialog();

            DialogResult dr = frm2.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                frm2.Close();
            }
            else if (dr == DialogResult.OK)
            {
                if (frm2.getText() == "")
                    MessageBox.Show("Введите не пустое название технологии");
                else
                {
                    DB db = new DB();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM `tehnology`", db.getConnection());

                    db.openConnection();

                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    db.closeConnection();

                    foreach (DataRow row in table.Rows)
                    {
                        var cells = row.ItemArray;

                        if (frm2.getText() == cells[1].ToString())
                        {
                            MessageBox.Show("Данное название технологии занято");
                            return;
                        }
                    }

                    Form2 newForm = new Form2(frm2.getText(), "new", user);
                    newForm.Show();
                    frm2.Close();
                    this.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void getTehno(int id_user)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `tehnology` WHERE `id_user` = @user", db.getConnection());
            command.Parameters.Add("@user", MySqlDbType.Int32).Value = id_user;

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            foreach (DataRow row in table.Rows)
            {
                var cells = row.ItemArray;

                TextBox newTextBox1 = new TextBox();
                newTextBox1.BackColor = System.Drawing.SystemColors.Control;
                newTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
                newTextBox1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                newTextBox1.Location = new System.Drawing.Point(6, 3);
                newTextBox1.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
                newTextBox1.MaxLength = 50;
                newTextBox1.Name = "newTextBox1_" + cells[0];
                newTextBox1.ReadOnly = true;
                newTextBox1.Size = new System.Drawing.Size(697, 25);
                newTextBox1.TabIndex = 2;
                newTextBox1.Text = cells[1].ToString();
                newTextBox1.Modified = false;

                //Label newLabel1 = new Label();
                //newLabel1.AutoSize = true;
                //newLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                //newLabel1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                //newLabel1.Location = new System.Drawing.Point(6, 34);
                //newLabel1.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
                //newLabel1.MinimumSize = new System.Drawing.Size(694, 24);
                //newLabel1.MaximumSize = new System.Drawing.Size(694, 500);
                //newLabel1.Name = "newLabel1_" + cells[1];
                //newLabel1.Size = new System.Drawing.Size(694, 0);
                //newLabel1.TabIndex = 2;
                //newLabel1.Text = cells[2].ToString();
                //newLabel1.Click += new System.EventHandler(this.label1_Click);

                Panel newPanel1 = new Panel();
                newPanel1.AutoSize = true;
                newPanel1.Controls.Add(newTextBox1);
                //newPanel1.Controls.Add(newLabel1);
                newPanel1.Location = new System.Drawing.Point(3, 3);
                newPanel1.MaximumSize = new System.Drawing.Size(939, 500);
                newPanel1.Name = "newPanel1_" + cells[0];
                newPanel1.Size = new System.Drawing.Size(706, 119);
                newPanel1.TabIndex = 0;

                Button newButton1 = new Button();
                newButton1.Location = new System.Drawing.Point(3, 3);
                newButton1.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
                newButton1.Name = "newButton1_" + cells[0];
                newButton1.Size = new System.Drawing.Size(156, 23);
                newButton1.TabIndex = 1;
                newButton1.Text = "Редактировать технологию";
                newButton1.UseVisualStyleBackColor = true;
                newButton1.Click += buttonEditTeh_Click;
                newButton1.Tag = cells[0].ToString();

                Label newLabel2 = new Label();
                newLabel2.AutoSize = true;
                newLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                newLabel2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                newLabel2.Location = new System.Drawing.Point(165, 4);
                newLabel2.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
                newLabel2.MaximumSize = new System.Drawing.Size(90, 20);
                newLabel2.Name = "newLabel2_" + cells[1];
                newLabel2.TabIndex = 2;
                newLabel2.Text = cells[3].ToString();

                Panel newPanel2 = new Panel();
                newPanel2.AutoSize = true;
                newPanel2.Controls.Add(newButton1);
                newPanel2.Controls.Add(newLabel2);
                newPanel2.Location = new System.Drawing.Point(3, 3);
                newPanel2.MaximumSize = new System.Drawing.Size(939, 29);
                newPanel2.Name = "newPanel2_" + cells[0];
                newPanel2.Size = new System.Drawing.Size(706, 119);
                newPanel2.TabIndex = 0;

                FlowLayoutPanel newFlowLayoutPanel1 = new FlowLayoutPanel();
                newFlowLayoutPanel1.AutoSize = true;
                newFlowLayoutPanel1.Controls.Add(newPanel1);
                newFlowLayoutPanel1.Controls.Add(newPanel2);
                newFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
                newFlowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
                newFlowLayoutPanel1.Name = "newFlowLayoutPanel1_" + cells[0];
                newFlowLayoutPanel1.Size = new System.Drawing.Size(712, 154);
                newFlowLayoutPanel1.TabIndex = 1;

                flowLayoutPanel1.Controls.Add(newFlowLayoutPanel1);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonEditTeh_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string id_teh = button.Tag.ToString();

            Form2 newForm = new Form2(id_teh, "edit", user);
            newForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            newForm.Show();
            this.Close();
        }
    }
}
