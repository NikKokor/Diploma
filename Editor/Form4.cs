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
    public partial class Form4 : Form
    {
        private int IndexTabs = 1;
        private string nameTehno;
        private int user;

        public Form4(string Tehno)
        {
            InitializeComponent();
            getNameTeh(Tehno);
            getKratkOpis(Tehno);
            getTehnology(Tehno);
        }

        public bool getNameTeh(string Tehno)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT `name` FROM `tehnology` WHERE `id` = @id_teh", db.getConnection());
            command.Parameters.Add("@id_teh", MySqlDbType.Int32).Value = Convert.ToInt32(Tehno);

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            try
            {
                nameTehno = table.Rows[0].ItemArray[0].ToString();
                textBox1.Text = nameTehno; return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool getKratkOpis(string Tehno)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `kratkoe` FROM `tehnology` WHERE `id` = @id_teh", db.getConnection());
            command.Parameters.Add("@id_teh", MySqlDbType.Int32).Value = Convert.ToInt32(Tehno);

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            try
            {
                textBox3.Text = table.Rows[0].ItemArray[0].ToString();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool getTehnology(string Tehno)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `etap` WHERE `id_teh` = @id_teh", db.getConnection());
            command.Parameters.Add("@id_teh", MySqlDbType.Int32).Value = Convert.ToInt32(Tehno);

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            try
            {
                foreach (DataRow row in table.Rows)
                {
                    var cells = row.ItemArray;

                    TabPage page = new TabPage();
                    page.Name = "tabPageStep" + IndexTabs;
                    page.Text = cells[2].ToString();
                    AddTabs(page, nameTehno);
                    IndexTabs++;
                    tabControl1.TabPages.Add(page);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void AddTabs(TabPage newPage, string nameTeh = "")
        {
            TextBox newTextBox1 = new TextBox();
            newTextBox1.BackColor = System.Drawing.SystemColors.Control;
            newTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            newTextBox1.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            newTextBox1.Location = new System.Drawing.Point(117, 6);
            newTextBox1.MaxLength = 50;
            newTextBox1.Multiline = true;
            newTextBox1.Name = "newTextBox1_" + IndexTabs;
            newTextBox1.ReadOnly = true;
            newTextBox1.Size = new System.Drawing.Size(501, 66);
            newTextBox1.TabIndex = 0;
            newTextBox1.Text = nameTehno;
            newTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            TextBox newTextBox2 = new TextBox();
            newTextBox2.BackColor = System.Drawing.SystemColors.Control;
            newTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            newTextBox2.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            newTextBox2.Location = new System.Drawing.Point(6, 3);
            newTextBox2.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            newTextBox2.MaxLength = 50;
            newTextBox2.Name = "newTextBox2_" + IndexTabs;
            newTextBox2.ReadOnly = true;
            newTextBox2.Size = new System.Drawing.Size(550, 25);
            newTextBox2.TabIndex = 1;
            newTextBox2.Text = "Этап: " + newPage.Text;

            RichTextBox newRichTextBox1 = new RichTextBox();
            newRichTextBox1.AcceptsTab = true;
            newRichTextBox1.AllowDrop = true;
            newRichTextBox1.BackColor = System.Drawing.SystemColors.Control;
            newRichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            newRichTextBox1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            newRichTextBox1.Location = new System.Drawing.Point(6, 34);
            newRichTextBox1.Multiline = true;
            newRichTextBox1.Name = "newRichTextBox1_" + IndexTabs;
            newRichTextBox1.ReadOnly = true;
            newRichTextBox1.RightMargin = 626;
            newRichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            newRichTextBox1.Size = new System.Drawing.Size(727, 847);
            newRichTextBox1.TabIndex = 0;
            if (nameTeh != "")
                LoadRtf(newRichTextBox1, nameTeh, newPage.Text);

            #region Osob

            TextBox newTextBox3 = new TextBox();
            newTextBox3.BackColor = System.Drawing.SystemColors.Control;
            newTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            newTextBox3.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            newTextBox3.Location = new System.Drawing.Point(-2, -1);
            newTextBox3.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            newTextBox3.MaxLength = 50;
            newTextBox3.Name = "newTextBox3_" + IndexTabs;
            newTextBox3.ReadOnly = true;
            newTextBox3.Size = new System.Drawing.Size(217, 32);
            newTextBox3.TabIndex = 2;
            newTextBox3.Text = "Особенности этапа";
            newTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            FlowLayoutPanel newFlowLayoutPanel2 = new FlowLayoutPanel();
            newFlowLayoutPanel2.AutoSize = true;
            newFlowLayoutPanel2.Name = "newFlowLayoutPanel2_" + IndexTabs;
            if (nameTeh != "")
                GetListOsob(newFlowLayoutPanel2, newPage.Text, nameTeh);
            else
                GetListOsob(newFlowLayoutPanel2, newPage.Text);
            newFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            newFlowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            newFlowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            newFlowLayoutPanel2.Size = new System.Drawing.Size(195, 0);
            newFlowLayoutPanel2.TabIndex = 3;

            FlowLayoutPanel newFlowLayoutPanel1 = new FlowLayoutPanel();
            newFlowLayoutPanel1.AutoSize = true;
            newFlowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            newFlowLayoutPanel1.Controls.Add(newFlowLayoutPanel2);
            newFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            newFlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            newFlowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            newFlowLayoutPanel1.Name = "newFlowLayoutPanel1_" + IndexTabs;
            newFlowLayoutPanel1.Size = new System.Drawing.Size(195, 49);
            newFlowLayoutPanel1.TabIndex = 4;

            Panel newPanel3 = new Panel();
            newPanel3.AutoScroll = true;
            newPanel3.Controls.Add(newFlowLayoutPanel1);
            newPanel3.Location = new System.Drawing.Point(0, 34);
            newPanel3.Margin = new System.Windows.Forms.Padding(0);
            newPanel3.Name = "newPanel3_" + IndexTabs;
            newPanel3.Size = new System.Drawing.Size(214, 954);
            newPanel3.TabIndex = 5;

            Panel newPanel1 = new Panel();
            newPanel1.Controls.Add(newTextBox3);
            newPanel1.Controls.Add(newPanel3);
            newPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            newPanel1.Location = new System.Drawing.Point(748, 6);
            newPanel1.Name = "newPanel1_" + IndexTabs;
            newPanel1.Size = new System.Drawing.Size(216, 954);
            newPanel1.TabIndex = 3;

            #endregion

            Panel newPanel2 = new Panel();
            newPanel2.AutoSize = true;
            newPanel2.Controls.Add(newTextBox2);
            newPanel2.Controls.Add(newRichTextBox1);
            newPanel2.Location = new System.Drawing.Point(6, 78);
            newPanel2.Name = "newPanel2_" + IndexTabs;
            newPanel2.Size = new System.Drawing.Size(736, 585);
            newPanel2.TabIndex = 2;

            newPage.Controls.Add(newPanel1);
            newPage.Controls.Add(newPanel2);
            newPage.Controls.Add(newTextBox1);
        }

        private void GetListOsob(FlowLayoutPanel flowPanel, string etap, string Tehno = "")
        {
            flowPanel.Controls.Clear();

            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `vos_osob` WHERE `id_etap` = @id_etap", db.getConnection());
            command.Parameters.Add("@id_etap", MySqlDbType.Int32).Value = db.searchByName("id_etap", "vos_etap", etap);

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            foreach (DataRow row in table.Rows)
            {
                var cells = row.ItemArray;

                string ind = flowPanel.Name.Substring(20);

                FlowLayoutPanel newPanel = new FlowLayoutPanel();
                newPanel.Name = "newPanelOsob1_" + cells[0] + "_" + ind;
                newPanel.Width = 195;
                newPanel.Location = new System.Drawing.Point(0, 0);
                newPanel.Margin = new System.Windows.Forms.Padding(0);

                Label newlabel1 = new Label();
                newlabel1.AutoSize = true;
                newlabel1.BackColor = System.Drawing.SystemColors.Control;
                newlabel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
                newlabel1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                newlabel1.Location = new System.Drawing.Point(0, 3);
                newlabel1.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
                newlabel1.Name = "newTextBoxOsob1_" + cells[0] + "_" + ind;
                newlabel1.MaximumSize = new System.Drawing.Size(189, 0);
                newlabel1.MinimumSize = new System.Drawing.Size(0, 22);
                newlabel1.TabIndex = 3;
                newlabel1.Text = cells[2].ToString();

                newPanel.Controls.Add(newlabel1);

                DataTable znach = new DataTable();
                DataTable znach_teh = new DataTable();
                command = new MySqlCommand("SELECT * FROM `vos_znach` WHERE `id_osob` = @id_osob", db.getConnection());
                command.Parameters.Add("@id_osob", MySqlDbType.Int32).Value = cells[0];

                db.openConnection();

                adapter.SelectCommand = command;
                adapter.Fill(znach);

                db.closeConnection();

                bool flagOsob = false;
                if (Tehno != "")
                {
                    DataTable table_teh = new DataTable();
                    command = new MySqlCommand("SELECT `id_etap` FROM `etap` WHERE `id_teh` = @id_teh AND `name` = @name", db.getConnection());
                    command.Parameters.Add("@id_teh", MySqlDbType.Int32).Value = db.searchByName("id", "tehnology", Tehno);
                    command.Parameters.Add("@name", MySqlDbType.VarChar).Value = etap;
                    db.openConnection();

                    adapter.SelectCommand = command;
                    adapter.Fill(table_teh);

                    db.closeConnection();

                    DataTable table_osob = new DataTable();
                    command = new MySqlCommand("SELECT `id_osob` FROM `osob` WHERE `id_etap` = @id_etap AND `name` = @name", db.getConnection());
                    command.Parameters.Add("@id_etap", MySqlDbType.Int32).Value = table_teh.Rows[0].ItemArray[0];
                    command.Parameters.Add("@name", MySqlDbType.VarChar).Value = cells[2].ToString();
                    db.openConnection();

                    adapter.SelectCommand = command;
                    adapter.Fill(table_osob);

                    db.closeConnection();

                    if (!(table_osob.Rows.Count > 0))
                        flagOsob = false;
                    else
                    {
                        flagOsob = true;
                        command = new MySqlCommand("SELECT * FROM `znach` WHERE `id_osob` = @id_osob", db.getConnection());
                        command.Parameters.Add("@id_osob", MySqlDbType.Int32).Value = table_osob.Rows[0].ItemArray[0];

                        db.openConnection();

                        adapter.SelectCommand = command;
                        adapter.Fill(znach_teh);

                        db.closeConnection();
                    }

                }

                if (cells[3].ToString() == "Выпадающий список")
                {
                    //newPanel.Height = 58;

                    ComboBox newComboBox1 = new ComboBox();
                    newComboBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    newComboBox1.FormattingEnabled = true;
                    newComboBox1.Location = new System.Drawing.Point(3, 31);
                    newComboBox1.Name = "newComboBoxOsob1_" + ind;
                    newComboBox1.Size = new System.Drawing.Size(174, 24);
                    newComboBox1.TabIndex = 4;
                    newComboBox1.Enabled = false;

                    //получение значений
                    foreach (DataRow rowZnach in znach.Rows)
                    {
                        var cellsZnach = rowZnach.ItemArray;
                        newComboBox1.Items.Add(cellsZnach[2]);
                    }

                    if (flagOsob)
                        newComboBox1.SelectedText = znach_teh.Rows[0].ItemArray[2].ToString();

                    newPanel.Controls.Add(newComboBox1);
                }
                else if (cells[3].ToString() == "Список с множественным выделением")
                {
                    newPanel.Height = 200;

                    ListBox newListBox1 = new ListBox();
                    newListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    newListBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

                    newListBox1.FormattingEnabled = true;
                    newListBox1.HorizontalScrollbar = true;
                    newListBox1.ItemHeight = 16;
                    newListBox1.Location = new System.Drawing.Point(3, 31);
                    newListBox1.Name = "newListBoxOsob1_" + ind;
                    newListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
                    newListBox1.Size = new System.Drawing.Size(174, 146);
                    newListBox1.TabIndex = 4;
                    newListBox1.ForeColor = System.Drawing.SystemColors.WindowText;
                    newListBox1.SelectionMode = SelectionMode.None;

                    if (flagOsob)
                        foreach (DataRow rowZnach in znach_teh.Rows)
                        {
                            var cellsZnach = rowZnach.ItemArray;
                            newListBox1.Items.Add(cellsZnach[2]);
                        }

                    newPanel.Controls.Add(newListBox1);
                }
                else
                {
                    //newPanel.Height = 90;

                    Panel newPanel1 = new Panel();
                    newPanel1.Name = "newPanelOsob2_" + cells[0] + "_" + ind;
                    newPanel1.Size = new System.Drawing.Size(189, 30);
                    newPanel1.Location = new System.Drawing.Point(0, 0);
                    newPanel1.Margin = new System.Windows.Forms.Padding(0);

                    Label newLabel3 = new Label();
                    newLabel3.AutoSize = true;
                    newLabel3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    newLabel3.Location = new System.Drawing.Point(3, 3);
                    newLabel3.Name = "newLabelOsob1_" + ind;
                    newLabel3.Size = new System.Drawing.Size(27, 17);
                    newLabel3.TabIndex = 5;
                    newLabel3.Text = "от:";

                    NumericUpDown newNumericUpDown1 = new NumericUpDown();
                    newNumericUpDown1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    newNumericUpDown1.Location = new System.Drawing.Point(35, 3);
                    newNumericUpDown1.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
                    newNumericUpDown1.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
                    newNumericUpDown1.Name = "newNumericUpDownOsob1_" + ind;
                    newNumericUpDown1.Size = new System.Drawing.Size(83, 24);
                    newNumericUpDown1.TabIndex = 8;
                    newNumericUpDown1.ReadOnly = true;

                    newPanel1.Controls.Add(newLabel3);
                    newPanel1.Controls.Add(newNumericUpDown1);

                    Panel newPanel2 = new Panel();
                    newPanel2.Name = "newPanelOsob3_" + cells[0] + "_" + ind;
                    newPanel2.Size = new System.Drawing.Size(189, 30);
                    newPanel2.Location = new System.Drawing.Point(0, 0);
                    newPanel2.Margin = new System.Windows.Forms.Padding(0);

                    Label newLabel2 = new Label();
                    newLabel2.AutoSize = true;
                    newLabel2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    newLabel2.Location = new System.Drawing.Point(3, 3);
                    newLabel2.Name = "newLabelOsob2_" + ind;
                    newLabel2.Size = new System.Drawing.Size(29, 17);
                    newLabel2.TabIndex = 6;
                    newLabel2.Text = "до:";

                    NumericUpDown newNumericUpDown2 = new NumericUpDown();
                    newNumericUpDown2.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    newNumericUpDown2.Location = new System.Drawing.Point(35, 3);
                    newNumericUpDown2.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
                    newNumericUpDown2.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
                    newNumericUpDown2.Name = "newNumericUpDownOsob2_" + ind;
                    newNumericUpDown2.Size = new System.Drawing.Size(83, 24);
                    newNumericUpDown2.TabIndex = 9;
                    newNumericUpDown2.ReadOnly = true;

                    newPanel2.Controls.Add(newLabel2);
                    newPanel2.Controls.Add(newNumericUpDown2);

                    //получение значений
                    foreach (DataRow rowZnach in znach.Rows)
                    {
                        var cellsZnach = rowZnach.ItemArray;
                        newNumericUpDown1.Minimum = Convert.ToInt32(cellsZnach[3]);
                        newNumericUpDown1.Maximum = Convert.ToInt32(cellsZnach[4]);
                        newNumericUpDown2.Minimum = Convert.ToInt32(cellsZnach[3]);
                        newNumericUpDown2.Maximum = Convert.ToInt32(cellsZnach[4]);
                    }

                    if (flagOsob)
                    {
                        newNumericUpDown1.Value = Convert.ToInt32(znach_teh.Rows[0].ItemArray[3]);
                        newNumericUpDown2.Value = Convert.ToInt32(znach_teh.Rows[0].ItemArray[4]);
                    }

                    newPanel.Controls.Add(newPanel1);
                    newPanel.Controls.Add(newPanel2);
                }

                flowPanel.Controls.Add(newPanel);
            }
        }

        private void LoadRtf(RichTextBox rich, string nameTeh, string nameEtap)
        {
            rich.LoadFile(nameTeh + "\\" + nameEtap + ".rtf");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            newForm.Show();
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
