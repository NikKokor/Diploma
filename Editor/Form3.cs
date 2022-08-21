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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            getAllTeh();
            getAllOsob();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void getAllTeh()
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `tehnology`", db.getConnection());

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            flowLayoutPanel1.Controls.Clear();
            getTeh(table);
        }

        private void getTeh(DataTable table)
        {
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

                Label newLabel1 = new Label();
                newLabel1.AutoSize = true;
                newLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                newLabel1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                newLabel1.Location = new System.Drawing.Point(6, 34);
                newLabel1.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
                newLabel1.MinimumSize = new System.Drawing.Size(694, 24);
                newLabel1.MaximumSize = new System.Drawing.Size(694, 500);
                newLabel1.Name = "newLabel1_" + cells[1];
                newLabel1.Size = new System.Drawing.Size(694, 0);
                newLabel1.TabIndex = 2;
                newLabel1.Text = cells[2].ToString();
                newLabel1.Click += new System.EventHandler(this.label1_Click);

                Panel newPanel1 = new Panel();
                newPanel1.AutoSize = true;
                newPanel1.Controls.Add(newTextBox1);
                newPanel1.Controls.Add(newLabel1);
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
                newButton1.Text = "Просмотр технологии";
                newButton1.UseVisualStyleBackColor = true;
                newButton1.Click += buttonLookTeh_Click;
                newButton1.Tag = cells[0].ToString();

                Label newLabel2 = new Label();
                newLabel2.AutoSize = true;
                newLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                newLabel2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                newLabel2.Location = new System.Drawing.Point(400, 4);
                newLabel2.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
                newLabel2.MaximumSize = new System.Drawing.Size(90, 20);
                newLabel2.Name = "newLabel2_" + cells[1];
                newLabel2.TabIndex = 2;
                newLabel2.Text = cells[3].ToString();

                Label newLabel3 = new Label();
                newLabel3.AutoSize = true;
                newLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                newLabel3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                newLabel3.Location = new System.Drawing.Point(494, 4);
                newLabel3.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
                newLabel3.MaximumSize = new System.Drawing.Size(200, 20);
                newLabel3.Name = "newLabel3_" + cells[1];
                newLabel3.TabIndex = 2;
                newLabel3.Text = getUserName(Convert.ToInt32(cells[4]));

                Panel newPanel2 = new Panel();
                newPanel2.AutoSize = true;
                newPanel2.Controls.Add(newButton1);
                newPanel2.Controls.Add(newLabel2);
                newPanel2.Controls.Add(newLabel3);
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

        private void buttonLookTeh_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string id_teh = button.Tag.ToString();

            Form4 newForm = new Form4(id_teh);
            newForm.Show();
            this.Close();
        }

        public string getUserName(int id)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `id` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            try
            {
                return table.Rows[0].ItemArray[1].ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            EnterInAcc newForm = new EnterInAcc(this);
            newForm.Show();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Filtr_getTeh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //поиск по названию
            getListTehByName(textBox1.Text.ToLower());
        }

        public void getListTehByName(string str)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `tehnology`", db.getConnection());

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            DataTable tableTeh = new DataTable();
            DataColumn nameAttrib = new DataColumn("id", Type.GetType("System.Int32"));
            DataColumn typeAttrib = new DataColumn("name", Type.GetType("System.String"));
            DataColumn minAttrib = new DataColumn("kratkoe", Type.GetType("System.String"));
            DataColumn maxAttrib = new DataColumn("date", Type.GetType("System.DateTime"));
            DataColumn ed_izmAttrib = new DataColumn("id_user", Type.GetType("System.Int32"));

            tableTeh.Columns.Add(nameAttrib);
            tableTeh.Columns.Add(typeAttrib);
            tableTeh.Columns.Add(minAttrib);
            tableTeh.Columns.Add(maxAttrib);
            tableTeh.Columns.Add(ed_izmAttrib);

            foreach (DataRow row in table.Rows)
            {
                var cells = row.ItemArray;
                string name = cells[1].ToString().ToLower();
                List<string> list_teh = new List<string>();
                List<StringAndBool> list_search = new List<StringAndBool>();

                foreach (string s in name.Split(' '))
                {
                    list_teh.Add(s);
                }

                foreach (string s in str.Split(' '))
                {
                    StringAndBool sab = new StringAndBool();
                    sab.flag = false;
                    sab.word = s;
                    list_search.Add(sab);
                }

                for (int i = 0; i < list_search.Count; i++)
                {
                    foreach (string s in list_teh)
                    {
                        if (s == list_search[i].word)
                        {
                            list_search[i].flag = true;
                        }
                    }
                }

                bool flag = true;

                for (int i = 0; i < list_search.Count; i++)
                {
                    if (!list_search[i].flag)
                    { 
                        flag = false;
                    }
                }

                if (flag)
                {
                    DataRow r = tableTeh.NewRow();
                    r[0] = cells[0];
                    r[1] = cells[1];
                    r[2] = cells[2];
                    r[3] = cells[3];
                    r[4] = cells[4];
                    tableTeh.Rows.Add(r);
                }
            }

            if (tableTeh.Rows.Count == 0)
            {
                MessageBox.Show("Технологий с таким название не найдено");
            }
            else
            {
                flowLayoutPanel1.Controls.Clear();
                getTeh(tableTeh);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getAllTeh();
        }

        private void getAllOsob()
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `vos_etap`", db.getConnection());

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            FlowLayoutPanel newFlowLayoutPanel2 = new FlowLayoutPanel();
            newFlowLayoutPanel2.AutoSize = false;
            newFlowLayoutPanel2.AutoScroll = true;
            newFlowLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
            newFlowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            newFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            newFlowLayoutPanel2.Location = new System.Drawing.Point(-2, -2);
            newFlowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            newFlowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0);
            newFlowLayoutPanel2.Name = "newFlowLayoutPanel";
            newFlowLayoutPanel2.Size = new System.Drawing.Size(216, 825);
            newFlowLayoutPanel2.WrapContents = false;

            newFlowLayoutPanel2.TabIndex = 5;

            foreach (DataRow rowEtap in table.Rows)
            {
                var cellsEtap = rowEtap.ItemArray;

                DataTable tableOsob = new DataTable();
                command = new MySqlCommand("SELECT * FROM `vos_osob` WHERE `id_etap` = @id", db.getConnection());
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = cellsEtap[0];

                db.openConnection();

                adapter.SelectCommand = command;
                adapter.Fill(tableOsob);

                db.closeConnection();

                if (tableOsob.Rows.Count > 0)
                {

                    FlowLayoutPanel newFlowLayoutPanel1 = new FlowLayoutPanel();
                    newFlowLayoutPanel1.AutoSize = true;
                    newFlowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
                    newFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
                    newFlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
                    newFlowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
                    newFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0);
                    newFlowLayoutPanel1.Name = "newFlowLayoutPanel1_" + cellsEtap[0].ToString();
                    newFlowLayoutPanel1.Size = new System.Drawing.Size(195, 49);
                    newFlowLayoutPanel1.TabIndex = 4;
                    newFlowLayoutPanel1.Tag = cellsEtap[0].ToString();

                    TextBox newTextBox1 = new TextBox();
                    newTextBox1.BackColor = System.Drawing.SystemColors.Control;
                    newTextBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
                    newTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    newTextBox1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    newTextBox1.Location = new System.Drawing.Point(0, 0);
                    newTextBox1.Margin = new System.Windows.Forms.Padding(0);
                    newTextBox1.MaxLength = 50;
                    newTextBox1.Name = "newTextBox31_" + cellsEtap[0].ToString();
                    newTextBox1.ReadOnly = true;
                    newTextBox1.Size = new System.Drawing.Size(199, 32);
                    newTextBox1.TabIndex = 6;
                    newTextBox1.Text = cellsEtap[1].ToString();
                    newTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                    newFlowLayoutPanel1.Controls.Add(newTextBox1);

                    Panel newPanel2 = new Panel();
                    newPanel2.AutoScroll = true;
                    newPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    newPanel2.Location = new System.Drawing.Point(0, 0);
                    newPanel2.Margin = new System.Windows.Forms.Padding(0);
                    newPanel2.Name = "newPanel2_" + cellsEtap[0].ToString();
                    newPanel2.Size = new System.Drawing.Size(199, 1);
                    newPanel2.TabIndex = 7;
                    newFlowLayoutPanel1.Controls.Add(newPanel2);

                    getOsobByEtap(newFlowLayoutPanel1, cellsEtap[1].ToString());

                    Panel newPanel1 = new Panel();
                    newPanel1.AutoScroll = true;
                    newPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    newPanel1.Location = new System.Drawing.Point(0, 0);
                    newPanel1.Margin = new System.Windows.Forms.Padding(0);
                    newPanel1.Name = "newPanel1_" + cellsEtap[0].ToString();
                    newPanel1.Size = new System.Drawing.Size(199, 1);
                    newPanel1.TabIndex = 7;
                    newFlowLayoutPanel1.Controls.Add(newPanel1);

                    newFlowLayoutPanel2.Controls.Add(newFlowLayoutPanel1);
                }
            }
            panel11.Controls.Add(newFlowLayoutPanel2);
        }

        private void getOsobByEtap(FlowLayoutPanel flowPanel, string etap)
        {
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
                command = new MySqlCommand("SELECT * FROM `vos_znach` WHERE `id_osob` = @id_osob", db.getConnection());
                command.Parameters.Add("@id_osob", MySqlDbType.Int32).Value = cells[0];

                db.openConnection();

                adapter.SelectCommand = command;
                adapter.Fill(znach);

                db.closeConnection();

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

                    //получение значений
                    foreach (DataRow rowZnach in znach.Rows)
                    {
                        var cellsZnach = rowZnach.ItemArray;
                        newComboBox1.Items.Add(cellsZnach[2]);
                    }

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

                    foreach (DataRow rowZnach in znach.Rows)
                    {
                        var cellsZnach = rowZnach.ItemArray;
                        newListBox1.Items.Add(cellsZnach[2]);
                    }

                    newPanel.Controls.Add(newListBox1);
                }
                else
                {
                    newPanel.Height = 90;

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

                    newPanel.Controls.Add(newPanel1);
                    newPanel.Controls.Add(newPanel2);
                }

                flowPanel.Controls.Add(newPanel);
            }
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private List<Osob> Filtr_getCheckOsob()
        {
            List<Osob> list = new List<Osob>();
            var parent = this.FindForm();


            DB db = new DB();
            DataTable tableEtap = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `vos_etap`", db.getConnection());

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(tableEtap);

            db.closeConnection();

            foreach (DataRow row in tableEtap.Rows)
            {
                var cellsEtap = row.ItemArray;

                FlowLayoutPanel flowPanel = parent.Controls.Find("newFlowLayoutPanel1_" + cellsEtap[0].ToString(), true).FirstOrDefault() as FlowLayoutPanel;

                if (flowPanel != null)
                {
                    foreach (FlowLayoutPanel panel in flowPanel.Controls.OfType<FlowLayoutPanel>())
                    {
                        string id_osob = panel.Name.Substring(panel.Name.IndexOf('_') + 1, panel.Name.LastIndexOf('_') - panel.Name.IndexOf('_') - 1);
                        MySqlCommand com = new MySqlCommand();
                        DataTable tableOsob = new DataTable();
                        int net_znach = 0;
                        List<string> str_znach = new List<string>();
                        int[] int_znach = new int[2];

                        command = new MySqlCommand("SELECT * FROM `vos_osob` WHERE `id_osob` = @id_osob", db.getConnection());
                        command.Parameters.Add("@id_osob", MySqlDbType.Int32).Value = id_osob;

                        db.openConnection();

                        adapter.SelectCommand = command;
                        adapter.Fill(tableOsob);

                        db.closeConnection();

                        if (tableOsob.Rows[0].ItemArray[3].ToString() == "Выпадающий список")
                        {
                            ComboBox Box = panel.Controls.Find("newComboBoxOsob1_" + cellsEtap[0].ToString(), true).FirstOrDefault() as ComboBox;

                            if (Box.SelectedItem == null || Box.SelectedItem.ToString() == "")
                                net_znach = -1;
                            else
                            {
                                net_znach = 1;
                                str_znach.Add(Box.SelectedItem.ToString());
                            }

                        }
                        else if (tableOsob.Rows[0].ItemArray[3].ToString() == "Список с множественным выделением")
                        {
                            ListBox Box = panel.Controls.Find("newListBoxOsob1_" + cellsEtap[0].ToString(), true).FirstOrDefault() as ListBox;

                            if (Box.SelectedItem == null)
                                net_znach = -1;
                            else
                            {
                                net_znach = 1;
                                for (int k = 0; k < Box.SelectedItems.Count; k++)
                                {
                                    str_znach.Add(Box.SelectedItems[k].ToString());
                                }
                            }
                        }
                        else
                        {
                            NumericUpDown numericUpDown1 = panel.Controls.Find("newNumericUpDownOsob1_" + cellsEtap[0].ToString(), true).FirstOrDefault() as NumericUpDown;
                            NumericUpDown numericUpDown2 = panel.Controls.Find("newNumericUpDownOsob2_" + cellsEtap[0].ToString(), true).FirstOrDefault() as NumericUpDown;

                            if (numericUpDown1.Value == 0 && numericUpDown2.Value == 0)
                            {
                                net_znach = -1;
                            }
                            else
                            {
                                net_znach = 2;
                                int_znach[0] = Convert.ToInt32(numericUpDown1.Value);
                                int_znach[1] = Convert.ToInt32(numericUpDown2.Value);
                            }
                        }

                        if (net_znach != -1)
                        {
                            Osob osob = new Osob();
                            osob.id_osob = Convert.ToInt32(id_osob);
                            osob.name_etap = cellsEtap[1].ToString();
                            osob.name = tableOsob.Rows[0].ItemArray[2].ToString();
                            osob.type = tableOsob.Rows[0].ItemArray[3].ToString();
                            osob.str_value = str_znach;
                            osob.int_value = int_znach;

                            list.Add(osob);
                        }
                    }
                }
            }

            if (list.Count == 0)
                return null;
            else
                return list;
        }

        private void Filtr_getTeh()
        {
            List<Osob> list = Filtr_getCheckOsob();

            DataTable table = new DataTable();
            DataColumn nameAttrib = new DataColumn("id", Type.GetType("System.Int32"));
            DataColumn typeAttrib = new DataColumn("name", Type.GetType("System.String"));
            DataColumn minAttrib = new DataColumn("kratkoe", Type.GetType("System.String"));
            DataColumn maxAttrib = new DataColumn("date", Type.GetType("System.DateTime"));
            DataColumn ed_izmAttrib = new DataColumn("id_user", Type.GetType("System.Int32"));

            table.Columns.Add(nameAttrib);
            table.Columns.Add(typeAttrib);
            table.Columns.Add(minAttrib);
            table.Columns.Add(maxAttrib);
            table.Columns.Add(ed_izmAttrib);

            DB db = new DB();
            DataTable tableTeh = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `tehnology`", db.getConnection());

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(tableTeh);

            db.closeConnection();

            if (list != null)
            {
                foreach (DataRow rowTeh in tableTeh.Rows)
                {
                    var cellsTeh = rowTeh.ItemArray;

                    Filtr_Loop(cellsTeh, list, table);
                }
            }
            else
                MessageBox.Show("Выберите хотя бы одно значение");


            if (table.Rows.Count > 0)
            {
                flowLayoutPanel1.Controls.Clear();
                getTeh(table);
            }
            else
            {
                MessageBox.Show("Нет подходящих технологий");
            }
        }

        private void Filtr_Loop(object[] cellsTeh, List<Osob> list, DataTable table)
        {
            bool flag = false;

            DB db = new DB();
            DataTable tableEtap = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `etap` WHERE `id_teh` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = cellsTeh[0];

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(tableEtap);

            db.closeConnection();
            foreach (DataRow rowEtap in tableEtap.Rows)
            {
                var cellsEtap = rowEtap.ItemArray;

                List<OsobAndEtap> listOsob = new List<OsobAndEtap>();

                foreach (Osob osob in list)
                {
                    if (cellsEtap[2].ToString() == osob.name_etap)
                    {
                        OsobAndEtap osobAndEtap = new OsobAndEtap();
                        osobAndEtap.name = osob.name;
                        osobAndEtap.id_etap = Convert.ToInt32(cellsEtap[0]);
                        osobAndEtap.osob = osob;
                        listOsob.Add(osobAndEtap);
                    }
                }
                if (listOsob.Count > 0)
                {
                    foreach (OsobAndEtap osob in listOsob)
                    {
                        DataTable tableOsob = new DataTable();
                        command = new MySqlCommand("SELECT * FROM `osob` WHERE `name` = @name AND `id_etap` = @id", db.getConnection());
                        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = osob.name;
                        command.Parameters.Add("@id", MySqlDbType.Int32).Value = osob.id_etap;

                        db.openConnection();

                        adapter.SelectCommand = command;
                        adapter.Fill(tableOsob);

                        db.closeConnection();

                        foreach (DataRow rowOsob in tableOsob.Rows)
                        {
                            var cellsOsob = rowOsob.ItemArray;

                            DataTable tableZnach = new DataTable();
                            command = new MySqlCommand("SELECT * FROM `znach` WHERE `id_osob` = @id", db.getConnection());
                            command.Parameters.Add("@id", MySqlDbType.Int32).Value = cellsOsob[0];

                            db.openConnection();

                            adapter.SelectCommand = command;
                            adapter.Fill(tableZnach);

                            db.closeConnection();

                            if (cellsOsob[3].ToString() == "Выпадающий список")
                            {
                                if (tableZnach.Rows.Count > 0 && tableZnach.Rows[0].ItemArray[2].ToString() == osob.osob.str_value[0])
                                {
                                    flag = true;
                                }
                                else
                                {
                                    flag = false;
                                    return;
                                }
                            }
                            else if (cellsOsob[3].ToString() == "Список с множественным выделением")
                            {
                                int i = 0;
                                foreach (string str_znach in osob.osob.str_value)
                                {
                                    foreach (DataRow rowZnach in tableZnach.Rows)
                                    {
                                        var cellsZnach = rowZnach.ItemArray;

                                        if (str_znach == cellsZnach[2].ToString())
                                        {
                                            i++;
                                        }
                                    }
                                }

                                if (osob.osob.str_value.Count == i)
                                    flag = true;
                                else
                                {
                                    flag = false;
                                    return;
                                }

                            }
                            else
                            {
                                if (tableZnach.Rows.Count > 0)
                                {
                                    var cells = tableZnach.Rows[0].ItemArray;

                                    if (Convert.ToInt32(cells[3]) <= osob.osob.int_value[0] && Convert.ToInt32(cells[4]) <= osob.osob.int_value[1])
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        flag = false;
                                        return;
                                    }
                                }
                                else
                                {
                                    flag = false;
                                    return;
                                }
                            }


                        }
                    }
                }
            }

            if (flag)
            {
                DataRow r = table.NewRow();
                r[0] = cellsTeh[0];
                r[1] = cellsTeh[1];
                r[2] = cellsTeh[2];
                r[3] = cellsTeh[3];
                r[4] = cellsTeh[4];
                table.Rows.Add(r);
            }
        }
    }
}
