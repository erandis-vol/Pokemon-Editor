namespace Lost
{
    partial class s
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblROM = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tName = new System.Windows.Forms.TextBox();
            this.listPokemon = new System.Windows.Forms.ListView();
            this.columnID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPkmn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tBaseStatsHP = new GBAHL.Windows.Forms.DecimalBox();
            this.tBaseStatsAtk = new GBAHL.Windows.Forms.DecimalBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tBaseStatsDef = new GBAHL.Windows.Forms.DecimalBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tBaseStatsSpAtk = new GBAHL.Windows.Forms.DecimalBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tBaseStatsSpDef = new GBAHL.Windows.Forms.DecimalBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tBaseStatsSpd = new GBAHL.Windows.Forms.DecimalBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tBaseStatsSpd2 = new GBAHL.Windows.Forms.DecimalBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tBaseStatsSpDef2 = new GBAHL.Windows.Forms.DecimalBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tBaseStatsSpAtk2 = new GBAHL.Windows.Forms.DecimalBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tBaseStatsDef2 = new GBAHL.Windows.Forms.DecimalBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tBaseStatsAtk2 = new GBAHL.Windows.Forms.DecimalBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tBaseStatsHP2 = new GBAHL.Windows.Forms.DecimalBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cBaseStatsType1 = new System.Windows.Forms.ComboBox();
            this.cBaseStatsType2 = new System.Windows.Forms.ComboBox();
            this.pBaseStatsVisualizer = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBaseStatsVisualizer)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(786, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(198, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(576, 567);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(568, 541);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Base Stats.";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(568, 541);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Evolutions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lblROM);
            this.groupBox1.Location = new System.Drawing.Point(12, 485);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 109);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // lblROM
            // 
            this.lblROM.AutoSize = true;
            this.lblROM.Location = new System.Drawing.Point(6, 16);
            this.lblROM.Name = "lblROM";
            this.lblROM.Size = new System.Drawing.Size(77, 13);
            this.lblROM.TabIndex = 0;
            this.lblROM.Text = "Load a ROM...";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.tName);
            this.groupBox2.Location = new System.Drawing.Point(12, 434);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Name";
            // 
            // tName
            // 
            this.tName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tName.Location = new System.Drawing.Point(6, 19);
            this.tName.Name = "tName";
            this.tName.Size = new System.Drawing.Size(168, 20);
            this.tName.TabIndex = 0;
            // 
            // listPokemon
            // 
            this.listPokemon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listPokemon.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnID,
            this.columnPkmn});
            this.listPokemon.FullRowSelect = true;
            this.listPokemon.GridLines = true;
            this.listPokemon.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listPokemon.Location = new System.Drawing.Point(12, 27);
            this.listPokemon.MultiSelect = false;
            this.listPokemon.Name = "listPokemon";
            this.listPokemon.Size = new System.Drawing.Size(180, 401);
            this.listPokemon.TabIndex = 4;
            this.listPokemon.UseCompatibleStateImageBehavior = false;
            this.listPokemon.View = System.Windows.Forms.View.Details;
            this.listPokemon.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listPokemon_ColumnClick);
            this.listPokemon.SelectedIndexChanged += new System.EventHandler(this.listPokemon_SelectedIndexChanged);
            // 
            // columnID
            // 
            this.columnID.Text = "ID";
            this.columnID.Width = 32;
            // 
            // columnPkmn
            // 
            this.columnPkmn.Text = "Pokémon";
            this.columnPkmn.Width = 116;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pBaseStatsVisualizer);
            this.groupBox3.Controls.Add(this.tBaseStatsSpd);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tBaseStatsSpDef);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.tBaseStatsSpAtk);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.tBaseStatsDef);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tBaseStatsAtk);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.tBaseStatsHP);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(216, 175);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Base Stats.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "HP";
            // 
            // tBaseStatsHP
            // 
            this.tBaseStatsHP.Location = new System.Drawing.Point(59, 19);
            this.tBaseStatsHP.MaximumValue = 255;
            this.tBaseStatsHP.MinimumValue = 0;
            this.tBaseStatsHP.Name = "tBaseStatsHP";
            this.tBaseStatsHP.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsHP.TabIndex = 1;
            this.tBaseStatsHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsHP.Value = 0;
            // 
            // tBaseStatsAtk
            // 
            this.tBaseStatsAtk.Location = new System.Drawing.Point(59, 45);
            this.tBaseStatsAtk.MaximumValue = 255;
            this.tBaseStatsAtk.MinimumValue = 0;
            this.tBaseStatsAtk.Name = "tBaseStatsAtk";
            this.tBaseStatsAtk.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsAtk.TabIndex = 3;
            this.tBaseStatsAtk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsAtk.Value = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Attack";
            // 
            // tBaseStatsDef
            // 
            this.tBaseStatsDef.Location = new System.Drawing.Point(59, 71);
            this.tBaseStatsDef.MaximumValue = 255;
            this.tBaseStatsDef.MinimumValue = 0;
            this.tBaseStatsDef.Name = "tBaseStatsDef";
            this.tBaseStatsDef.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsDef.TabIndex = 5;
            this.tBaseStatsDef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsDef.Value = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Defense";
            // 
            // tBaseStatsSpAtk
            // 
            this.tBaseStatsSpAtk.Location = new System.Drawing.Point(59, 97);
            this.tBaseStatsSpAtk.MaximumValue = 255;
            this.tBaseStatsSpAtk.MinimumValue = 0;
            this.tBaseStatsSpAtk.Name = "tBaseStatsSpAtk";
            this.tBaseStatsSpAtk.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsSpAtk.TabIndex = 7;
            this.tBaseStatsSpAtk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsSpAtk.Value = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sp. Atk.";
            // 
            // tBaseStatsSpDef
            // 
            this.tBaseStatsSpDef.Location = new System.Drawing.Point(59, 123);
            this.tBaseStatsSpDef.MaximumValue = 255;
            this.tBaseStatsSpDef.MinimumValue = 0;
            this.tBaseStatsSpDef.Name = "tBaseStatsSpDef";
            this.tBaseStatsSpDef.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsSpDef.TabIndex = 9;
            this.tBaseStatsSpDef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsSpDef.Value = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Sp. Def.";
            // 
            // tBaseStatsSpd
            // 
            this.tBaseStatsSpd.Location = new System.Drawing.Point(59, 149);
            this.tBaseStatsSpd.MaximumValue = 255;
            this.tBaseStatsSpd.MinimumValue = 0;
            this.tBaseStatsSpd.Name = "tBaseStatsSpd";
            this.tBaseStatsSpd.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsSpd.TabIndex = 11;
            this.tBaseStatsSpd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsSpd.Value = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Speed";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tBaseStatsSpd2);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tBaseStatsSpDef2);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.tBaseStatsSpAtk2);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.tBaseStatsDef2);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.tBaseStatsAtk2);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.tBaseStatsHP2);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Location = new System.Drawing.Point(228, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(116, 175);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Effort Yield";
            // 
            // tBaseStatsSpd2
            // 
            this.tBaseStatsSpd2.Location = new System.Drawing.Point(59, 149);
            this.tBaseStatsSpd2.MaximumValue = 3;
            this.tBaseStatsSpd2.MinimumValue = 0;
            this.tBaseStatsSpd2.Name = "tBaseStatsSpd2";
            this.tBaseStatsSpd2.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsSpd2.TabIndex = 11;
            this.tBaseStatsSpd2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsSpd2.Value = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Speed";
            // 
            // tBaseStatsSpDef2
            // 
            this.tBaseStatsSpDef2.Location = new System.Drawing.Point(59, 123);
            this.tBaseStatsSpDef2.MaximumValue = 3;
            this.tBaseStatsSpDef2.MinimumValue = 0;
            this.tBaseStatsSpDef2.Name = "tBaseStatsSpDef2";
            this.tBaseStatsSpDef2.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsSpDef2.TabIndex = 9;
            this.tBaseStatsSpDef2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsSpDef2.Value = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Sp. Def.";
            // 
            // tBaseStatsSpAtk2
            // 
            this.tBaseStatsSpAtk2.Location = new System.Drawing.Point(59, 97);
            this.tBaseStatsSpAtk2.MaximumValue = 3;
            this.tBaseStatsSpAtk2.MinimumValue = 0;
            this.tBaseStatsSpAtk2.Name = "tBaseStatsSpAtk2";
            this.tBaseStatsSpAtk2.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsSpAtk2.TabIndex = 7;
            this.tBaseStatsSpAtk2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsSpAtk2.Value = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Sp. Atk.";
            // 
            // tBaseStatsDef2
            // 
            this.tBaseStatsDef2.Location = new System.Drawing.Point(59, 71);
            this.tBaseStatsDef2.MaximumValue = 3;
            this.tBaseStatsDef2.MinimumValue = 0;
            this.tBaseStatsDef2.Name = "tBaseStatsDef2";
            this.tBaseStatsDef2.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsDef2.TabIndex = 5;
            this.tBaseStatsDef2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsDef2.Value = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Defense";
            // 
            // tBaseStatsAtk2
            // 
            this.tBaseStatsAtk2.Location = new System.Drawing.Point(59, 45);
            this.tBaseStatsAtk2.MaximumValue = 3;
            this.tBaseStatsAtk2.MinimumValue = 0;
            this.tBaseStatsAtk2.Name = "tBaseStatsAtk2";
            this.tBaseStatsAtk2.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsAtk2.TabIndex = 3;
            this.tBaseStatsAtk2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsAtk2.Value = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Attack";
            // 
            // tBaseStatsHP2
            // 
            this.tBaseStatsHP2.Location = new System.Drawing.Point(59, 19);
            this.tBaseStatsHP2.MaximumValue = 3;
            this.tBaseStatsHP2.MinimumValue = 0;
            this.tBaseStatsHP2.Name = "tBaseStatsHP2";
            this.tBaseStatsHP2.Size = new System.Drawing.Size(51, 20);
            this.tBaseStatsHP2.TabIndex = 1;
            this.tBaseStatsHP2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseStatsHP2.Value = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "HP";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cBaseStatsType2);
            this.groupBox5.Controls.Add(this.cBaseStatsType1);
            this.groupBox5.Location = new System.Drawing.Point(6, 187);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(216, 73);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Types";
            // 
            // cBaseStatsType1
            // 
            this.cBaseStatsType1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBaseStatsType1.FormattingEnabled = true;
            this.cBaseStatsType1.Location = new System.Drawing.Point(6, 19);
            this.cBaseStatsType1.Name = "cBaseStatsType1";
            this.cBaseStatsType1.Size = new System.Drawing.Size(204, 21);
            this.cBaseStatsType1.TabIndex = 0;
            // 
            // cBaseStatsType2
            // 
            this.cBaseStatsType2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBaseStatsType2.FormattingEnabled = true;
            this.cBaseStatsType2.Location = new System.Drawing.Point(6, 46);
            this.cBaseStatsType2.Name = "cBaseStatsType2";
            this.cBaseStatsType2.Size = new System.Drawing.Size(204, 21);
            this.cBaseStatsType2.TabIndex = 1;
            // 
            // pBaseStatsVisualizer
            // 
            this.pBaseStatsVisualizer.Location = new System.Drawing.Point(110, 19);
            this.pBaseStatsVisualizer.Name = "pBaseStatsVisualizer";
            this.pBaseStatsVisualizer.Size = new System.Drawing.Size(100, 150);
            this.pBaseStatsVisualizer.TabIndex = 12;
            this.pBaseStatsVisualizer.TabStop = false;
            this.pBaseStatsVisualizer.Paint += new System.Windows.Forms.PaintEventHandler(this.pBaseStatsVisualizer_Paint);
            // 
            // s
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 606);
            this.Controls.Add(this.listPokemon);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "s";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pokémon Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBaseStatsVisualizer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblROM;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tName;
        private System.Windows.Forms.ListView listPokemon;
        private System.Windows.Forms.ColumnHeader columnID;
        private System.Windows.Forms.ColumnHeader columnPkmn;
        private System.Windows.Forms.GroupBox groupBox3;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsSpd;
        private System.Windows.Forms.Label label6;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsSpDef;
        private System.Windows.Forms.Label label5;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsSpAtk;
        private System.Windows.Forms.Label label4;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsDef;
        private System.Windows.Forms.Label label3;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsAtk;
        private System.Windows.Forms.Label label2;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsHP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsSpd2;
        private System.Windows.Forms.Label label7;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsSpDef2;
        private System.Windows.Forms.Label label8;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsSpAtk2;
        private System.Windows.Forms.Label label9;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsDef2;
        private System.Windows.Forms.Label label10;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsAtk2;
        private System.Windows.Forms.Label label11;
        private GBAHL.Windows.Forms.DecimalBox tBaseStatsHP2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cBaseStatsType2;
        private System.Windows.Forms.ComboBox cBaseStatsType1;
        private System.Windows.Forms.PictureBox pBaseStatsVisualizer;
    }
}

