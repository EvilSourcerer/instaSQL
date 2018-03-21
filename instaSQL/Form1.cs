using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Dynamic;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;
namespace instaSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listView1.AfterLabelEdit += ListView1_AfterLabelEdit;
            listView3.AfterLabelEdit += ListView3_AfterLabelEdit;
            button7.Click += Button7_Click;
            button5.Click += Button5_Click;

        }

        public List<string> column_names_ = new List<string>();
        public List<string> column_datatype_ = new List<string>();
        public Label label26 = new Label();
        public Label label19 = new Label();
        public Label label21 = new Label();
        public ComboBox comboBox1 = new ComboBox();
        public MySqlConnection dbconnect;
        public ListView listView1 = new ListView();
        public ListView listView3 = new ListView();
        public Label label22 = new Label();
        public Label label20 = new Label();
        public TextBox textBox6 = new TextBox();
        public bool Connected_ = false;
        public string originaldbname_;
        public Panel panel7 = new Panel();
        public Button button5 = new Button();
        public Panel panel6 = new Panel();
        public Button button7 = new Button();
        public int columnCount = 1;
        public List<TextBox> columnTextBoxes = new List<TextBox>();
        public List<ComboBox> columnComboBoxes = new List<ComboBox>();
        public List<Label> columnLabels = new List<Label>();
        public TextBox previoustextbox = new TextBox();
        public ComboBox previouscombobox = new ComboBox();
        public Label[] previouslabels_ = new Label[3];
        public TextBox CloneTextBox(TextBox textboxtoclone)
        {
            TextBox clonedcontrol_ = new TextBox();
            clonedcontrol_.Location = textboxtoclone.Location;
            clonedcontrol_.BackColor = textboxtoclone.BackColor;
            clonedcontrol_.ForeColor = textboxtoclone.ForeColor;
            clonedcontrol_.Visible = textboxtoclone.Visible;
            clonedcontrol_.Size = textboxtoclone.Size;
            clonedcontrol_.Font = textboxtoclone.Font;
            clonedcontrol_.Anchor = textboxtoclone.Anchor;
            return clonedcontrol_;
        }
        public ComboBox CloneComboBox(ComboBox comboboxtoclone)
        {
            ComboBox clonedcontrol_ = new ComboBox();
            clonedcontrol_.Location = comboboxtoclone.Location;
            clonedcontrol_.BackColor = comboboxtoclone.BackColor;
            clonedcontrol_.ForeColor = comboboxtoclone.ForeColor;
            clonedcontrol_.Visible = comboboxtoclone.Visible;
            clonedcontrol_.Size = comboboxtoclone.Size;
            clonedcontrol_.Font = comboboxtoclone.Font;
            clonedcontrol_.Anchor = comboboxtoclone.Anchor;
            clonedcontrol_.Items.AddRange(comboboxtoclone.Items.Cast<Object>().ToArray());
            return clonedcontrol_;
        }
        public int RowNumber = 1;
        public Label CloneLabel(Label Labeltoclone)
        {
            Label clonedcontrol_ = new Label();

            clonedcontrol_ = new Label();
            clonedcontrol_.Location = Labeltoclone.Location;
            clonedcontrol_.BackColor = Labeltoclone.BackColor;
            clonedcontrol_.ForeColor = Labeltoclone.ForeColor;
            clonedcontrol_.Visible = Labeltoclone.Visible;
            clonedcontrol_.Size = Labeltoclone.Size;
            clonedcontrol_.Font = Labeltoclone.Font;
            clonedcontrol_.Anchor = Labeltoclone.Anchor;
            clonedcontrol_.Text = Labeltoclone.Text;
            return clonedcontrol_;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Process.Start("instaSQL_Updater.exe");
            this.panel6.Location = new System.Drawing.Point(184, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(602, 535);
            this.panel6.TabIndex = 6;
            this.panel7.AutoScroll = true;
            this.panel7.Location = new System.Drawing.Point(0, 52);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(602, 409);
            this.panel7.TabIndex = 13;
            listView2.Anchor = (AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            listView3.Anchor = (AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            richTextBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);

            panel4.Parent = this;
            panel4.BringToFront();
            linkLabel1.Parent = panel4;
            linkLabel2.Parent = panel4;
            linkLabel3.Parent = panel4;
            label2.Parent = panel4;
            label6.Parent = panel4;
            linkLabel2.Visible = false;
            linkLabel3.Visible = false;
            label2.Visible = false;
            label6.Visible = false;
            if (File.ReadAllText("FirstRun.txt") == "true")
            {
                Process.Start("FirstRun.exe");
                File.WriteAllText("FirstRun.txt", "false");
                Close();
            }
            else
            {

                if (File.Exists("saved_connections.txt"))
                {
                    for (int i = 0; i < File.ReadAllText("saved_connections.txt").Split('\n').Length; i++)
                    {
                        listView2.Items.Add(Regex.Match(File.ReadAllText("saved_connections.txt").Split('\n')[i], "(?<=<name>).*(?=</name>)").Value);
                    }
                }
                if (File.ReadAllText("Theme_Pref") == "light")
                {
                }
                else if (File.ReadAllText("Theme_Pref") == "dark")
                {
                    textBox3.ForeColor = Color.White;
                    button5.BackColor = Color.FromArgb(51, 51, 55);
                    label20.ForeColor = Color.White;
                    label21.ForeColor = Color.White;
                    label22.ForeColor = Color.White;
                    label26.ForeColor = Color.White;
                    label19.ForeColor = Color.White;
                    listView3.ForeColor = Color.White;
                    listView1.ForeColor = Color.White;
                    label11.ForeColor = Color.White;
                    label12.ForeColor = Color.White;
                    label13.ForeColor = Color.White;
                    label16.ForeColor = Color.White;
                    label14.ForeColor = Color.White;
                    textBox3.BackColor = Color.FromArgb(51, 51, 55);
                    label17.ForeColor = Color.White;
                    label18.ForeColor = Color.White;
                    button2.ForeColor = Color.White;
                    listView1.BackColor = Color.FromArgb(51, 51, 55);
                    listView2.BackColor = Color.FromArgb(51, 51, 55);
                    listView3.BackColor = Color.FromArgb(51, 51, 55);
                    treeView1.BackColor = Color.FromArgb(51, 51, 55);
                    treeView1.ForeColor = Color.White;
                    richTextBox1.BackColor = Color.FromArgb(51, 51, 55);
                    richTextBox1.ForeColor = Color.White;
                    this.BackColor = Color.FromArgb(45, 45, 48);
                    listView2.BackColor = Color.FromArgb(54, 54, 56);
                    listView2.ForeColor = Color.White;
                    label10.ForeColor = Color.White;
                    label9.ForeColor = Color.White;
                    button1.BackColor = Color.FromArgb(54, 54, 56);
                    button1.ForeColor = Color.White;
                    label3.ForeColor = Color.White;
                    label4.ForeColor = Color.White;
                    label5.ForeColor = Color.White;
                    label7.ForeColor = Color.White;
                    label8.ForeColor = Color.White;
                    button6.BackColor = Color.FromArgb(54, 54, 56);
                    button6.ForeColor = Color.White;
                }
                else
                {
                    Process.Start("FirstRun.exe");
                    Close();
                }
            }
        }

        private void Dbconnect_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            if (e.CurrentState == System.Data.ConnectionState.Open)
            {
                panel5.BackColor = Color.LimeGreen;
                label1.ForeColor = Color.LimeGreen;
                label1.Text = "Connected";
            }
            if (e.CurrentState == System.Data.ConnectionState.Connecting)
            {
                panel5.BackColor = Color.Yellow;
                label1.ForeColor = Color.Yellow;
                label1.Text = "Connecting...";
            }
            if (e.CurrentState == System.Data.ConnectionState.Closed)
            {
                panel5.BackColor = Color.Red;
                label1.ForeColor = Color.Red;
                label1.Text = "Not Connected";
            }
            if (e.CurrentState == System.Data.ConnectionState.Broken)
            {
                panel5.BackColor = Color.DarkRed;
                label1.ForeColor = Color.DarkRed;
                label1.Text = "Error";
            }
            if (e.CurrentState == System.Data.ConnectionState.Executing)
            {
                panel5.BackColor = Color.Yellow;
                label1.ForeColor = Color.Yellow;
                label1.Text = "Running Command";
            }
            if (e.CurrentState == System.Data.ConnectionState.Fetching)
            {
                panel5.BackColor = Color.Yellow;
                label1.ForeColor = Color.Yellow;
                label1.Text = "Fetching...";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                if (textBox2.Text != null)
                {
                    if (textBox4.Text != null)
                    {
                        panel5.BackColor = Color.Yellow;
                        label1.Text = "Connecting...";
                        label1.ForeColor = Color.Yellow;
                        dbconnector.RunWorkerAsync();
                        button6.Text = "...";
                    }
                }
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dbconnector_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                MySqlConnectionStringBuilder conn_str = new MySqlConnectionStringBuilder();
                conn_str.Server = textBox2.Text;
                conn_str.UserID = textBox4.Text;
                conn_str.Password = textBox5.Text;
                MySqlConnection dbconnect = new MySqlConnection(conn_str.ToString());
                dbconnect.Open();
                Connected_ = true;
                e.Result = dbconnect;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dbconnector_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            button6.Text = "Attempt to connect";
            listView1.Clear();
            listView3.Clear();
            treeView1.Nodes[0].Nodes.Clear();
            if (Connected_ == true)
            {
                panel5.BackColor = Color.LimeGreen;
                label1.Text = "Connection Established";
                label1.ForeColor = Color.LimeGreen;
                linkLabel3.Visible = true;
                linkLabel2.Visible = true;
                linkLabel1.Visible = true;
                label2.Visible = true;
                label6.Visible = true;
                panel3.Visible = true;
                if (File.Exists("saved_connections.txt"))
                {
                    if (Regex.Match(File.ReadAllText("saved_connections.txt"), "(?<=<name>).*(?=</name>)").Captures[0].Value == textBox1.Text)
                    {

                    }
                    else
                    {
                        File.WriteAllText("saved_connections.txt", File.ReadAllText("saved_connections.txt") + "\n<name>" + textBox1.Text + "</name><host>" + textBox2.Text + "</host><username>" + textBox4.Text + "</username>");
                    }
                }
                else
                {
                    File.WriteAllText("saved_connections.txt", "<name>" + textBox1.Text + "</name><host>" + textBox2.Text + "</host><username>" + textBox4.Text + "</username>");
                }
                dbconnect = (MySqlConnection)e.Result;
                MySqlCommand showdatabases_ = dbconnect.CreateCommand();
                showdatabases_.CommandText = "SHOW DATABASES";
                richTextBox1.Text = richTextBox1.Text + "\n" + showdatabases_.CommandText;
                MySqlDataReader reader;
                reader = showdatabases_.ExecuteReader();
                while (reader.Read())
                {
                    string row = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row += reader.GetValue(i).ToString();
                        treeView1.Nodes[0].Nodes.Add(row, row);
                    }

                }
                reader.Close();
                TreeNodeCollection tnc = treeView1.Nodes[0].Nodes;
                MySqlCommand listtables_ = dbconnect.CreateCommand();
                for (int i = 0; i < tnc.Count; i++)
                {
                    listtables_.CommandText = "SHOW tables FROM `" + tnc[i].Name + "`";
                    richTextBox1.Text = richTextBox1.Text + "\n" + listtables_.CommandText;
                    using (MySqlDataReader tablereader = listtables_.ExecuteReader())
                    {
                        while (tablereader.Read())
                        {
                            string row = "";
                            for (int j = 0; j < tablereader.FieldCount; j++)
                            {
                                row += tablereader.GetValue(j).ToString();
                                treeView1.Nodes[0].Nodes[i].Nodes.Add(row, row);
                            }
                        }
                    }
                }
                
                panel2.Visible = false;
                dbconnect.StateChange += Dbconnect_StateChange;
                listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text == "SQL Server")
            {
                panel7.Controls.Clear();
                previouscombobox.Dispose();
                previoustextbox.Dispose();
                columnTextBoxes.RemoveRange(0, columnTextBoxes.Count);
                columnLabels.RemoveRange(0, columnLabels.Count);
                columnComboBoxes.RemoveRange(0, columnComboBoxes.Count);
                panel7.Visible = false;
                button5.Visible = false;
                listView1.Visible = true;
                listView3.Visible = false;
                listView1.Clear();
                listView1.View = View.List;
                listView1.Parent = panel3;
                listView1.Size = new Size(panel3.Width - treeView1.Width - splitContainer1.Width, panel3.Height);
                listView1.Location = new Point(182, 0);
                listView1.Font = new Font("Open Sans", 16);
                for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                {
                    listView1.Items.Add(treeView1.Nodes[0].Nodes[i].Name);
                }
                listView1.LabelEdit = true;
                listView1.Anchor = (AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            }

            if (e.Node.Parent != null && e.Node.Parent.Text == "SQL Server")
            {
                listView1.Visible = false;
                listView3.Visible = true;
                listView3.Clear();
                listView3.View = View.List;
                listView3.Parent = panel3;
                listView3.Size = new Size(panel3.Width - treeView1.Width - splitContainer1.Width, panel3.Height);
                listView3.Location = new Point(182, 0);
                listView3.Font = new Font("Open Sans", 16);
                for (int i = 0; i < e.Node.Nodes.Count; i++)
                {
                    listView3.Items.Add(e.Node.Nodes[i].Name);
                }
                listView3.LabelEdit = true;
            }
        }
        private void ListView3_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            MySqlCommand rename_table = new MySqlCommand("RENAME TABLE `" + e.Label + "`TO `" + e.Label + "`;", dbconnect);
            try
            {
                MySqlDataReader reader;
                reader = rename_table.ExecuteReader();
                while (reader.Read())
                {

                }
                reader.Close();
                treeView1.Update();
                listView1.Refresh();
                treeView1.Refresh();
            }
            catch
            {
                MessageBox.Show("Rename failed. Maybe you already have a table named " + e.Label + "?");
                return;
            }


        }
        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                ListViewItem selecteditem_ = listView1.SelectedItems[0];
                originaldbname_ = listView1.SelectedItems[0].Text;
                selecteditem_.BeginEdit();
            }
        }

        private void ListView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            MySqlCommand rename_db = new MySqlCommand("CREATE DATABASE `" + e.Label + "`; ", dbconnect);
            richTextBox1.Text = richTextBox1.Text + "\n" + rename_db.CommandText;
            try
            {
                MySqlDataReader reader;
                reader = rename_db.ExecuteReader();
                while (reader.Read())
                {

                }
                reader.Close();
            }
            catch
            {
                MessageBox.Show("Rename failed. Maybe you already have a database named " + e.Label + "?");
                return;
            }
            if (treeView1.Nodes[0].Nodes[originaldbname_].Nodes.Count == 0)
            {
                MySqlDataReader reader;
                rename_db.CommandText = "DROP DATABASE `" + originaldbname_ + "`";
                richTextBox1.Text = richTextBox1.Text + "\n" + rename_db.CommandText;
                reader = rename_db.ExecuteReader();
                reader.Close();
                treeView1.Nodes[0].Nodes[originaldbname_].Text = e.Label;
                treeView1.Nodes[0].Nodes[originaldbname_].Name = e.Label;
                originaldbname_ = e.Label;
                treeView1.Update();
                listView1.Refresh();
                treeView1.Refresh();
            }
            else
            {
                MySqlDataReader reader;
                for (int i = 0; i < treeView1.Nodes[0].Nodes[originaldbname_].Nodes.Count; i++)
                {
                    string tablename_ = treeView1.Nodes[0].Nodes[originaldbname_].Nodes[i].Name;
                    rename_db.CommandText = "ALTER TABLE `" + originaldbname_ + "`." + tablename_ + " RENAME `" + e.Label + "`." + tablename_ + "";
                    richTextBox1.Text = richTextBox1.Text + "\n" + rename_db.CommandText;
                }
                reader = rename_db.ExecuteReader();
                while (reader.Read())
                {

                }
                reader.Close();
                rename_db.CommandText = "DROP DATABASE `" + originaldbname_ + "`";
                richTextBox1.Text = richTextBox1.Text + "\n" + rename_db.CommandText;
                reader = rename_db.ExecuteReader();
                reader.Close();
                treeView1.Nodes[0].Nodes[originaldbname_].Text = e.Label;
                treeView1.Nodes[0].Nodes[originaldbname_].Name = e.Label;
                originaldbname_ = e.Label;
                treeView1.Update();
                listView1.Refresh();
                treeView1.Refresh();

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
            linkLabel2.Visible = true;
            label2.Visible = true;

        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count != 0)
            {
                panel2.Visible = true;
                linkLabel2.Visible = true;
                label2.Visible = true;
                string Existing_Connections = File.ReadAllText("saved_connections.txt");
                string selectedconnection_ = "";
                for (int i = 0; i < Existing_Connections.Split('\n').Length; i++)
                {
                    if (Existing_Connections.Split('\n')[i].Contains(listView2.SelectedItems[0].Text))
                    {
                        selectedconnection_ = Existing_Connections.Split('\n')[i];
                    }
                }
                textBox1.Text = listView2.SelectedItems[0].Text;
                textBox2.Text = Regex.Match(selectedconnection_, "(?<=<host>).*(?=</host>)").Value;
                textBox4.Text = Regex.Match(selectedconnection_, "(?<=<username>).*(?=</username>)").Value;
                panel1.Visible = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = true;
            label2.Visible = false;
            label6.Visible = false;
            linkLabel2.Visible = false;
            linkLabel3.Visible = false;
            if (dbconnect != null)
            {
                if (dbconnect.State == System.Data.ConnectionState.Open)
                {
                    dbconnect.Close();
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel3.Visible = false;
            dbconnect.Close();
            label6.Visible = false;
            linkLabel3.Visible = false;
            panel2.Visible = true;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            label12.MaximumSize = new Size(0, splitContainer1.Panel1.Width);
            this.panel7.AutoScroll = true;
            this.panel7.Location = new System.Drawing.Point(panel3.Location.X + treeView1.Width, 0);
            this.panel7.Name = "panel7";
            this.panel7.TabIndex = 13;
            this.panel7.Size = new System.Drawing.Size(panel3.Width - treeView1.Width - splitContainer1.Width, panel3.Height / 2);
            this.button5.Location = new System.Drawing.Point(treeView1.Width + 30, panel3.Height - panel7.Height + 30);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dbconnect.Close();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("SQL Server", "SQL Server");
            dbconnector.RunWorkerAsync();

            treeView1.Refresh();
            listView1.Refresh();
            listView2.Refresh();
            listView3.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Text == "SQL Server")
            {
                if (textBox3.Text != "")
                {
                    if (treeView1.Nodes[0].Nodes.ContainsKey(textBox3.Text) == false)
                    {
                        try
                        {
                            MySqlCommand newdatabase_ = dbconnect.CreateCommand();
                            newdatabase_.CommandText = "CREATE DATABASE `" + textBox3.Text + "`;";
                            MySqlDataReader reader_;
                            reader_ = newdatabase_.ExecuteReader();
                            treeView1.Nodes[0].Nodes.Add(textBox3.Text, textBox3.Text);
                            listView1.Items.Add(textBox3.Text);
                            reader_.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Query failed! Maybe you used a second editor without refreshing?");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You seem to have a database with that name already!");
                    }
                }
                else
                {
                    MessageBox.Show("Database name not specified.");
                }
            }
            if (treeView1.SelectedNode.Parent != null && treeView1.SelectedNode.Parent.Text == "SQL Server")
            {
                if (textBox3.Text != null)
                {
                    if (treeView1.SelectedNode.Nodes.ContainsKey(textBox3.Text) == false)
                    {
                        //Create table creator here
                        panel7.Visible = true;
                        button5.Visible = true;
                        label19.Text = "Create a new table";
                        label19.Size = new Size(194, 28);
                        label19.Location = new Point(12, 5);
                        label19.Font = new Font("Open Sans", 16);
                        label26.Text = "";
                        this.label26.AutoSize = true;
                        this.label26.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.label26.Location = new System.Drawing.Point(12, 9);
                        this.label26.Name = "label26";
                        this.label26.Size = new System.Drawing.Size(64, 28);
                        this.label26.TabIndex = 15;
                        this.label26.Text = "Rows";
                        panel7.Controls.Add(label26);
                        this.label22.AutoSize = true;
                        this.label22.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.label22.Location = new System.Drawing.Point(12, 43);
                        this.label22.Name = "label22";
                        this.label22.Size = new System.Drawing.Size(30, 28);
                        this.label22.TabIndex = 12;
                        this.label22.Text = "1.";
                        panel7.Controls.Add(label22);
                        this.label21.AutoSize = true;
                        this.label21.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.label21.Location = new System.Drawing.Point(307, 44);
                        this.label21.Name = "label21";
                        this.label21.Size = new System.Drawing.Size(106, 28);
                        this.label21.TabIndex = 9;
                        this.label21.Text = "Data type";
                        panel7.Controls.Add(label21);
                        this.label20.AutoSize = true;
                        this.label20.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.label20.Location = new System.Drawing.Point(48, 43);
                        this.label20.Name = "label20";
                        this.label20.Size = new System.Drawing.Size(72, 28);
                        this.label20.TabIndex = 8;
                        this.label20.Text = "Name";
                        panel7.Controls.Add(label20);
                        this.textBox6.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.textBox6.Location = new System.Drawing.Point(126, 43);
                        this.textBox6.Name = "textBox6";
                        this.textBox6.Size = new System.Drawing.Size(173, 29);
                        this.textBox6.TabIndex = 10;
                        panel7.Controls.Add(textBox6);
                        this.comboBox1.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.comboBox1.FormattingEnabled = true;
                        this.comboBox1.Location = new System.Drawing.Point(419, 43);
                        this.comboBox1.Name = "comboBox1";
                        this.comboBox1.Size = new System.Drawing.Size(170, 30);
                        this.comboBox1.TabIndex = 11;
                        comboBox1.Items.AddRange(new string[] { "TINYINT", "SMALLINT", "MEDIUMINT", "INT", "BIGINT", "DECIMAL", "FLOAT", "DOUBLE", "REAL", "BIT", "BOOLEAN", "SERIAL", "DATE", "DATETIME", "TIMESTAMP", "TIME", "YEAR", "CHAR", "VARCHAR", "TINYTEXT", "TEXT", "MEDIUMTEXT", "LONGTEXT", "BINARY", "VARBINARY", "TINYBLOB", "MEDIUMBLOB", "BLOB", "LONGBLOB", "ENUM", "SET", "GEOMETRY", "POINT", "LINESTRING", "POLYGON", "MULTIPOINT", "MULTILINESTRING", "MULTIPOLYGON", "GEOMETRYCOLLECTION" });

                        panel7.Controls.Add(comboBox1);
                        this.button7.Location = new System.Drawing.Point(30, 89);
                        this.button7.Name = "button7";
                        this.button7.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.button7.Size = new System.Drawing.Size(154, 62);
                        this.button7.TabIndex = 6;
                        this.button7.Text = "Add Column";
                        this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        this.button7.UseVisualStyleBackColor = true;
                        this.button7.BackColor = System.Drawing.Color.White;
                        this.button7.FlatAppearance.BorderSize = 0;
                        panel7.Controls.Add(button7);
                        button7.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
                        button7.BringToFront();
                        panel7.Parent = panel3;
                        panel7.BringToFront();
                        this.panel7.AutoScroll = true;
                        this.panel7.Location = new System.Drawing.Point(panel3.Location.X + treeView1.Width, 0);
                        this.panel7.Name = "panel7";
                        this.panel7.TabIndex = 13;
                        this.panel7.Size = new System.Drawing.Size(panel3.Width - treeView1.Width - splitContainer1.Width, panel3.Height / 2);
                        this.button5.BackColor = System.Drawing.Color.White;
                        this.button5.FlatAppearance.BorderSize = 0;
                        this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        this.button5.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.button5.Location = new System.Drawing.Point(treeView1.Width + 30, panel3.Height - panel7.Height + 30);
                        this.button5.Name = "button5";
                        this.button5.Size = new System.Drawing.Size(154, 62);
                        this.button5.TabIndex = 14;
                        this.button5.Text = "Add Table";
                        this.button5.UseVisualStyleBackColor = false;
                        button5.Parent = panel3;
                        button5.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
                        button5.BringToFront();
                        columnTextBoxes.Add(textBox6);
                        columnComboBoxes.Add(comboBox1);

                    }
                    else
                    {
                        MessageBox.Show("You seem to have a table with that name already!");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Text == "SQL Server")
            {
                MySqlCommand dropdb_ = dbconnect.CreateCommand();
                dropdb_.CommandText = "DROP DATABASE `" + listView1.SelectedItems[0].Text + "`;";
                MySqlDataReader reader_;
                reader_ = dropdb_.ExecuteReader();
                while (reader_.Read())
                {

                }
                treeView1.SelectedNode.Nodes[listView1.SelectedItems[0].Text].Remove();
                listView1.Items.Remove(listView1.SelectedItems[0]);
                treeView1.Refresh();
                listView1.Refresh();
                reader_.Close();
            }
        }
        
        private void Button7_Click(object sender, EventArgs e)
        {
            RowNumber++;
            if (columnTextBoxes.Count == 1)
            {

                TextBox tempTextBox = CloneTextBox(columnTextBoxes[0]);
                tempTextBox.Location = new Point(tempTextBox.Location.X, tempTextBox.Location.Y + 100);
                tempTextBox.Parent = panel7;
                panel7.Controls.Add(tempTextBox);
                columnTextBoxes.Add(CloneTextBox(tempTextBox));
                button7.Location = new Point(button7.Location.X, button7.Location.Y + 100);
                previoustextbox = tempTextBox;

                ComboBox tempComboBox = CloneComboBox(columnComboBoxes[0]);
                tempComboBox.Location = new Point(tempComboBox.Location.X, tempComboBox.Location.Y + 100);
                tempComboBox.Parent = panel7;
                panel7.Controls.Add(tempComboBox);
                previouscombobox = tempComboBox;
                columnComboBoxes.Add(tempComboBox);
                
                Label[] tempLabel = new Label[3];
                int prelabeladd_ = columnLabels.ToArray().Length;
                for (int i = 0; i < prelabeladd_; i++)
                {
                    tempLabel[i] = CloneLabel(columnLabels[i]);
                    if(tempLabel[i].Text.Contains("."))
                    {
                        tempLabel[i].Text = RowNumber.ToString() + ".";
                    }
                    tempLabel[i].Location = new Point(tempLabel[i].Location.X, tempLabel[i].Location.Y + 100);
                    tempLabel[i].Parent = panel7;
                    
                    panel7.Controls.Add(tempLabel[i]);
                    tempLabel[i].BringToFront();
                }
                previouslabels_ = tempLabel;
                
            }
            else
            {
                TextBox tempTextBox = CloneTextBox(previoustextbox);
                tempTextBox.Location = new Point(tempTextBox.Location.X, tempTextBox.Location.Y + 100);
                tempTextBox.Parent = panel7;
                panel7.Controls.Add(tempTextBox);
                button7.Location = new Point(button7.Location.X, button7.Location.Y + 100);
                previoustextbox = tempTextBox;
                columnTextBoxes.Add(tempTextBox);
                ComboBox tempComboBox = CloneComboBox(previouscombobox);
                tempComboBox.Location = new Point(tempComboBox.Location.X, tempComboBox.Location.Y + 100);
                tempComboBox.Parent = panel7;
                panel7.Controls.Add(tempComboBox);
                previouscombobox = tempComboBox;
                columnComboBoxes.Add(tempComboBox);
                Label[] tempLabel = new Label[3];
                int prelabeladd_ = previouslabels_.Length;
                for (int i = 0; i < prelabeladd_; i++)
                {
                    Label previous_label_ = previouslabels_[i];
                    tempLabel[i] = CloneLabel(previous_label_);
                    if (tempLabel[i].Text.Contains("."))
                    {
                        tempLabel[i].Text = RowNumber.ToString() + ".";
                    }
                    tempLabel[i].Location = new Point(tempLabel[i].Location.X, tempLabel[i].Location.Y + 100);
                    
                    tempLabel[i].Parent = panel7;
                    panel7.Controls.Add(tempLabel[i]);
                    tempLabel[i].BringToFront();
                }
                previouslabels_ = tempLabel;
            }
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(columnTextBoxes.ToArray().Length.ToString());
            MySqlCommand Addtable_ = dbconnect.CreateCommand();
            Addtable_.CommandText = "CREATE TABLE `" + treeView1.SelectedNode.Text + "`.`" + textBox3.Text + "` (";
            for(int i=0; i<columnTextBoxes.ToArray().Length; i++)
            {
                if (i == columnTextBoxes.ToArray().Length - 1)
                {
                    Addtable_.CommandText = Addtable_.CommandText + "`" + columnTextBoxes[i].Text + "` " + columnComboBoxes[i].Text + ");";
                }
                else
                {
                    Addtable_.CommandText = Addtable_.CommandText + "`" +columnTextBoxes[i].Text + "` " + columnComboBoxes[i].Text + ",";
                }
            }
            MySqlDataReader reader_;
            reader_ = Addtable_.ExecuteReader();
            while(reader_.Read())
            {

            }
            reader_.Close();
            treeView1.SelectedNode.Nodes.Add(textBox3.Text);
        }
    }
}
