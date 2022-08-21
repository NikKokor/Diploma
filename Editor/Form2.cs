using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    public partial class Form2 : Form
    {
        private int IndexTabs = 1;
        private string nameTehno;
        private string mod = "";
        private int user;

        public Form2(string Tehno, string mod, int user)
        {
            this.user = user;
            if (mod == "new")
            {
                nameTehno = Tehno;
                InitializeComponent();
                GetFontCollection();
                PopulateFontSizes();
                createStartTabs(1);
                createStartTabs(3);
                createStartTabs(4);
                createStartTabs(5);
                createStartTabs(6);
                createStartTabs(7);
                createStartTabs(8);
                tabPanelAdd();
                textBox1.Text = nameTehno;
                
            }
            else if (mod == "edit")
            {
                this.mod = mod;
                InitializeComponent();
                GetFontCollection();
                PopulateFontSizes();
                getNameTeh(Tehno);
                getKratkOpis(Tehno);
                getTehnology(Tehno);
                tabPanelAdd();
            }
            button3.Click += buttonEditTeh_Click;
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

        private void buttonEditTeh_Click(object sender, EventArgs e)
        {
            ClickButton();
        }

        private bool ClickButton()
        {
            TextDialog frm2 = new TextDialog();

            frm2.setText(textBox1.Text);
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
                            return false;
                        }
                    }

                    textBox1.Text = frm2.getText();

                    foreach (TabPage tab in tabControl1.TabPages)
                    {
                        if (tab.Text != "Краткое описание" && tab.Text != "+")
                        {
                            string step = tab.Name.Substring(11);
                            var parent = this.FindForm();
                            TextBox textBox = parent.Controls.Find("newTextBox1_" + step, true).FirstOrDefault() as TextBox;

                            textBox.Text = frm2.getText();
                        }
                    }

                    if (mod == "edit")
                    {
                        command = new MySqlCommand("UPDATE `tehnology` SET `name` = @name_n WHERE `name` = @name", db.getConnection());
                        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameTehno;
                        command.Parameters.Add("@name_n", MySqlDbType.VarChar).Value = frm2.getText();

                        db.openConnection();

                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Название технологии обновлено");
                        }
                        else
                        {
                            MessageBox.Show("Название технологии не обновлено");
                        }
                    }
                    else
                        MessageBox.Show("Название технологии обновлено");

                    frm2.Close();
                }

            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1(user);
            newForm.Show();
            this.Close();
        }

        #region TextEditor

        #region SaveAndOpen

        private bool Save()
        {
            if (erichTextBox1.Tag == null)
                return false;

            string step = erichTextBox1.Tag.ToString();

            if (step != "")
            {
                var parent = this.FindForm();
                RichTextBox rich = parent.Controls.Find("newRichTextBox1_" + step, true).FirstOrDefault() as RichTextBox;

                rich.Rtf = erichTextBox1.Rtf;
                erichTextBox1.Text = "";
                erichTextBox1.Tag = "";
                textBox5.Text = "";
            }

            return true;
        }

        #endregion

        #region Copy

        private void Undo()
        {
            erichTextBox1.Undo();
        }

        private void Redo()
        {
            erichTextBox1.Redo();
        }
        private void Cut()
        {
            erichTextBox1.Cut();
        }

        private void Copy()
        {
            erichTextBox1.Copy();
        }

        private void Paste()
        {
            erichTextBox1.Paste();
        }

        private void SelectAll()
        {
            erichTextBox1.SelectAll();
        }

        #region General

        private void GetFontCollection()
        {
            InstalledFontCollection InsFonts = new InstalledFontCollection();

            foreach (FontFamily item in InsFonts.Families)
            {
                toolStripComboBox1.Items.Add(item.Name);
            }
            toolStripComboBox1.SelectedIndex = 0;
        }

        private void PopulateFontSizes()
        {
            for (int i = 1; i <= 75; i++)
            {
                toolStripComboBox2.Items.Add(i);
            }

            toolStripComboBox2.SelectedIndex = 11;
        }


        #endregion

        #endregion

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (erichTextBox1.SelectionFont == null)
                return;

            Font BoldFont = new Font(erichTextBox1.SelectionFont.FontFamily, erichTextBox1.SelectionFont.SizeInPoints, FontStyle.Bold);
            Font RegularFont = new Font(erichTextBox1.SelectionFont.FontFamily, erichTextBox1.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (erichTextBox1.SelectionFont.Bold)
            {
                erichTextBox1.SelectionFont = RegularFont;
            }
            else
            {
                erichTextBox1.SelectionFont = BoldFont;
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (erichTextBox1.SelectionFont == null)
                return;

            Font ItalicFont = new Font(erichTextBox1.SelectionFont.FontFamily, erichTextBox1.SelectionFont.SizeInPoints, FontStyle.Italic);
            Font RegularFont = new Font(erichTextBox1.SelectionFont.FontFamily, erichTextBox1.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (erichTextBox1.SelectionFont.Italic)
            {
                erichTextBox1.SelectionFont = RegularFont;
            }
            else
            {
                erichTextBox1.SelectionFont = ItalicFont;
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (erichTextBox1.SelectionFont == null)
                return;

            Font UnderlineFont = new Font(erichTextBox1.SelectionFont.FontFamily, erichTextBox1.SelectionFont.SizeInPoints, FontStyle.Underline);
            Font RegularFont = new Font(erichTextBox1.SelectionFont.FontFamily, erichTextBox1.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (erichTextBox1.SelectionFont.Underline)
            {
                erichTextBox1.SelectionFont = RegularFont;
            }
            else
            {
                erichTextBox1.SelectionFont = UnderlineFont;
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (erichTextBox1.SelectionFont == null)
                return;

            Font StrikeoutFont = new Font(erichTextBox1.SelectionFont.FontFamily, erichTextBox1.SelectionFont.SizeInPoints, FontStyle.Strikeout);
            Font RegularFont = new Font(erichTextBox1.SelectionFont.FontFamily, erichTextBox1.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (erichTextBox1.SelectionFont.Strikeout)
            {
                erichTextBox1.SelectionFont = RegularFont;
            }
            else
            {
                erichTextBox1.SelectionFont = StrikeoutFont;
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            erichTextBox1.SelectedText = erichTextBox1.SelectedText.ToUpper();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            erichTextBox1.SelectedText = erichTextBox1.SelectedText.ToLower();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (erichTextBox1.SelectionFont == null)
                return;

            float NewFontSize = erichTextBox1.SelectionFont.SizeInPoints + 2;

            Font NewSize = new Font(erichTextBox1.SelectionFont.Name, NewFontSize, erichTextBox1.SelectionFont.Style);

            erichTextBox1.SelectionFont = NewSize;
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if (erichTextBox1.SelectionFont == null)
                return;

            float NewFontSize = erichTextBox1.SelectionFont.SizeInPoints - 2;

            Font NewSize = new Font(erichTextBox1.SelectionFont.Name, NewFontSize, erichTextBox1.SelectionFont.Style);

            erichTextBox1.SelectionFont = NewSize;
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                erichTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void HighlightGreen_Click(object sender, EventArgs e)
        {
            erichTextBox1.SelectionBackColor = Color.FromArgb(128, 255, 128);
        }

        private void HighlightYellow_Click(object sender, EventArgs e)
        {
            erichTextBox1.SelectionBackColor = Color.FromArgb(255, 255, 128);
        }

        private void HighlightOrange_Click(object sender, EventArgs e)
        {
            erichTextBox1.SelectionBackColor = Color.FromName("Orange");
        }

        private void HighlightRed_Click(object sender, EventArgs e)
        {
            erichTextBox1.SelectionBackColor = Color.FromName("Red");
        }

        private void HighlightEmpty_Click(object sender, EventArgs e)
        {
            erichTextBox1.SelectionBackColor = Color.FromName("Window");
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (erichTextBox1.SelectionFont == null)
                return;

            Font NewFont = new Font(toolStripComboBox1.SelectedItem.ToString(), erichTextBox1.SelectionFont.Size, erichTextBox1.SelectionFont.Style);

            erichTextBox1.SelectionFont = NewFont;
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (erichTextBox1.SelectionFont == null)
                return;

            float NewSize;

            float.TryParse(toolStripComboBox2.SelectedItem.ToString(), out NewSize);

            Font NewFont = new Font(erichTextBox1.SelectionFont.Name, NewSize, erichTextBox1.SelectionFont.Style);

            erichTextBox1.SelectionFont = NewFont;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void LeftAlign()
        {
            erichTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }
        private void CenterAlign()
        {
            erichTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }
        private void RightAlign()
        {
            erichTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            LeftAlign();
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            CenterAlign();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RightAlign();
        }

        #endregion

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Название технологии")
                textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                textBox1.Text = "Название технологии";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

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

            Button newButton1 = new Button();
            newButton1.Location = new System.Drawing.Point(6, 558);
            newButton1.Name = "newButton1_" + IndexTabs;
            newButton1.Size = new System.Drawing.Size(100, 23);
            newButton1.TabIndex = 1;
            newButton1.Text = "Редактировать";
            newButton1.UseVisualStyleBackColor = true;
            newButton1.Click += newButton_Click;

            Button newButton2 = new Button();
            newButton2.Location = new System.Drawing.Point(267, 558);
            newButton2.Name = "newButton1_" + IndexTabs;
            newButton2.Size = new System.Drawing.Size(90, 23);
            newButton2.TabIndex = 4;
            newButton2.Text = "Удалить этап";
            newButton2.UseVisualStyleBackColor = true;
            newButton2.Click += new System.EventHandler(this.button2_Click);

            Button newButton4 = new Button();
            newButton4.Location = new System.Drawing.Point(112, 558);
            newButton4.Name = "newButton4_" + IndexTabs;
            newButton4.Size = new System.Drawing.Size(149, 23);
            newButton4.TabIndex = 4;
            newButton4.Text = "Изменить название этапа";
            newButton4.UseVisualStyleBackColor = true;
            newButton4.Click += button5_Click;

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
            newRichTextBox1.Size = new System.Drawing.Size(727, 518);
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
            newTextBox3.TextChanged += new System.EventHandler(this.textBox4_TextChanged);

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
            newFlowLayoutPanel2.Size = new System.Drawing.Size(195, 20);
            newFlowLayoutPanel2.TabIndex = 3;
            newFlowLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);

            Button newButton3 = new Button();
            newButton3.Location = new System.Drawing.Point(3, 623);
            newButton3.Name = "newButton3_" + IndexTabs;
            newButton3.Size = new System.Drawing.Size(138, 23);
            newButton3.TabIndex = 4;
            newButton3.Text = "Добавить особенность";
            newButton3.UseVisualStyleBackColor = true;
            newButton3.Click += AddOsob;

            FlowLayoutPanel newFlowLayoutPanel1 = new FlowLayoutPanel();
            newFlowLayoutPanel1.AutoSize = true;
            newFlowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            newFlowLayoutPanel1.Controls.Add(newFlowLayoutPanel2);
            newFlowLayoutPanel1.Controls.Add(newButton3);
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
            newPanel3.Size = new System.Drawing.Size(214, 618);
            newPanel3.TabIndex = 5;
            newPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel11_Paint);

            Panel newPanel1 = new Panel();
            newPanel1.Controls.Add(newTextBox3);
            newPanel1.Controls.Add(newPanel3);
            newPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            newPanel1.Location = new System.Drawing.Point(748, 6);
            newPanel1.Name = "newPanel1_" + IndexTabs;
            newPanel1.Size = new System.Drawing.Size(216, 654);
            newPanel1.TabIndex = 3;

            #endregion

            Panel newPanel2 = new Panel();
            newPanel2.AutoSize = true;
            newPanel2.Controls.Add(newTextBox2);
            newPanel2.Controls.Add(newButton1);
            newPanel2.Controls.Add(newButton2);
            newPanel2.Controls.Add(newButton4);
            newPanel2.Controls.Add(newRichTextBox1);
            newPanel2.Location = new System.Drawing.Point(6, 78);
            newPanel2.Name = "newPanel2_" + IndexTabs;
            newPanel2.Size = new System.Drawing.Size(736, 585);
            newPanel2.TabIndex = 2;
            newPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);

            newPage.Controls.Add(newPanel1);
            newPage.Controls.Add(newPanel2);
            newPage.Controls.Add(newTextBox1);
        }

        private void RichTextBox_ContentResized(object sender, ContentsResizedEventArgs e)
        {
            RichTextBox richTextBox = (RichTextBox)sender;

            richTextBox.Height = richTextBox.GetPreferredSize(Size.Empty).Height;
            //MessageBox.Show(richTextBox.Rtf);
        }

        private void ButtonDelOnClick(object sender, EventArgs eventArgs)
        {
            Button triggeredButton = (Button)sender;

            char step = triggeredButton.Name[triggeredButton.Name.Length - 1];
            var parent = this.FindForm();
            var findButton = parent.Controls.Find("buttonEditStep" + step, true).FirstOrDefault();
            var findTextBox = parent.Controls.Find("textBoxStep" + step, true).FirstOrDefault();
            var findRichTextBox = parent.Controls.Find("richTextBoxStep" + step, true).FirstOrDefault();
            var findPanel = parent.Controls.Find("panelStep" + step, true).FirstOrDefault();

            findRichTextBox.Dispose();
            findTextBox.Dispose();
            findButton.Dispose();
            triggeredButton.Dispose();
            findPanel.Dispose();
        }

        private void sizeImage(string rtf)
        {
            List<int> images = new List<int> { };

            int i = rtf.IndexOf("picw", 0);
            while (i > -1)
            {
                images.Add(i);
                //i = rtf.IndexOf("picw", i + )
            }

        }

        private void tabPanelAdd()
        {

            TabPage newPage = new TabPage();
            newPage.Name = "add";
            newPage.Text = "+";
            newPage.Enter += (object sender, EventArgs eventArgs) =>
            {
                ComboBoxDialog frm2 = new ComboBoxDialog("etap", tabControl1);

                DialogResult dr = frm2.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    frm2.Close();
                    tabControl1.SelectTab(0);
                }
                else if (dr == DialogResult.OK)
                {
                    tabControl1.TabPages.RemoveAt(tabControl1.TabPages.Count - 1);

                    TabPage page = new TabPage();
                    page.Name = "tabPageStep" + IndexTabs;
                    page.Text = frm2.getText();
                    AddTabs(page);
                    IndexTabs++;
                    tabControl1.TabPages.Add(page);

                    frm2.Close();
                    tabPanelAdd();
                    tabControl1.SelectTab(tabControl1.TabCount - 2);
                }
            };

            tabControl1.TabPages.Add(newPage);
        }

        private void createStartTabs(int id)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT `name` FROM `vos_etap` WHERE `id_etap` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            if (table.Rows.Count > 0)
            {
                TabPage page = new TabPage();
                page.Name = "tabPageStep" + IndexTabs;
                page.Text = table.Rows[0].ItemArray[0].ToString();
                AddTabs(page);
                IndexTabs++;
                tabControl1.TabPages.Add(page);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void newButton_Click(object sender, EventArgs eventArgs)
        {
            Button triggeredButton = (Button)sender;

            string step = triggeredButton.Name.Substring(11);
            var parent = this.FindForm();
            RichTextBox rich = parent.Controls.Find("newRichTextBox1_" + step, true).FirstOrDefault() as RichTextBox;
            TabPage page = parent.Controls.Find("tabPageStep" + step, true).FirstOrDefault() as TabPage;
            erichTextBox1.Rtf = rich.Rtf;
            erichTextBox1.Tag = step;
            textBox5.Text = "Редактирование этапа: " + page.Text;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog1.Title = "Insert image";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog1.FileName.Length > 0)
                {
                    try
                    {
                        Image img = Image.FromFile(openFileDialog1.FileName);
                        erichTextBox1.InsertImage(img);
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("Невозможно вставить файл такого формата");
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button triggeredButton = (Button)sender;

            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void AddOsob(object sender, EventArgs e)
        {
            Button triggeredButton = (Button)sender;

            string step = triggeredButton.Name.Substring(11);
            var parent = this.FindForm();
            TextBox textBox = parent.Controls.Find("newTextBox2_" + step, true).FirstOrDefault() as TextBox;
            string name_etap = textBox.Text.Substring(textBox.Text.IndexOf(':') + 2);
            FlowLayoutPanel flowLayoutPanel = parent.Controls.Find("newFlowLayoutPanel2_" + step, true).FirstOrDefault() as FlowLayoutPanel;

            AddOsobDialog addOsobDialog = new AddOsobDialog();

            DialogResult dr = addOsobDialog.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                addOsobDialog.Close();
            }
            else if (dr == DialogResult.OK)
            {
                DB db = new DB();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                if (addOsobDialog.getText()[0] == "")
                    MessageBox.Show("Введите не пустое название особенности");
                else if (addOsobDialog.getText()[1] == null)
                    MessageBox.Show("Выберите тип особенности");
                else
                {
                    if (db.searchByName("id_osob", "vos_osob", addOsobDialog.getText()[0]) != -1)
                        MessageBox.Show("Особенность с данным названием уже существует");
                    else
                    {
                        MySqlCommand command = new MySqlCommand("INSERT INTO `vos_osob` (`id_etap`, `name`, `type`) VALUES (@id, @nameOsob, @type)", db.getConnection());
                        command.Parameters.Add("@id", MySqlDbType.Int32).Value = db.searchByName("id_etap", "vos_etap", name_etap);
                        command.Parameters.Add("@nameOsob", MySqlDbType.VarChar).Value = addOsobDialog.getText()[0];
                        command.Parameters.Add("@type", MySqlDbType.VarChar).Value = addOsobDialog.getText()[1];

                        db.openConnection();

                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Новая особенность добавлена");
                            addOsobDialog.Close();
                            GetListOsob(flowLayoutPanel, name_etap);
                        }
                        else
                        {
                            MessageBox.Show("Новая особенность не добавлена");
                        }

                        db.closeConnection();
                    }
                }
            }
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

                Panel newPanel = new Panel();
                newPanel.Name = "newPanelOsob1_" + cells[0] + "_" + ind;
                newPanel.Width = 195;
                newPanel.Location = new System.Drawing.Point(0, 0);
                newPanel.Margin = new System.Windows.Forms.Padding(0);

                TextBox newTextBox1 = new TextBox();
                newTextBox1.BackColor = System.Drawing.SystemColors.Control;
                newTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
                newTextBox1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                newTextBox1.Location = new System.Drawing.Point(0, 3);
                newTextBox1.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
                newTextBox1.MaxLength = 50;
                newTextBox1.Name = "newTextBoxOsob1_" + cells[0] + "_" + ind;
                newTextBox1.ReadOnly = true;
                newTextBox1.Size = new System.Drawing.Size(195, 22);
                newTextBox1.TabIndex = 3;
                newTextBox1.Text = cells[2].ToString();
                newTextBox1.TextChanged += new System.EventHandler(this.textBox5_TextChanged);

                Button newButton1 = new Button();
                newButton1.Name = "newButtonOsob1_" + cells[0] + "_" + ind;
                newButton1.Size = new System.Drawing.Size(117, 23);
                newButton1.TabIndex = 6;
                newButton1.Text = "Добавить значение";
                newButton1.UseVisualStyleBackColor = true;

                Button newButton2 = new Button();
                newButton2.Name = "newButtonOsob2_" + cells[0] + "_" + ind;
                newButton2.Size = new System.Drawing.Size(190, 23);
                newButton2.TabIndex = 6;
                newButton2.Text = "Изменить название особенности";
                newButton2.UseVisualStyleBackColor = true;

                Button newButton3 = new Button();
                newButton3.Name = "newButtonOsob3_" + cells[0] + "_" + ind;
                newButton3.Size = new System.Drawing.Size(120, 23);
                newButton3.TabIndex = 6;
                newButton3.Text = "Изменить значения";
                newButton3.UseVisualStyleBackColor = true;

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
                    newPanel.Height = 139;
                    newButton1.Location = new System.Drawing.Point(6, 61);
                    newButton1.Click += buttonAddZnach_Click;
                    newButton2.Location = new System.Drawing.Point(6, 87);
                    newButton2.Click += buttonEditOsob_Click;
                    newButton3.Location = new System.Drawing.Point(6, 113);
                    newButton3.Click += buttonEditZnach_Click;

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

                    if (flagOsob)
                        newComboBox1.SelectedText = znach_teh.Rows[0].ItemArray[2].ToString();

                    newPanel.Controls.Add(newComboBox1);
                    newPanel.Controls.Add(newButton1);
                    newPanel.Controls.Add(newButton2);
                    newPanel.Controls.Add(newButton3);
                }
                else if (cells[3].ToString() == "Список с множественным выделением")
                {
                    newPanel.Height = 262;
                    newButton1.Location = new System.Drawing.Point(6, 183);
                    newButton1.Click += buttonAddZnach_Click;
                    newButton2.Location = new System.Drawing.Point(6, 209);
                    newButton2.Click += buttonEditOsob_Click;
                    newButton3.Location = new System.Drawing.Point(6, 235);
                    newButton3.Click += buttonEditZnach_Click;

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

                    //получение значений
                    foreach (DataRow rowZnach in znach.Rows)
                    {
                        var cellsZnach = rowZnach.ItemArray;
                        newListBox1.Items.Add(cellsZnach[2]);
                    }

                    if (flagOsob)
                        foreach (DataRow rowZnach in znach_teh.Rows)
                        {
                            var cellsZnach = rowZnach.ItemArray;
                            newListBox1.SelectedItem = cellsZnach[2].ToString();
                        }

                    newPanel.Controls.Add(newListBox1);
                    newPanel.Controls.Add(newButton1);
                    newPanel.Controls.Add(newButton2);
                    newPanel.Controls.Add(newButton3);
                }
                else
                {
                    newPanel.Height = 144;
                    newButton1.Location = new System.Drawing.Point(6, 87);
                    newButton1.Size = new System.Drawing.Size(135, 23);
                    newButton1.Text = "Задать ограничения";
                    newButton1.Click += buttonAddZnachInt_Click;
                    newButton2.Location = new System.Drawing.Point(6, 113);
                    newButton2.Click += buttonEditOsob_Click;

                    Label newLabel1 = new Label();
                    newLabel1.AutoSize = true;
                    newLabel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    newLabel1.Location = new System.Drawing.Point(3, 32);
                    newLabel1.Name = "newLabelOsob1_" + ind;
                    newLabel1.Size = new System.Drawing.Size(27, 17);
                    newLabel1.TabIndex = 5;
                    newLabel1.Text = "от:";

                    Label newLabel2 = new Label();
                    newLabel2.AutoSize = true;
                    newLabel2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    newLabel2.Location = new System.Drawing.Point(3, 59);
                    newLabel2.Name = "newLabelOsob2_" + ind;
                    newLabel2.Size = new System.Drawing.Size(29, 17);
                    newLabel2.TabIndex = 6;
                    newLabel2.Text = "до:";

                    NumericUpDown newNumericUpDown1 = new NumericUpDown();
                    newNumericUpDown1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    newNumericUpDown1.Location = new System.Drawing.Point(40, 30);
                    newNumericUpDown1.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
                    newNumericUpDown1.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
                    newNumericUpDown1.Name = "newNumericUpDownOsob1_" + ind;
                    newNumericUpDown1.Size = new System.Drawing.Size(83, 24);
                    newNumericUpDown1.TabIndex = 8;

                    NumericUpDown newNumericUpDown2 = new NumericUpDown();
                    newNumericUpDown2.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    newNumericUpDown2.Location = new System.Drawing.Point(40, 57);
                    newNumericUpDown2.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
                    newNumericUpDown2.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
                    newNumericUpDown2.Name = "newNumericUpDownOsob2_" + ind;
                    newNumericUpDown2.Size = new System.Drawing.Size(83, 24);
                    newNumericUpDown2.TabIndex = 9;

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

                    newPanel.Controls.Add(newLabel1);
                    newPanel.Controls.Add(newLabel2);
                    newPanel.Controls.Add(newNumericUpDown1);
                    newPanel.Controls.Add(newNumericUpDown2);
                    newPanel.Controls.Add(newButton1);
                    newPanel.Controls.Add(newButton2);
                }

                newPanel.Controls.Add(newTextBox1);
                flowPanel.Controls.Add(newPanel);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool isNameExists(Int32 id_osob, string name)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `vos_znach` WHERE `str_value` = @name AND `id_osob` = @id_osob", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("@id_osob", MySqlDbType.Int32).Value = id_osob;

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();
            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void buttonAddZnach_Click(object sender, EventArgs e)
        {
            //добавление значения
            Button triggeredButton = (Button)sender;
            DB db = new DB();
            MySqlCommand command = new MySqlCommand();

            string step = triggeredButton.Name.Substring(triggeredButton.Name.LastIndexOf('_') + 1);
            string id_osob = triggeredButton.Name.Substring(triggeredButton.Name.IndexOf('_') + 1,
                triggeredButton.Name.LastIndexOf('_') - triggeredButton.Name.IndexOf('_') - 1);
            var parent = this.FindForm();
            FlowLayoutPanel flowLayoutPanel = parent.Controls.Find("newFlowLayoutPanel2_" + step, true).FirstOrDefault() as FlowLayoutPanel;
            TextBox textBox = parent.Controls.Find("newTextBox2_" + step, true).FirstOrDefault() as TextBox;
            string name_etap = textBox.Text.Substring(textBox.Text.IndexOf(':') + 2);

            TextDialog frm2 = new TextDialog();

            DialogResult dr = frm2.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                frm2.Close();
            }
            else if (dr == DialogResult.OK)
            {
                if (frm2.getText() == "")
                    MessageBox.Show("Введите не пустое значение");
                else
                {
                    if (isNameExists(Convert.ToInt32(id_osob), frm2.getText()))
                        MessageBox.Show("Данное значение уже существует");
                    else
                    {
                        command = new MySqlCommand("INSERT INTO `vos_znach` (`id_osob`, `str_value`) VALUES (@id, @value)", db.getConnection());
                        command.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToInt32(id_osob);
                        command.Parameters.Add("@value", MySqlDbType.VarChar).Value = frm2.getText();

                        db.openConnection();

                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Значение добавлено");
                            frm2.Close();
                            GetListOsob(flowLayoutPanel, name_etap);
                        }
                        else
                        {
                            MessageBox.Show("Значение не добавлено");
                        }

                        db.closeConnection();
                    }
                }
            }
        }

        private void buttonAddZnachInt_Click(object sender, EventArgs e)
        {
            Button triggeredButton = (Button)sender;
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();

            string step = triggeredButton.Name.Substring(triggeredButton.Name.LastIndexOf('_') + 1);
            string id_osob = triggeredButton.Name.Substring(triggeredButton.Name.IndexOf('_') + 1,
                triggeredButton.Name.LastIndexOf('_') - triggeredButton.Name.IndexOf('_') - 1);
            var parent = this.FindForm();
            FlowLayoutPanel flowLayoutPanel = parent.Controls.Find("newFlowLayoutPanel2_" + step, true).FirstOrDefault() as FlowLayoutPanel;
            TextBox textBox = parent.Controls.Find("newTextBox2_" + step, true).FirstOrDefault() as TextBox;
            string name_etap = textBox.Text.Substring(textBox.Text.IndexOf(':') + 2);

            MinMaxDialog frm2 = new MinMaxDialog();

            DialogResult dr = frm2.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                frm2.Close();
            }
            else if (dr == DialogResult.OK)
            {
                if (frm2.getValue()[0] > frm2.getValue()[1])
                    MessageBox.Show("Минимум не может быть больше максимума");
                else
                {

                    command = new MySqlCommand("SELECT * FROM `vos_znach` WHERE `id_osob` = @id_osob", db.getConnection());
                    command.Parameters.Add("@id_osob", MySqlDbType.Int32).Value = Convert.ToInt32(id_osob);

                    db.openConnection();

                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    db.closeConnection();

                    bool flag;
                    if (table.Rows.Count > 0)
                        flag = true;
                    else
                        flag = false;

                    if (flag)
                    {
                        command = new MySqlCommand("UPDATE `vos_znach` SET `int_value_min` = @min, `int_value_max` = @max WHERE `id_osob` = @id", db.getConnection());

                    }
                    else
                    {
                        command = new MySqlCommand("INSERT INTO `vos_znach` (`id_osob`, `int_value_min`, `int_value_max`) VALUES (@id, @min, @max)", db.getConnection());
                    }
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToInt32(id_osob);
                    command.Parameters.Add("@min", MySqlDbType.Int32).Value = frm2.getValue()[0];
                    command.Parameters.Add("@max", MySqlDbType.Int32).Value = frm2.getValue()[1];

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Ограничения добавлены");
                        frm2.Close();
                        GetListOsob(flowLayoutPanel, name_etap);
                    }
                    else
                    {
                        MessageBox.Show("Ограничения не добавлены");
                    }

                    db.closeConnection();
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command;

            if (mod == "edit")
            {
                command = new MySqlCommand("DELETE FROM `tehnology` WHERE `name` = @name", db.getConnection());
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;

                db.openConnection();

                if (command.ExecuteNonQuery() == 1) { }

                db.closeConnection();
            }

            command = new MySqlCommand("INSERT INTO `tehnology` (`name`, `kratkoe`, `id_user`) VALUES (@name, @kr, @id_user)", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@kr", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@id_user", MySqlDbType.Int32).Value = user;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1) { }

            db.closeConnection();

            DirectoryInfo dirInfo = new DirectoryInfo(textBox1.Text);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            else
            {
                dirInfo.Delete(true);
                dirInfo.Create();
            }

            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab.Text == "Краткое описание" || tab.Text == "+")
                {

                }
                else
                {
                    command = new MySqlCommand("INSERT INTO `etap` (`name`, `id_teh`) VALUES (@name, @id_teh)", db.getConnection());
                    command.Parameters.Add("@name", MySqlDbType.VarChar).Value = tab.Text;
                    command.Parameters.Add("@id_teh", MySqlDbType.Int32).Value = db.searchByName("id", "tehnology", textBox1.Text);

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1) { }

                    db.closeConnection();

                    string step = tab.Name.Substring(11);
                    FlowLayoutPanel flowLayoutPanel = tab.Controls.Find("newFlowLayoutPanel2_" + step, true).FirstOrDefault() as FlowLayoutPanel;

                    foreach (Panel pan in flowLayoutPanel.Controls.OfType<Panel>())
                    {
                        string id_osob = pan.Name.Substring(pan.Name.IndexOf('_') + 1, pan.Name.LastIndexOf('_') - pan.Name.IndexOf('_') - 1);
                        MySqlCommand com = new MySqlCommand();
                        int net_znach = 0;
                        List<string> str_znach = new List<string>();
                        int[] int_znach = new int[2];

                        command = new MySqlCommand("SELECT * FROM `vos_osob` WHERE `id_osob` = @id_osob", db.getConnection());
                        command.Parameters.Add("@id_osob", MySqlDbType.Int32).Value = id_osob;

                        db.openConnection();

                        table.Clear();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);

                        db.closeConnection();

                        if (table.Rows[0].ItemArray[3].ToString() == "Выпадающий список")
                        {
                            ComboBox Box = pan.Controls.Find("newComboBoxOsob1_" + step, true).FirstOrDefault() as ComboBox;

                            if (Box.SelectedItem == null)
                                net_znach = -1;
                            else
                            {
                                net_znach = 1;
                                str_znach.Add(Box.SelectedItem.ToString());
                            }
                        }
                        else if (table.Rows[0].ItemArray[3].ToString() == "Список с множественным выделением")
                        {
                            ListBox Box = pan.Controls.Find("newListBoxOsob1_" + step, true).FirstOrDefault() as ListBox;

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
                            NumericUpDown numericUpDown1 = pan.Controls.Find("newNumericUpDownOsob1_" + step, true).FirstOrDefault() as NumericUpDown;
                            NumericUpDown numericUpDown2 = pan.Controls.Find("newNumericUpDownOsob2_" + step, true).FirstOrDefault() as NumericUpDown;

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
                            DataTable tableTehno = new DataTable();
                            command = new MySqlCommand("SELECT `id_etap` FROM `etap` WHERE `id_teh` = @id AND `name` = @name", db.getConnection());
                            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = tab.Text;
                            command.Parameters.Add("@id", MySqlDbType.Int32).Value = db.searchByName("id", "tehnology", nameTehno);

                            db.openConnection();

                            adapter.SelectCommand = command;
                            adapter.Fill(tableTehno);

                            db.closeConnection();

                            command = new MySqlCommand("INSERT INTO `osob` (`name`, `id_etap`, `type`) VALUES (@name, @id_etap, @type)", db.getConnection());
                            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = table.Rows[0].ItemArray[2].ToString();
                            command.Parameters.Add("@id_etap", MySqlDbType.Int32).Value = tableTehno.Rows[0].ItemArray[0];
                            command.Parameters.Add("@type", MySqlDbType.VarChar).Value = table.Rows[0].ItemArray[3].ToString();

                            db.openConnection();

                            if (command.ExecuteNonQuery() == 1) { }

                            db.closeConnection();

                            DataTable table_osob = new DataTable();
                            command = new MySqlCommand("SELECT `id_osob` FROM `osob` WHERE `id_etap` = @id_etap AND `name` = @name", db.getConnection());
                            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = table.Rows[0].ItemArray[2].ToString();
                            command.Parameters.Add("@id_etap", MySqlDbType.Int32).Value = tableTehno.Rows[0].ItemArray[0];

                            db.openConnection();

                            adapter.SelectCommand = command;
                            adapter.Fill(table_osob);

                            db.closeConnection();

                            int id = Convert.ToInt32(table_osob.Rows[0].ItemArray[0]);

                            if (net_znach == 1)
                            {
                                for (int j = 0; j < str_znach.Count; j++)
                                {
                                    com = new MySqlCommand("INSERT INTO `znach` (`id_osob`, `str_value`) VALUES (@id_osob, @value)", db.getConnection());
                                    com.Parameters.Add("@id_osob", MySqlDbType.Int32).Value = id;
                                    com.Parameters.Add("@value", MySqlDbType.VarChar).Value = str_znach[j];

                                    db.openConnection();

                                    if (com.ExecuteNonQuery() == 1) { }

                                    db.closeConnection();
                                }
                            }
                            else if (net_znach == 2)
                            {
                                com = new MySqlCommand("INSERT INTO `znach` (`id_osob`, `int_value_min`, `int_value_max`) VALUES (@id_osob, @min, @max)", db.getConnection());
                                com.Parameters.Add("@id_osob", MySqlDbType.Int32).Value = id;
                                com.Parameters.Add("@min", MySqlDbType.Int32).Value = int_znach[0];
                                com.Parameters.Add("@max", MySqlDbType.Int32).Value = int_znach[1];

                                db.openConnection();

                                if (com.ExecuteNonQuery() == 1) { }

                                db.closeConnection();
                            }
                        }
                    }

                    #region saveInFiles

                    try
                    {
                        RichTextBox rich = tab.Controls.Find("newRichTextBox1_" + step, true).FirstOrDefault() as RichTextBox;
                        string str;
                        if (rich.Rtf == null)
                            str = "";
                        else
                            str = rich.Rtf;

                        FileInfo fileInfo = new FileInfo(textBox1.Text + "\\" + tab.Text + ".rtf");
                        FileStream fs = fileInfo.Create();
                        fs.Write(Encoding.UTF8.GetBytes(str), 0, str.Length);
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    #endregion
                }
            }

            Form1 newForm = new Form1(user);
            newForm.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Button triggeredButton = (Button)sender;

            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();

            var parent = this.FindForm();
            string step = triggeredButton.Name.Substring(triggeredButton.Name.LastIndexOf('_') + 1);
            TextBox textBox = parent.Controls.Find("newTextBox2_" + step, true).FirstOrDefault() as TextBox;
            TabPage page = parent.Controls.Find("tabPageStep" + step, true).FirstOrDefault() as TabPage;
            string name_etap = textBox.Text.Substring(textBox.Text.IndexOf(':') + 2);

            command = new MySqlCommand("SELECT `id_etap` FROM `etap` WHERE `name` = @old_name AND `id_teh` = @id", db.getConnection());
            command.Parameters.Add("@old_name", MySqlDbType.VarChar).Value = name_etap;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = db.searchByName("id", "tehnology", nameTehno);

            db.openConnection();

            table.Clear();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            int id_etap = -1;

            if (table.Rows.Count > 0)
                id_etap = Convert.ToInt32(table.Rows[0].ItemArray[0]);

            table.Clear();

            TextDialog frm2 = new TextDialog();
            frm2.setText(name_etap);

            DialogResult dr = frm2.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                frm2.Close();
            }
            else if (dr == DialogResult.OK)
            {
                if (frm2.getText() == "")
                    MessageBox.Show("Введите не пустое название этапа");
                else
                {
                    DataTable table_ve = new DataTable();
                    command = new MySqlCommand("SELECT * FROM `vos_etap`", db.getConnection());

                    db.openConnection();

                    adapter.SelectCommand = command;
                    adapter.Fill(table_ve);

                    db.closeConnection();

                    foreach (DataRow row in table_ve.Rows)
                    {
                        var cells = row.ItemArray;

                        if (frm2.getText() == cells[1].ToString())
                        {
                            MessageBox.Show("Данное название этапа занято");
                            return;
                        }
                    }

                    DataTable table_e = new DataTable();
                    command = new MySqlCommand("SELECT * FROM `etap` WHERE `name` = @old_name AND `id_etap` != @id", db.getConnection());
                    command.Parameters.Add("@old_name", MySqlDbType.VarChar).Value = name_etap;
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id_etap;

                    db.openConnection();

                    adapter.SelectCommand = command;
                    adapter.Fill(table_e);

                    db.closeConnection();

                    if (table_e.Rows.Count > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Данный этап используется в других технологиях, вы уверены в изменении?", "Предупреждение", MessageBoxButtons.OKCancel);
                        if (dialogResult == DialogResult.OK)
                        {
                            //do something
                        }
                        else if (dialogResult == DialogResult.Cancel)
                        {
                            frm2.Close();
                            return;
                        }
                    }

                    command = new MySqlCommand("UPDATE `vos_etap` SET `name` = @new_name WHERE `name` = @old_name", db.getConnection());
                    command.Parameters.Add("@old_name", MySqlDbType.VarChar).Value = name_etap;
                    command.Parameters.Add("@new_name", MySqlDbType.VarChar).Value = frm2.getText();

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Название этапа обновлено");
                        textBox.Text = "Этап: " + frm2.getText();
                        page.Text = frm2.getText();
                        frm2.Close();
                    }
                    else
                    {
                        MessageBox.Show("Название этапа не обновлено");
                    }

                    db.closeConnection();
                }
            }
        }

        private void buttonEditOsob_Click(object sender, EventArgs e)
        {
            Button triggeredButton = (Button)sender;

            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();

            var parent = this.FindForm();
            string step = triggeredButton.Name.Substring(triggeredButton.Name.LastIndexOf('_') + 1);
            TextBox textBox = parent.Controls.Find("newTextBox2_" + step, true).FirstOrDefault() as TextBox;
            string name_etap = textBox.Text.Substring(textBox.Text.IndexOf(':') + 2);
            string id_osob = triggeredButton.Name.Substring(triggeredButton.Name.IndexOf('_') + 1,
                triggeredButton.Name.LastIndexOf('_') - triggeredButton.Name.IndexOf('_') - 1);
            TextBox textBoxOsob = parent.Controls.Find("newTextBoxOsob1_" + id_osob + "_" + step, true).FirstOrDefault() as TextBox;
            string name_osob = textBoxOsob.Text;
            FlowLayoutPanel flowLayoutPanel = parent.Controls.Find("newFlowLayoutPanel2_" + step, true).FirstOrDefault() as FlowLayoutPanel;

            command = new MySqlCommand("SELECT `id_etap` FROM `etap` WHERE `name` = @old_name AND `id_teh` = @id", db.getConnection());
            command.Parameters.Add("@old_name", MySqlDbType.VarChar).Value = name_etap;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = db.searchByName("id", "tehnology", nameTehno);

            db.openConnection();

            table.Clear();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            int id_etap = -1;

            if (table.Rows.Count > 0)
                id_etap = Convert.ToInt32(table.Rows[0].ItemArray[0]);

            table.Clear();

            TextDialog frm2 = new TextDialog();
            frm2.setText(name_osob);

            DialogResult dr = frm2.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                frm2.Close();
            }
            else if (dr == DialogResult.OK)
            {
                if (frm2.getText() == "")
                    MessageBox.Show("Введите не пустое название особенности");
                else
                {
                    DataTable table_vo = new DataTable();
                    command = new MySqlCommand("SELECT * FROM `vos_osob`", db.getConnection());

                    db.openConnection();

                    adapter.SelectCommand = command;
                    adapter.Fill(table_vo);

                    db.closeConnection();

                    foreach (DataRow row in table_vo.Rows)
                    {
                        var cells = row.ItemArray;

                        if (frm2.getText() == cells[2].ToString())
                        {
                            MessageBox.Show("Данное название особенности занято");
                            return;
                        }
                    }

                    DataTable table_o = new DataTable();
                    command = new MySqlCommand("SELECT * FROM `osob` WHERE `name` = @old_name AND `id_etap` != @id", db.getConnection());
                    command.Parameters.Add("@old_name", MySqlDbType.VarChar).Value = name_osob;
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id_etap;

                    db.openConnection();

                    table.Clear();
                    adapter.SelectCommand = command;
                    adapter.Fill(table_o);

                    db.closeConnection();

                    if (table_o.Rows.Count > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Данная особенность используется в других технологиях, вы уверены в изменении?", "Предупреждение", MessageBoxButtons.OKCancel);
                        if (dialogResult == DialogResult.OK)
                        {
                            //do something
                        }
                        else if (dialogResult == DialogResult.Cancel)
                        {
                            frm2.Close();
                            return;
                        }
                    }

                    command = new MySqlCommand("UPDATE `vos_osob` SET `name` = @new_name WHERE `name` = @old_name", db.getConnection());
                    command.Parameters.Add("@old_name", MySqlDbType.VarChar).Value = name_osob;
                    command.Parameters.Add("@new_name", MySqlDbType.VarChar).Value = frm2.getText();

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Название особенности обновлено");
                        GetListOsob(flowLayoutPanel, name_etap);
                        frm2.Close();
                    }
                    else
                    {
                        MessageBox.Show("Название особенности не обновлено");
                    }

                    db.closeConnection();
                }
            }
        }

        private void buttonEditZnach_Click(object sender, EventArgs e)
        {
            Button triggeredButton = (Button)sender;

            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();

            var parent = this.FindForm();
            string step = triggeredButton.Name.Substring(triggeredButton.Name.LastIndexOf('_') + 1);
            TextBox textBox = parent.Controls.Find("newTextBox2_" + step, true).FirstOrDefault() as TextBox;
            string name_etap = textBox.Text.Substring(textBox.Text.IndexOf(':') + 2);
            string id_osob = triggeredButton.Name.Substring(triggeredButton.Name.IndexOf('_') + 1,
                triggeredButton.Name.LastIndexOf('_') - triggeredButton.Name.IndexOf('_') - 1);
            TextBox textBoxOsob = parent.Controls.Find("newTextBoxOsob1_" + id_osob + "_" + step, true).FirstOrDefault() as TextBox;
            string name_osob = textBoxOsob.Text;
            FlowLayoutPanel flowLayoutPanel = parent.Controls.Find("newFlowLayoutPanel2_" + step, true).FirstOrDefault() as FlowLayoutPanel;

            command = new MySqlCommand("SELECT `id_etap` FROM `etap` WHERE `name` = @old_name AND `id_teh` = @id", db.getConnection());
            command.Parameters.Add("@old_name", MySqlDbType.VarChar).Value = name_etap;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = db.searchByName("id", "tehnology", nameTehno);

            db.openConnection();

            table.Clear();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            db.closeConnection();

            int id_etap = -1;

            if (table.Rows.Count > 0)
                id_etap = Convert.ToInt32(table.Rows[0].ItemArray[0]);

            table.Clear();

            DataTable table_osob = new DataTable();
            command = new MySqlCommand("SELECT * FROM `osob`", db.getConnection());

            db.openConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table_osob);

            db.closeConnection();

            ComboBoxDialog frm2 = new ComboBoxDialog(name_osob);

            DialogResult dr = frm2.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                frm2.Close();
            }
            else if (dr == DialogResult.OK)
            {
                if (frm2.getText() == null)
                    MessageBox.Show("Выберите значение");
                else
                {
                    TextDialog frm1 = new TextDialog();

                    frm1.setText(frm2.getText());
                    DialogResult dr1 = frm1.ShowDialog(this);
                    if (dr1 == DialogResult.Cancel)
                    {
                        frm1.Close();
                    }
                    else if (dr1 == DialogResult.OK)
                    {
                        if (frm1.getText() == "")
                            MessageBox.Show("Введите не пустое значение");
                        else
                        {
                            DataTable table_vz = new DataTable();
                            command = new MySqlCommand("SELECT * FROM `vos_znach`", db.getConnection());

                            db.openConnection();

                            adapter.SelectCommand = command;
                            adapter.Fill(table_vz);

                            db.closeConnection();

                            foreach (DataRow row in table_vz.Rows)
                            {
                                var cells = row.ItemArray;

                                if (frm1.getText() == cells[2].ToString() && id_osob == cells[1].ToString())
                                {
                                    MessageBox.Show("Данное значение занято");
                                    return;
                                }
                            }

                            DataTable table_z = new DataTable();
                            command = new MySqlCommand("SELECT `id_osob` FROM `znach` WHERE `str_value` = @value", db.getConnection());
                            command.Parameters.Add("@value", MySqlDbType.VarChar).Value = frm2.getText();

                            db.openConnection();

                            adapter.SelectCommand = command;
                            adapter.Fill(table_z);

                            db.closeConnection();

                            bool flag = false;
                            foreach (DataRow row in table_osob.Rows)
                            {
                                var cells = row.ItemArray;

                                foreach (DataRow row_z in table_z.Rows)
                                {
                                    var cells_z = row_z.ItemArray;

                                    if (cells[0].ToString() == cells_z[0].ToString() && cells[2].ToString() == name_osob && id_etap != Convert.ToInt32(cells[1]))
                                    {
                                        flag = true;
                                    }
                                }
                            }

                            if (flag)
                            {
                                DialogResult dialogResult = MessageBox.Show("Данное значение используется в других технологиях, вы уверены в изменении?", "Предупреждение", MessageBoxButtons.OKCancel);
                                if (dialogResult == DialogResult.OK)
                                {
                                    //do something
                                }
                                else if (dialogResult == DialogResult.Cancel)
                                {
                                    frm1.Close();
                                    frm2.Close();
                                    return;
                                }
                            }

                            command = new MySqlCommand("UPDATE `vos_znach` SET `str_value` = @new_value WHERE `str_value` = @value AND `id_osob` = @id_osob", db.getConnection());
                            command.Parameters.Add("@value", MySqlDbType.VarChar).Value = frm2.getText();
                            command.Parameters.Add("@new_value", MySqlDbType.VarChar).Value = frm1.getText();
                            command.Parameters.Add("@id_osob", MySqlDbType.Int32).Value = db.searchByName("id_osob", "vos_osob", name_osob);

                            db.openConnection();

                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Значение обновлено");
                                GetListOsob(flowLayoutPanel, name_etap);
                                frm1.Close();
                                frm2.Close();
                            }
                            else
                            {
                                MessageBox.Show("Значение не обновлено");
                            }

                            db.closeConnection();
                        }
                    }
                }
            }
        }

        private void LoadRtf(RichTextBox rich, string nameTeh, string nameEtap)
        {
            rich.LoadFile(nameTeh + "\\" + nameEtap + ".rtf");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }
    }
}
