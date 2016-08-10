namespace Lost
{
    partial class MainForm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tBaseSpeed2 = new Lost.NumberBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tBaseSpecialDefense2 = new Lost.NumberBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tBaseSpecialAttack2 = new Lost.NumberBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tBaseDefense2 = new Lost.NumberBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tBaseAttack2 = new Lost.NumberBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tBaseHealth2 = new Lost.NumberBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tBaseSpeed = new Lost.NumberBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tBaseSpecialDefense = new Lost.NumberBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tBaseSpecialAttack = new Lost.NumberBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tBaseDefense = new Lost.NumberBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tBaseAttack = new Lost.NumberBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tBaseHealth = new Lost.NumberBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cBaseType = new System.Windows.Forms.ComboBox();
            this.cBaseType2 = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 31);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(264, 372);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(282, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(490, 504);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(482, 475);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Base Stats.";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tBaseSpeed2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tBaseSpecialDefense2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tBaseSpecialAttack2);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tBaseDefense2);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tBaseAttack2);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.tBaseHealth2);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(147, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(135, 189);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Effort Yield";
            // 
            // tBaseSpeed2
            // 
            this.tBaseSpeed2.Location = new System.Drawing.Point(73, 161);
            this.tBaseSpeed2.MaximumValue = 255;
            this.tBaseSpeed2.MinimumValue = 0;
            this.tBaseSpeed2.Name = "tBaseSpeed2";
            this.tBaseSpeed2.Size = new System.Drawing.Size(56, 22);
            this.tBaseSpeed2.TabIndex = 12;
            this.tBaseSpeed2.Text = "0";
            this.tBaseSpeed2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseSpeed2.Value = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Speed";
            // 
            // tBaseSpecialDefense2
            // 
            this.tBaseSpecialDefense2.Location = new System.Drawing.Point(73, 133);
            this.tBaseSpecialDefense2.MaximumValue = 255;
            this.tBaseSpecialDefense2.MinimumValue = 0;
            this.tBaseSpecialDefense2.Name = "tBaseSpecialDefense2";
            this.tBaseSpecialDefense2.Size = new System.Drawing.Size(56, 22);
            this.tBaseSpecialDefense2.TabIndex = 11;
            this.tBaseSpecialDefense2.Text = "0";
            this.tBaseSpecialDefense2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseSpecialDefense2.Value = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Sp. Def.";
            // 
            // tBaseSpecialAttack2
            // 
            this.tBaseSpecialAttack2.Location = new System.Drawing.Point(73, 105);
            this.tBaseSpecialAttack2.MaximumValue = 255;
            this.tBaseSpecialAttack2.MinimumValue = 0;
            this.tBaseSpecialAttack2.Name = "tBaseSpecialAttack2";
            this.tBaseSpecialAttack2.Size = new System.Drawing.Size(56, 22);
            this.tBaseSpecialAttack2.TabIndex = 10;
            this.tBaseSpecialAttack2.Text = "0";
            this.tBaseSpecialAttack2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseSpecialAttack2.Value = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "Sp. Atk.";
            // 
            // tBaseDefense2
            // 
            this.tBaseDefense2.Location = new System.Drawing.Point(73, 77);
            this.tBaseDefense2.MaximumValue = 255;
            this.tBaseDefense2.MinimumValue = 0;
            this.tBaseDefense2.Name = "tBaseDefense2";
            this.tBaseDefense2.Size = new System.Drawing.Size(56, 22);
            this.tBaseDefense2.TabIndex = 9;
            this.tBaseDefense2.Text = "0";
            this.tBaseDefense2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseDefense2.Value = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 17);
            this.label10.TabIndex = 4;
            this.label10.Text = "Defense";
            // 
            // tBaseAttack2
            // 
            this.tBaseAttack2.Location = new System.Drawing.Point(73, 49);
            this.tBaseAttack2.MaximumValue = 255;
            this.tBaseAttack2.MinimumValue = 0;
            this.tBaseAttack2.Name = "tBaseAttack2";
            this.tBaseAttack2.Size = new System.Drawing.Size(56, 22);
            this.tBaseAttack2.TabIndex = 8;
            this.tBaseAttack2.Text = "0";
            this.tBaseAttack2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseAttack2.Value = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 17);
            this.label11.TabIndex = 2;
            this.label11.Text = "Attack";
            // 
            // tBaseHealth2
            // 
            this.tBaseHealth2.Location = new System.Drawing.Point(73, 21);
            this.tBaseHealth2.MaximumValue = 255;
            this.tBaseHealth2.MinimumValue = 0;
            this.tBaseHealth2.Name = "tBaseHealth2";
            this.tBaseHealth2.Size = new System.Drawing.Size(56, 22);
            this.tBaseHealth2.TabIndex = 7;
            this.tBaseHealth2.Text = "0";
            this.tBaseHealth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseHealth2.Value = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "HP";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tBaseSpeed);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tBaseSpecialDefense);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tBaseSpecialAttack);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tBaseDefense);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tBaseAttack);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tBaseHealth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 189);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stats";
            // 
            // tBaseSpeed
            // 
            this.tBaseSpeed.Location = new System.Drawing.Point(73, 161);
            this.tBaseSpeed.MaximumValue = 255;
            this.tBaseSpeed.MinimumValue = 0;
            this.tBaseSpeed.Name = "tBaseSpeed";
            this.tBaseSpeed.Size = new System.Drawing.Size(56, 22);
            this.tBaseSpeed.TabIndex = 6;
            this.tBaseSpeed.Text = "0";
            this.tBaseSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseSpeed.Value = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Speed";
            // 
            // tBaseSpecialDefense
            // 
            this.tBaseSpecialDefense.Location = new System.Drawing.Point(73, 133);
            this.tBaseSpecialDefense.MaximumValue = 255;
            this.tBaseSpecialDefense.MinimumValue = 0;
            this.tBaseSpecialDefense.Name = "tBaseSpecialDefense";
            this.tBaseSpecialDefense.Size = new System.Drawing.Size(56, 22);
            this.tBaseSpecialDefense.TabIndex = 5;
            this.tBaseSpecialDefense.Text = "0";
            this.tBaseSpecialDefense.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseSpecialDefense.Value = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Sp. Def.";
            // 
            // tBaseSpecialAttack
            // 
            this.tBaseSpecialAttack.Location = new System.Drawing.Point(73, 105);
            this.tBaseSpecialAttack.MaximumValue = 255;
            this.tBaseSpecialAttack.MinimumValue = 0;
            this.tBaseSpecialAttack.Name = "tBaseSpecialAttack";
            this.tBaseSpecialAttack.Size = new System.Drawing.Size(56, 22);
            this.tBaseSpecialAttack.TabIndex = 4;
            this.tBaseSpecialAttack.Text = "0";
            this.tBaseSpecialAttack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseSpecialAttack.Value = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sp. Atk.";
            // 
            // tBaseDefense
            // 
            this.tBaseDefense.Location = new System.Drawing.Point(73, 77);
            this.tBaseDefense.MaximumValue = 255;
            this.tBaseDefense.MinimumValue = 0;
            this.tBaseDefense.Name = "tBaseDefense";
            this.tBaseDefense.Size = new System.Drawing.Size(56, 22);
            this.tBaseDefense.TabIndex = 3;
            this.tBaseDefense.Text = "0";
            this.tBaseDefense.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseDefense.Value = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Defense";
            // 
            // tBaseAttack
            // 
            this.tBaseAttack.Location = new System.Drawing.Point(73, 49);
            this.tBaseAttack.MaximumValue = 255;
            this.tBaseAttack.MinimumValue = 0;
            this.tBaseAttack.Name = "tBaseAttack";
            this.tBaseAttack.Size = new System.Drawing.Size(56, 22);
            this.tBaseAttack.TabIndex = 2;
            this.tBaseAttack.Text = "0";
            this.tBaseAttack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseAttack.Value = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Attack";
            // 
            // tBaseHealth
            // 
            this.tBaseHealth.Location = new System.Drawing.Point(73, 21);
            this.tBaseHealth.MaximumValue = 255;
            this.tBaseHealth.MinimumValue = 0;
            this.tBaseHealth.Name = "tBaseHealth";
            this.tBaseHealth.Size = new System.Drawing.Size(56, 22);
            this.tBaseHealth.TabIndex = 1;
            this.tBaseHealth.Text = "0";
            this.tBaseHealth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBaseHealth.Value = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "HP";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(482, 475);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Evolutions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cBaseType2);
            this.groupBox3.Controls.Add(this.cBaseType);
            this.groupBox3.Location = new System.Drawing.Point(288, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(188, 81);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Types";
            // 
            // cBaseType
            // 
            this.cBaseType.FormattingEnabled = true;
            this.cBaseType.Location = new System.Drawing.Point(6, 21);
            this.cBaseType.Name = "cBaseType";
            this.cBaseType.Size = new System.Drawing.Size(176, 24);
            this.cBaseType.TabIndex = 0;
            // 
            // cBaseType2
            // 
            this.cBaseType2.FormattingEnabled = true;
            this.cBaseType2.Location = new System.Drawing.Point(6, 51);
            this.cBaseType2.Name = "cBaseType2";
            this.cBaseType2.Size = new System.Drawing.Size(176, 24);
            this.cBaseType2.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 537);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Pokémon Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private NumberBox tBaseSpecialDefense;
        private System.Windows.Forms.Label label5;
        private NumberBox tBaseSpecialAttack;
        private System.Windows.Forms.Label label4;
        private NumberBox tBaseDefense;
        private System.Windows.Forms.Label label3;
        private NumberBox tBaseAttack;
        private System.Windows.Forms.Label label2;
        private NumberBox tBaseHealth;
        private NumberBox tBaseSpeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private NumberBox tBaseSpeed2;
        private System.Windows.Forms.Label label7;
        private NumberBox tBaseSpecialDefense2;
        private System.Windows.Forms.Label label8;
        private NumberBox tBaseSpecialAttack2;
        private System.Windows.Forms.Label label9;
        private NumberBox tBaseDefense2;
        private System.Windows.Forms.Label label10;
        private NumberBox tBaseAttack2;
        private System.Windows.Forms.Label label11;
        private NumberBox tBaseHealth2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cBaseType2;
        private System.Windows.Forms.ComboBox cBaseType;
    }
}

